namespace Tredz.BikeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly ILogger<BrandsController> _logger;
    private readonly IBikeService _bikeService;

    public BrandsController(ILogger<BrandsController> logger, IBikeService bikeService)
    {
        _logger = logger;
        _bikeService = bikeService;
    }

    // GET: api/Brands
    [HttpGet]
    public async Task<IEnumerable<Brand>> GetBrandsAsync()
    {
        return await _bikeService.GetBrandsAsync();
    }

    // GET: api/Brands/2
    [HttpGet("{id}")]
    public async Task<ActionResult<BrandDto>> GetBrandByIdAsync(int id)
    {
        var brand = await _bikeService.GetBrandByIdAsync(id);

        if (brand == null)
        {
            return NotFound();
        }

        return BrandToDto(brand);
    }

    // PUT: api/Brands/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrandAsync(int id, BrandDto brandDto)
    {
        if (id != brandDto.Id)
        {
            return BadRequest();
        }

        var brand = await _bikeService.GetBrandByIdAsync(id);

        if (brand == null)
        {
            return NotFound();
        }

        brand.Name = brandDto.Name;
        brand.IsStocked = brandDto.IsStocked;

        

        return NoContent();
    }

    // POST: api/Brands
    [HttpPost]
    public async Task<ActionResult<BrandDto>> CreateBrand(BrandDto brandDto)
    {
        var brand = new Brand
        {
            IsStocked = brandDto.IsStocked,
            Name = brandDto.Name
        };

        brand = await _bikeService.CreateBrandAsync(brand);

        return CreatedAtAction(nameof(GetBrandByIdAsync), new { id = brand.Id }, BrandToDto(brand));
    }

    // DELETE: api/Brands/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrandAsync(int id)
    {
        var brand = await _bikeService.GetBrandByIdAsync(id);

        if (brand == null)
        {
            return NotFound();
        }

        await _bikeService.DeleteBrandAsync(brand.Id);

        return NoContent();
    }

    private static BrandDto BrandToDto(Brand brand) =>
        new()
        {
            Id = brand.Id,
            Name = brand.Name,
            IsStocked = brand.IsStocked
        };
}

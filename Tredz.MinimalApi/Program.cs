using Microsoft.OpenApi.Models;
using Tredz.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BrandDB>(options => options.UseInMemoryDatabase("items"));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tredz Minimal API with EF",
        Description = "Basic CRUD Operations using EF Core",
        Version = "v1"
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tredz Minimal API V1");
});

app.MapGet("/brands", async (BrandDB db) => await db.Brands.ToListAsync());
app.MapPost("/brand", async (BrandDB db, Brand brand) =>
{
    await db.Brands.AddAsync(brand);
    await db.SaveChangesAsync();
    return Results.Created($"/brand/{brand.Id}", brand);
});
app.MapGet("/brand/{id}", async (BrandDB db, int id) => await db.Brands.FindAsync(id));
app.MapPut("/brand/{id}", async (BrandDB db, Brand updatebrand, int id) =>
{
    var brand = await db.Brands.FindAsync(id);
    if (brand is null) return Results.NotFound();
    brand.Name = updatebrand.Name;
    brand.Description = updatebrand.Description;
    brand.IsStocked = updatebrand.IsStocked;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/brand/{id}", async (BrandDB db, int id) =>
{
    var brand = await db.Brands.FindAsync(id);
    if (brand is null)
    {
        return Results.NotFound();
    }
    db.Brands.Remove(brand);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
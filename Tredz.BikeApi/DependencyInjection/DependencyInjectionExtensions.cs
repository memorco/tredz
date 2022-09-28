namespace Tredz.BikeApi.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void RegisterCloudStorageDependencies(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<ICreateBlobServiceClientService>(new CreateBlobServiceClientService(connectionString));

        services.AddScoped<IDownloadFileService, DownloadFileService>();
        services.AddScoped<IAllowedMimeTypesService, AllowedMimeTypesService>();
        services.AddScoped<IStorageDirectoryMappingService, StorageDirectoryMappingService>();
        services.AddScoped<IStorageDirectoryStructureService, StorageDirectoryStructureService>();
    }

    public static void RegisterSqlDatabaseDependencies(this IServiceCollection services, string dbConnectionString)
    {
        services.AddSingleton<ISqlRepository>(_ => new SqlRepository(dbConnectionString));
    }

    public static void RegisterTredzMicroService(this IServiceCollection services)
    {
        services.AddScoped<IBikeService, BikeService>();
    }
}

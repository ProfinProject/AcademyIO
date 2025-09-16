using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AcademyIO.WebAPI.Core.DatabaseFlavor;

public class ProviderConfiguration
{
    private static readonly string MigrationAssembly =
        typeof(ProviderConfiguration).GetTypeInfo().Assembly.GetName().Name;

    private readonly string _connectionString;

    private readonly string _migrationAssembly;

    public ProviderConfiguration(string connString, string migrationAssembly = null)
    {
        _connectionString = connString;
        _migrationAssembly = migrationAssembly ?? typeof(ProviderConfiguration).GetTypeInfo().Assembly.GetName().Name;
    }

    public Action<DbContextOptionsBuilder> SqlServer =>
        options => options.UseSqlServer(_connectionString, sql => sql.MigrationsAssembly(_migrationAssembly));

    public Action<DbContextOptionsBuilder> Sqlite =>
        options => options.UseSqlite(_connectionString, sql => sql.MigrationsAssembly(_migrationAssembly));

    public ProviderConfiguration With()
    {
        return this;
    }

    public static ProviderConfiguration Build(string connString, string migrationAssembly = null)
    {
        return new ProviderConfiguration(connString, migrationAssembly);
    }


    /// <summary>
    ///     it's just a tuple. Returns 2 parameters.
    ///     Trying to improve readability at ConfigureServices
    /// </summary>
    public static (DatabaseType, string) DetectDatabase(IConfiguration configuration)
    {
        return (
            configuration.GetValue("AppSettings:DatabaseType", DatabaseType.None),
            configuration.GetConnectionString("DefaultConnection"));
    }
}
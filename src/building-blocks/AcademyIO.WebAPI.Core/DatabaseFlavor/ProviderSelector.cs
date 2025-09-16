using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static AcademyIO.WebAPI.Core.DatabaseFlavor.ProviderConfiguration;

namespace AcademyIO.WebAPI.Core.DatabaseFlavor;

public static class ProviderSelector
{
    public static IServiceCollection ConfigureProviderForContext<TContext>(
        this IServiceCollection services,
        (DatabaseType, string) options,
        string migrationAssembly = null) where TContext : DbContext
    {
        var (database, connString) = options;
        return database switch
        {
            DatabaseType.SqlServer => services.PersistStore<TContext>(Build(connString, migrationAssembly).With().SqlServer),
            DatabaseType.Sqlite => services.PersistStore<TContext>(Build(connString, migrationAssembly).With().Sqlite),

            _ => throw new ArgumentOutOfRangeException(nameof(database), database, null)
        };
    }

    public static Action<DbContextOptionsBuilder> WithProviderAutoSelection((DatabaseType, string) options)
    {
        var (database, connString) = options;
        return database switch
        {
            DatabaseType.SqlServer => Build(connString).With().SqlServer,
            DatabaseType.Sqlite => Build(connString).With().Sqlite,

            _ => throw new ArgumentOutOfRangeException(nameof(database), database, null)
        };
    }
}
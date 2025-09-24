using AcademyIO.Core.Data;
using AcademyIO.Core.DomainObjects;
using AcademyIO.Payments.API.Business;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AcademyIO.Core.Extensions;
using AcademyIO.Core.Messages;


namespace AcademyIO.Payments.API.Data;

public class PaymentsContext(DbContextOptions<PaymentsContext> options, IMediator mediator) : DbContext(options), IUnitOfWork
{
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.Ignore<Event>();
    }

    public async Task<bool> Commit()
    {
        var sucesso = await SaveChangesAsync() > 0;

        if (sucesso)
            await mediator.PublishDomainEvents(this);

        return sucesso;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entityEntry in ChangeTracker.Entries<Entity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
            }
            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property("CreatedDate").IsModified = false;
            }
            if (entityEntry.State == EntityState.Deleted)
            {
                entityEntry.State = EntityState.Modified;
                entityEntry.Property("CreatedDate").IsModified = false;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CardName)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(c => c.CardNumber)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(c => c.CardExpirationDate)
            .IsRequired()
            .HasColumnType("varchar(10)");

        builder.Property(c => c.CardCVV)
            .IsRequired()
            .HasColumnType("varchar(4)");
        // 1 : 1
        builder.HasOne(c => c.Transaction)
            .WithOne(c => c.Payment);

        builder.ToTable("Payments");
    }
}

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(c => c.Id);

        builder.ToTable("Transactions");
    }
}

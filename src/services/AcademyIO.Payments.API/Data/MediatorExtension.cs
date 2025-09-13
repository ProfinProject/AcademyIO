using AcademyIO.Core.DomainObjects;
using MediatR;

namespace AcademyIO.Payments.API.Data;

//TO DO ver se eu nao consigo criar so um publishDomainEvents pra reutilizar ja que todos usam a mesma logica
public static class MediatorExtension
{
    public static async Task PublishDomainEvents(this IMediator mediator, PaymentsContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.CleanEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await mediator.Publish(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}
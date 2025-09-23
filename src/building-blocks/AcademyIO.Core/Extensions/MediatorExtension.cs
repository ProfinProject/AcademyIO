using MediatR;
using Microsoft.EntityFrameworkCore;
using AcademyIO.Core.DomainObjects;

namespace AcademyIO.Core.Extensions;

public static class MediatorExtension
{
    public static async Task PublishDomainEvents(this IMediator mediator, DbContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.Notifications).ToList();

        domainEntities.ForEach(entity => entity.CleanEvents());

        var tasks = domainEvents.Select(async (domainEvent) =>
        {
            await mediator.Publish(domainEvent);
        });

        await Task.WhenAll(tasks);
    }
}
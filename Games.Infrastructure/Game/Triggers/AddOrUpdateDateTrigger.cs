using EntityFrameworkCore.Triggered;
using Games.Domain.Models;

namespace Games.Infrastructure.Game.Triggers;

public class AddOrUpdateDateTrigger : IBeforeSaveTrigger<Domain.Models.Game>, IBeforeSaveTrigger<Genre>
{
    public Task BeforeSave(ITriggerContext<Domain.Models.Game> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType == ChangeType.Modified || context.ChangeType == ChangeType.Added)
        {
            context.Entity.UpdatedAt = DateTime.Now;

            if (context.ChangeType == ChangeType.Added) context.Entity.CreatedAt = DateTime.Now;
        }


        return Task.CompletedTask;
    }

    public Task BeforeSave(ITriggerContext<Genre> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType == ChangeType.Modified || context.ChangeType == ChangeType.Added)
        {
            context.Entity.UpdatedAt = DateTime.Now;

            if (context.ChangeType == ChangeType.Added) context.Entity.CreatedAt = DateTime.Now;
        }

        return Task.CompletedTask;
    }
}
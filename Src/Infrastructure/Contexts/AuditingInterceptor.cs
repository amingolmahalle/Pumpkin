using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pumpkin.Domain.Framework.Extensions;

namespace Pumpkin.Infrastructure.Contexts;

public class AuditingInterceptor : ISaveChangesInterceptor
{
    private readonly IHttpContextAccessor _accessor;

    public AuditingInterceptor(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    #region SavingChanges

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
     //   SetAudits(eventData.Context);
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
      //  SetAudits(eventData.Context);
        return ValueTask.FromResult(result);
    }

    #endregion

    #region SavedChanges

    public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        => result;

    public ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(result);

    #endregion

    #region SaveChangesFailed

    public void SaveChangesFailed(DbContextErrorEventData eventData)
    {
    }

    public async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
        => await Task.CompletedTask;

    #endregion

    #region SetAudits

    // private void SetAudits(DbContext context)
    // {
    //     Guid currenUserId;
    //     try
    //     {
    //         var authTicket = _accessor?.HttpContext?.Items[AuthenticationTicket<object>.SessionKey] as AuthenticationTicket<object>
    //                          ?? AuthenticationTicket<object>.Default;
    //         currenUserId = authTicket.Identity switch
    //         {
    //             UserIdentity ui => ui.UserId,
    //             _ => Guid.Parse("857afe44-b4fd-4b70-8383-f3b83c0f35e4")
    //         };
    //     }
    //     catch (Exception e)
    //     {
    //         currenUserId = Guid.Parse("857afe44-b4fd-4b70-8383-f3b83c0f35e4");
    //     }
    //
    //     context.ChangeTracker.DetectChanges();
    //     var changedAt = DateTime.UtcNow;
    //
    //     foreach (var entry in context.ChangeTracker.Entries())
    //     {
    //         CleanTextValue(entry);
    //         switch (entry.State)
    //         {
    //             case EntityState.Modified:
    //                 if (entry.Entity is IModifiableEntity mme)
    //                 {
    //                     mme.ModifiedAt = changedAt;
    //                     mme.ModifiedBy = currenUserId;
    //                 }
    //                 if (entry.Entity is IRemovableEntity {RemovedAt: { }} dre)
    //                 {
    //                     dre.RemovedAt = DateTime.UtcNow;
    //                     dre.RemovedBy = currenUserId;
    //                 }
    //
    //                 break;
    //             case EntityState.Added:
    //                 if (entry.Entity is ICreatableEntity ace)
    //                 {
    //                     ace.CreatedAt = changedAt;
    //                     ace.CreatedBy = currenUserId;
    //                 }
    //
    //                 if (entry.Entity is IModifiableEntity ame)
    //                 {
    //                     ame.ModifiedAt = changedAt;
    //                     ame.ModifiedBy = currenUserId;
    //                 }
    //
    //                 break;
    //             case EntityState.Detached:
    //             case EntityState.Unchanged:
    //             default:
    //                 continue;
    //         }
    //     }
  //  }

    private void CleanTextValue(EntityEntry entry)
    {
        var properties = entry.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

        foreach (var property in properties)
        {
            var val = (string) property.GetValue(entry.Entity, null);
            if (string.IsNullOrWhiteSpace(val)) 
                continue;
            
            var newVal = val.FixPersianChars().ToEnNumeric();
            if (newVal.Equals(val))
                continue;

            property.SetValue(entry.Entity, newVal, null);
        }
    }

    #endregion
}
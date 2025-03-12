using App.Application;
using App.Application.Contracts.Persistence;
using App.Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Api.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class NotFoundFilter<TEntity, TId>(IGenericRepository<TEntity, TId> genericRepository) : Attribute, IAsyncActionFilter
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idObject = context.ActionArguments.TryGetValue("id", out var idAsObject) ? idAsObject : null;
        if (idAsObject is not TId id)
        {
            await next();
            return;
        }

        if (await genericRepository.AnyAsync(id))
        {
            await next();
            return;
        }

        var entityName = typeof(TEntity).Name;
        var actionName = context.ActionDescriptor.RouteValues["action"];
        var result = ServiceResult.Failure($"Entity with id {id} was not found.({entityName} | {actionName})");
        context.Result = new NotFoundObjectResult(result);
    }
}
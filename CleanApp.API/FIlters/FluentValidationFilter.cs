﻿using CleanApp.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanApp.API.FIlters
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState

                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var resultModel = ServiceResult.Fail(errors);

                context.Result = new BadRequestObjectResult(resultModel);
                return;
            }
            await next();
        }
    }
}

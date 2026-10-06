using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace CheckMate.API.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    private readonly ApiBehaviorOptions _apiBehaviorOptions;

    public ValidationFilter(IOptions<ApiBehaviorOptions> options)
    {
        _apiBehaviorOptions = options.Value;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        // Declared parameters of the action method.
        foreach (var parameter in context.ActionDescriptor.Parameters)
        {
            // If the value of the parameter is null, we skip validation for that parameter.
            if (!context.ActionArguments.TryGetValue(
                    parameter.Name, out var argument)
                || argument is null)
            {
                continue;
            }

            // Construct the type of the validator for the parameter.
            var validatorType = typeof(IValidator<>)
                .MakeGenericType(parameter.ParameterType);

            var validator = context.HttpContext.RequestServices
                .GetService(validatorType) as IValidator;

            // All parameters are optional
            if (validator is null)
            {
                continue;
            }

            var validationContext =
                new ValidationContext<object>(argument);

            var result = await validator.ValidateAsync(
                validationContext,
                context.HttpContext.RequestAborted);

            foreach (var error in result.Errors)
            {
                context.ModelState.AddModelError(
                    error.PropertyName,
                    error.ErrorMessage);
            }
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = _apiBehaviorOptions
                .InvalidModelStateResponseFactory(context);

            return;
        }

        // No issues with the model state, so we can proceed to the next action filter or action method.
        await next();
    }
}
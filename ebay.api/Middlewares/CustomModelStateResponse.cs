
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public static class CustomModelStateResponse
{
    public static IActionResult HandleCustomModelStateResponse(ActionContext context)
    {
        var modelType = context.ActionDescriptor.Parameters
            .FirstOrDefault(p => p.BindingInfo?.BindingSource == BindingSource.Body)
            ?.ParameterType;

        var jsonNameMap = modelType?
            .GetProperties()
            .ToDictionary(
                prop => prop.Name,
                prop => prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ??
                        char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1)
            ) ?? new Dictionary<string, string>();

        var errors = context.ModelState
            .Where(e => e.Value!.Errors.Count > 0)
            .ToDictionary(
                kvp => jsonNameMap.ContainsKey(kvp.Key) ? jsonNameMap[kvp.Key] : kvp.Key,
                kvp => kvp.Value!.Errors.First().ErrorMessage
            );

        var response = new ResponseService<object>(
            statusCode: StatusCodes.Status422UnprocessableEntity,
            message: CommonMessages.ERROR,
            data: errors
        );

        return new UnprocessableEntityObjectResult(response);
    }
}
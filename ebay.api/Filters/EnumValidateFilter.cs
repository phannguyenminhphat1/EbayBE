using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


[AttributeUsage(AttributeTargets.Method)]
public class EnumValidateFilter<TEnum> : ActionFilterAttribute
    where TEnum : Enum
{
    private readonly string _key;

    public EnumValidateFilter(string key)
    {
        _key = key;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments)
        {
            // 1️⃣ Case: truyền trực tiếp ?status=xxx (rare nhưng hỗ trợ)
            if (argument.Key.Equals(_key, StringComparison.OrdinalIgnoreCase))
            {
                ValidateEnum(argument.Value?.ToString(), context);
                return;
            }

            // 2️⃣ Case: status nằm trong DTO (Query hoặc Body)
            var value = GetPropertyValue(argument.Value, _key);
            if (value != null)
            {
                ValidateEnum(value, context);
                return;
            }
        }

        base.OnActionExecuting(context);
    }

    private static string? GetPropertyValue(object? obj, string propertyName)
    {
        System.Console.WriteLine(obj);
        System.Console.WriteLine(propertyName);
        if (obj == null) return null;

        var prop = obj.GetType().GetProperty(
            propertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
        );

        return prop?.GetValue(obj)?.ToString();
    }

    private static void ValidateEnum(string? rawValue, ActionExecutingContext context)
    {
        System.Console.WriteLine("Vào đây không", rawValue);
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            System.Console.WriteLine("Vào đây không nè ?");

            context.Result = new BadRequestObjectResult(new
            {
                status = 400,
                message = "Status is required"
            });
            return;
        }

        if (!Enum.TryParse(typeof(TEnum), rawValue, true, out _))
        {
            System.Console.WriteLine("Vào đây không nè 2 ?");

            context.Result = new BadRequestObjectResult(new
            {
                status = 400,
                message = $"Status is invalid, status must be: {Enum.GetNames(typeof(TEnum))}",
            });
        }
    }
}

using System.Net;
public class ValidateAndParseRole
{
    public static bool TryParseRole<T>(
        string? roleString, out int? parsedRole, out ResponseService<T>? errorResponse, bool isFromBody = false
    )
    {
        parsedRole = null;
        errorResponse = null;
        if (roleString == null)
        {
            // Cho phép không truyền role
            return true;
        }
        if (!int.TryParse(roleString, out var role))
        {
            if (isFromBody)
            {
                errorResponse = new ResponseService<T>(
                    statusCode: isFromBody ? (int)HttpStatusCode.UnprocessableEntity : (int)HttpStatusCode.BadRequest,
                    message: UserMessages.ROLE_MUST_BE_A_NUMBER,
                    data: (T?)(object)new Dictionary<string, string> {
                        { "role_id", UserMessages.ROLE_MUST_BE_A_NUMBER }
                    }
                );
            }
            else
            {
                errorResponse = new ResponseService<T>(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    message: UserMessages.ROLE_MUST_BE_A_NUMBER
                );
            }

            return false;
        }
        parsedRole = role;
        return true;
    }
}

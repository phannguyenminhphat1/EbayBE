using System.Net;

public class ValidateAndParseSpecialty
{
    public static bool TryParseSpecialty<T>(
        string? specialty,
        out int? parsedSpecialty,
        out ResponseService<T>? errorResponse,
        bool isFromBody = false
    )
    {
        parsedSpecialty = null;
        errorResponse = null;

        if (specialty == null) return true;

        // Validate specialty
        if (!string.IsNullOrEmpty(specialty))
        {
            if (!int.TryParse(specialty, out var outSpecialty))
            {
                if (isFromBody)
                {
                    errorResponse = new ResponseService<T>(
                        statusCode: (int)HttpStatusCode.UnprocessableEntity,
                        message: UserMessages.ERROR,
                        data: (T?)(object)new Dictionary<string, string> {
                            { "specialty_id", UserMessages.SPECIALTY_MUST_BE_A_NUMBER }
                        }
                    );
                }
                else
                {
                    errorResponse = new ResponseService<T>(
                        statusCode: (int)HttpStatusCode.BadRequest,
                        message: UserMessages.SPECIALTY_MUST_BE_A_NUMBER
                    );
                }
                return false;
            }
            parsedSpecialty = outSpecialty;
        }
        return true;
    }
}

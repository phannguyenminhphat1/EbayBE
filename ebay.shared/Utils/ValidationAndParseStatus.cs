using System.Net;
public class ValidateAndParseStatus
{
    public static bool TryParseStatus<T>(
        string? statusIdString, out int? parsedStatusId, out ResponseService<T>? errorResponse, bool isFromBody = false
    )
    {
        parsedStatusId = null;
        errorResponse = null;
        if (statusIdString == null)
        {
            errorResponse = new ResponseService<T>(
                    statusCode: isFromBody ? (int)HttpStatusCode.UnprocessableEntity : (int)HttpStatusCode.BadRequest,
                    message: AppointmentMessages.ERROR,
                    data: (T?)(object)new Dictionary<string, string> {
                        { "status_id", AppointmentMessages.STATUS_IS_REQUIRED }
                    }
                );
            return false;
        }
        if (!int.TryParse(statusIdString, out var statusId))
        {
            if (isFromBody)
            {
                errorResponse = new ResponseService<T>(
                    statusCode: isFromBody ? (int)HttpStatusCode.UnprocessableEntity : (int)HttpStatusCode.BadRequest,
                    message: AppointmentMessages.ERROR,
                    data: (T?)(object)new Dictionary<string, string> {
                        { "status_id", AppointmentMessages.STATUS_MUST_BE_A_NUMBER }
                    }
                );
            }
            else
            {
                errorResponse = new ResponseService<T>(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    message: AppointmentMessages.STATUS_MUST_BE_A_NUMBER
                );
            }

            return false;
        }
        parsedStatusId = statusId;
        return true;
    }
}

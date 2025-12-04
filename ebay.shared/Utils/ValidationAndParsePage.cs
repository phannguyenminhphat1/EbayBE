using System.Net;
public class ValidateAndParsePagination
{
    public static bool TryParsePagination<T>(
        string? page,
        string? pageSize,
        out int? parsedPage,
        out int? parsedPageSize,
        out ResponseService<T>? errorResponse
    )
    {
        parsedPage = null;
        parsedPageSize = null;
        errorResponse = null;

        if (page == null && pageSize == null) return true;

        // Validate Page
        if (!string.IsNullOrEmpty(page))
        {
            if (!int.TryParse(page, out var outPage))
            {
                errorResponse = new ResponseService<T>(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    message: CommonMessages.PAGE_MUST_BE_A_NUMBER
                );
                return false;
            }

            if (outPage < 1)
            {
                errorResponse = new ResponseService<T>(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    message: CommonMessages.PAGE_NUMBER_MUST_BE_GREATER_THAN_0
                );
                return false;
            }

            parsedPage = outPage;
        }

        // Validate PageSize
        if (!string.IsNullOrEmpty(pageSize))
        {
            if (!int.TryParse(pageSize, out var outPageSize))
            {
                errorResponse = new ResponseService<T>(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    message: CommonMessages.PAGE_SIZE_MUST_BE_A_NUMBER
                );
                return false;
            }

            if (outPageSize < 1)
            {
                errorResponse = new ResponseService<T>(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    message: CommonMessages.PAGE_SIZE_MUST_BE_GREATER_THAN_0
                );
                return false;
            }

            parsedPageSize = outPageSize;
        }

        return true;
    }
}

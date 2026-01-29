using SharedKernel;

namespace Web.Api.Infrastructure;

public static class CustomResults
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.Type),
            statusCode: GetStatusCode(result.Error.Type),
            extensions: GetErrors(result));

        static string GetTitle(Error error) =>
            error.Type switch
            {
                ErrorType.Validation => error.Code,
                ErrorType.Problem => error.Code,
                ErrorType.NotFound => error.Code,
                ErrorType.Conflict => error.Code,
                ErrorType.Failure => error.Code,
                _ => "Server failure"
            };

        static string GetDetail(Error error) =>
            error.Type switch
            {
                ErrorType.Validation => error.Description,
                ErrorType.Problem => error.Description,
                ErrorType.NotFound => error.Description,
                ErrorType.Conflict => error.Description,
                ErrorType.Failure => error.Description,
                _ => "An unexpected error occurred"
            };

        static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                ErrorType.Failure => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

        static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation or ErrorType.Failure or ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

        static Dictionary<string, object?>? GetErrors(Result result)
        {
            // Case 1: ValidationError – already a collection of errors
            if (result.Error is ValidationError validationError)
            {
                return new Dictionary<string, object?>
                {
                    ["errors"] = validationError.Errors
                };
            }

            // Case 2: Problem / Failure – expose a single error in an array
            if (result.Error.Type is ErrorType.Problem or ErrorType.Failure)
            {
                return new Dictionary<string, object?>
                {
                    ["errors"] = new[] { result.Error }
                };
            }

            // Case 3: Other types (NotFound, Conflict, etc.) – no extra 'errors' section
            return null;
        }
    }

    internal static IResult Problem(string detail, int statusCode = StatusCodes.Status400BadRequest)
    {
        return Results.Problem(
            detail: detail,
            statusCode: statusCode,
            title: "Server failure",
            type: "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        );

    }
}

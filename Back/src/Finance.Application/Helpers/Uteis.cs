namespace Finance.Application.Helpers;

public static class Uteis
{
    public static object CreateObjectExceptionResponse(this ExceptionServiceBadRequestError error) =>
        new { title = error.Message };

    public static object CreateResponseValueBoolean(this bool variable, string response_true = "Success", string response_error = "Error") =>
        new { title = variable ? response_true : response_error };
}
namespace Finance.Application.Helpers;

public class ExceptionServiceBadRequestError : Exception
{
    public ExceptionServiceBadRequestError(string error) : base(error) { }
}
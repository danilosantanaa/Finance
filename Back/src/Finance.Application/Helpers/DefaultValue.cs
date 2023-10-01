namespace Finance.Application.Helpers;

public static class DefaultValue
{
    public static void SetDateNowDefault(this DateTime date, DateTime dateModel = default) =>
        date = dateModel != default ? dateModel : DateTime.Now;
}
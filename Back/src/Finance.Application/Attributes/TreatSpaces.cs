using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Finance.Application.Attributes;

public class TreatSpaces : ValidationAttribute
{
    public bool WithoutSpace { get; set; } = false;
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {

        var prop = validationContext.ObjectType.GetProperty(validationContext.MemberName);

        var oldValue = prop.GetValue(validationContext.ObjectInstance) as string;

        if (oldValue != null)
        {
            prop.SetValue(validationContext.ObjectInstance, Regex.Replace(oldValue.Trim(), @"\s\s+", WithoutSpace ? "" : " "));
        }
        return null;
    }
}
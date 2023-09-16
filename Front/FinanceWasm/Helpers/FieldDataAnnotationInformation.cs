using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FinanceWasm.Helpers {
    public static class FieldDataAnnotationInformation {
        public static bool IsRequired(this FieldIdentifier fieldIdentifier)
        {
            var attributes = fieldIdentifier.Model.GetType().GetProperty(fieldIdentifier.FieldName).GetCustomAttributes(false);

            return attributes.Any() && attributes.Any(a => a.GetType() == typeof(RequiredAttribute));
        }

        public static bool IsDisplayName(this FieldIdentifier fieldIdentifier)
        {
            var attributes = fieldIdentifier.Model.GetType().GetProperty(fieldIdentifier.FieldName).GetCustomAttributes(false);

            return attributes.Any() && attributes.Any(a => a.GetType() == typeof(DisplayAttribute));
        }

        public static string GetDisplayName(this FieldIdentifier fieldIdentifier)
        {
            var attributes = fieldIdentifier.Model.GetType().GetProperty(fieldIdentifier.FieldName).GetCustomAttributes(false);

            if (!fieldIdentifier.IsDisplayName())
                return fieldIdentifier.FieldName;

            DisplayAttribute displayAttribute = (DisplayAttribute) attributes.FirstOrDefault(a => a.GetType() == typeof(DisplayAttribute));

            return displayAttribute.Name;
        }
    }
}

using Microsoft.AspNetCore.Components.Forms;

namespace FinanceWasm.Components.Form {
    public class FieldCssClassProviderCustom : FieldCssClassProvider {
        public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
        {
            var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();

            return isValid ? "valid" : "is-invalid";
        }
    }
}

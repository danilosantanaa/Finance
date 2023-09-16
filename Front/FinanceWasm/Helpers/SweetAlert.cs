using Microsoft.JSInterop;

namespace FinanceWasm.Helpers
{
    public enum SweetAlertType { Question, Info, Success, Warning, Error };

    public class SweetAlert
    {
        private readonly IJSRuntime _jSRuntime;

        public SweetAlert(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task ToastAsync(SweetAlertType type, string title, int timer = 2000)
        {
            await _jSRuntime.InvokeVoidAsync("ToastAlert", title, type, timer);
        }

        public async Task AlertAsync(SweetAlertType type, string text, string? title = null)
        {
            await _jSRuntime.InvokeVoidAsync("Alert", type, text, title ?? GetTitleDefault()[type]);
        }

        public async Task<bool> AlertConfirmationAsync(string content, string? title = null, string textConfirm = "Sim", string textCancel = "Não")
        {
            return await _jSRuntime.InvokeAsync<bool>(
                "AlertConfirmation",
                content,
                title ?? GetTitleDefault()[SweetAlertType.Warning],
                textConfirm,
                textCancel
            );
        }

        #region VALIDATIONS
        Dictionary<SweetAlertType, string> GetTitleDefault() => new Dictionary<SweetAlertType, string> {
            {SweetAlertType.Question, "Ajuda" },
            {SweetAlertType.Info, "Informações" },
            {SweetAlertType.Success, "Sucesso" },
            {SweetAlertType.Warning, "Aviso" },
            {SweetAlertType.Error, "Erro" }
        };
        #endregion
    }
}

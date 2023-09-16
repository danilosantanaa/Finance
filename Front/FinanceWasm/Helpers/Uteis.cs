using System.Net.Http.Json;
using System.Globalization;

namespace FinanceWasm.Helpers {
    public static class Uteis {
        public static bool IsNew(this int Id) => Id == 0;
        public static T GetItem<T>(this HttpContent content) => content.ReadFromJsonAsync<T>().Result;
        public static async Task ThrowResponseError(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) {
                    var responseContent = await response.Content.ReadFromJsonAsync<ErrorResponse>();

                    throw new Exception(responseContent.Title);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new Exception("Error 404 - Not Found");
                }

                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public static string EnabledOrDisabled(this bool variable) => variable ? "Ativo" : "Inativo";

        public static string CurrecyFormat(this double variable, string country = "pt-br")
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(country);

            return $"{variable.ToString("C2")}";
        }

        /// <param name="variable"></param>
        /// <param name="value_default">Valor que será mostrado por padrão na tela, default "-"</param>
        /// <param name="mask">Mascara que será usada para mostrar a data e hora de forma personalizada.</param>
        /// <returns></returns>
        public static string FormatDateTime(this DateTime variable, string value_default = "-", string mask = null) => 
            variable == DateTime.MinValue ? value_default : variable.ToString(mask);

        public static string FormatStringEmptyOrNull(this string value, string def = "-") =>
            value = string.IsNullOrEmpty(value) ? def : value;

    }
}

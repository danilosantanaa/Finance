using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Finance.Application.Helpers
{
    public static class PostgresSQLCustomError
    {
        public static string DbAddOrUpdateExceptionErroTreated(this DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException.GetType() == typeof(PostgresException))
            {
                var postgresException = (PostgresException)dbUpdateException.InnerException;
                var code = postgresException.SqlState;

                if (code == PostgresErrorCodes.ForeignKeyViolation) return "Alguns campos que depende de outro registros devem ser fornecidos. Verifique os campos IDs que são obrigatório e forneça o IDs.";
            }

            return dbUpdateException.Message;
        }
    }
}
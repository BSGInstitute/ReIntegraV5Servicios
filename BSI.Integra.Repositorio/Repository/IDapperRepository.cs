namespace BSI.Integra.Repositorio.Repository
{
    public interface IDapperRepository
    {
        string QueryDapper(string sql, object? parametros);
        string QueryDapper(string sql, object? parametros, int timeoutMinutos);
        Task<string> QueryDapperAsync(string sql, object? parametros);
        Task<string> QueryDapperAsync(string sql, object? parametros, int timeoutMinutos);
        string FirstOrDefault(string sql, object? parametros);
        string FirstOrDefault(string sql, object? parametros, int timeoutMinutos);
        Task<string> FirstOrDefaultAsync(string sql, object? parametros);
        Task<string> FirstOrDefaultAsync(string sql, object? parametros, int timeoutMinutos);
        string QuerySPDapper(string sql, object? parametros);
        string QuerySPDapper(string sql, object? parametros, int timeoutMinutos);
        Task<string> QuerySPDapperAsync(string sql, object? parametros);
        Task<string> QuerySPDapperAsync(string sql, object? parametros, int timeoutMinutos);
        string QuerySPFirstOrDefault(string sql, object? parametros);
        string QuerySPFirstOrDefault(string sql, object? parametros, int timeoutMinutos);
        Task<string> QuerySPFirstOrDefaultAsync(string sql, object? parametros);
        Task<string> QuerySPFirstOrDefaultAsync(string sql, object? parametros, int timeoutMinutos);
    }
}

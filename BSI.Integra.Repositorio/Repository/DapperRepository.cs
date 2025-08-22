using BSI.Integra.Persistencia.Infrastructure;
using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace BSI.Integra.Repositorio.Repository
{
    public class DapperRepository : IDapperRepository
    {
        private int _timeout = 20 * 60;
        protected internal readonly IConnectionFactory _connectionFactory;

        public DapperRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public string QueryDapper(string sql, object? parametros)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var rpta = SqlMapper.Query<dynamic>(conn, sql, param: parametros, commandType: CommandType.Text, commandTimeout: _timeout).ToList();

                    var jsonResultado = JsonConvert.SerializeObject(rpta);
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> QueryDapperAsync(string sql, object? parametros)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var rpta = await SqlMapper.QueryAsync<dynamic>(conn, sql, param: parametros, commandType: CommandType.Text, commandTimeout: _timeout);

                    var jsonResultado = JsonConvert.SerializeObject(rpta.ToList());
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string QueryDapper(string sql, object? parametros, int timeoutMinutos)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
                    var rpta = SqlMapper.Query<dynamic>(conn, sql, param: parametros, commandType: CommandType.Text, commandTimeout: timeout).ToList();

                    var jsonResultado = JsonConvert.SerializeObject(rpta);
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> QueryDapperAsync(string sql, object? parametros, int timeoutMinutos)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
                    var rpta = await SqlMapper.QueryAsync<dynamic>(conn, sql, param: parametros, commandType: CommandType.Text, commandTimeout: timeout);

                    var jsonResultado = JsonConvert.SerializeObject(rpta.ToList());
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string FirstOrDefault(string sql, object? parametros)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var rpta = SqlMapper.Query<dynamic>(conn, sql, param: parametros, commandType: CommandType.Text, commandTimeout: _timeout).FirstOrDefault();

                    var jsonResultado = JsonConvert.SerializeObject(rpta);
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> FirstOrDefaultAsync(string sql, object? parametros)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var rpta = await SqlMapper.QueryAsync<dynamic>(conn, sql, param: parametros, commandType: CommandType.Text, commandTimeout: _timeout);

                    var jsonResultado = JsonConvert.SerializeObject(rpta.FirstOrDefault());
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FirstOrDefault(string sql, object? parametros, int timeoutMinutos)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
                    var rpta = SqlMapper.Query<dynamic>(conn, sql, param: parametros, commandType: CommandType.Text, commandTimeout: timeout).FirstOrDefault();

                    var jsonResultado = JsonConvert.SerializeObject(rpta);
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> FirstOrDefaultAsync(string sql, object? parametros, int timeoutMinutos)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
                    var rpta = await SqlMapper.QueryAsync<dynamic>(conn, sql, param: parametros, commandType: CommandType.Text, commandTimeout: timeout);

                    var jsonResultado = JsonConvert.SerializeObject(rpta.FirstOrDefault());
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string QuerySPDapper(string sql, object? parametros)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var rpta = SqlMapper.Query<dynamic>(conn, sql, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout).ToList();

                    var jsonResultado = JsonConvert.SerializeObject(rpta);
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> QuerySPDapperAsync(string sql, object? parametros)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var rpta = await SqlMapper.QueryAsync<dynamic>(conn, sql, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout);

                    var jsonResultado = JsonConvert.SerializeObject(rpta.ToList());
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string QuerySPDapper(string sql, object? parametros, int timeoutMinutos)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
                    var rpta = SqlMapper.Query<dynamic>(conn, sql, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: timeout).ToList();

                    var jsonResultado = JsonConvert.SerializeObject(rpta);
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> QuerySPDapperAsync(string sql, object? parametros, int timeoutMinutos)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
                    var rpta = await SqlMapper.QueryAsync<dynamic>(conn, sql, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: timeout);

                    var jsonResultado = JsonConvert.SerializeObject(rpta.ToList());
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string QuerySPFirstOrDefault(string sql, object? parametros)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var rpta = SqlMapper.Query<dynamic>(conn, sql, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout).FirstOrDefault();

                    var jsonResultado = JsonConvert.SerializeObject(rpta);
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> QuerySPFirstOrDefaultAsync(string sql, object? parametros)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var rpta = await SqlMapper.QueryAsync<dynamic>(conn, sql, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout);

                    var jsonResultado = JsonConvert.SerializeObject(rpta.FirstOrDefault());
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string QuerySPFirstOrDefault(string sql, object? parametros, int timeoutMinutos)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
                    var rpta = SqlMapper.Query<dynamic>(conn, sql, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: timeout).FirstOrDefault();

                    var jsonResultado = JsonConvert.SerializeObject(rpta);
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> QuerySPFirstOrDefaultAsync(string sql, object? parametros, int timeoutMinutos)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
                    var rpta = await SqlMapper.QueryAsync<dynamic>(conn, sql, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: timeout);

                    var jsonResultado = JsonConvert.SerializeObject(rpta.FirstOrDefault());
                    return jsonResultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int ConvertirTimeOutSegundos(int timeoutMinutos)
        {
            return timeoutMinutos * 60;
        }
    }
}

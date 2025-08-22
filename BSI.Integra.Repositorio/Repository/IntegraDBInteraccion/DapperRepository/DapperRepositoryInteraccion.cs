using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.DapperRepository;
using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace SI.Integra.Repositorio.Repository.IntegraDBInteraccion.DapperRepository
{
    public class DapperRepositoryInteraccion : IDapperRepositoryInteraccion
    {
        private int _timeout = 20 * 60;
        protected internal readonly IConnectionFactoryInteraccion _connectionFactoryInteraccion;
        public DapperRepositoryInteraccion(IConnectionFactoryInteraccion connectionFactoryInteraccion)
        {
            _connectionFactoryInteraccion = connectionFactoryInteraccion;
        }



        public string QueryDapper(string sql, object? parametros)
        {
            try
            {
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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
                using (var conn = _connectionFactoryInteraccion.GetConnection)
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

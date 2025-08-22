using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace BSI.Integra.Persistencia.Infrastructure
{
    public class ConnectionFactoryInteraccion : IConnectionFactoryInteraccion
    {
        private string _connectionString;

        public ConnectionFactoryInteraccion(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection GetConnection
        {
            get
            {
                try
                {
                    DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
                    var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");

                    var connection = factory.CreateConnection();
                    if (connection != null)
                    {
                        connection.ConnectionString = _connectionString;
                        connection.Open();
                        return connection;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}

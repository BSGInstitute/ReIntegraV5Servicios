using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace BSI.Integra.Persistencia.Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private string _connectionString;

        public ConnectionFactory(string connectionString)
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

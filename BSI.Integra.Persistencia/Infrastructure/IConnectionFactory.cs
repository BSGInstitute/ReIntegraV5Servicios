using System.Data;

namespace BSI.Integra.Persistencia.Infrastructure
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}


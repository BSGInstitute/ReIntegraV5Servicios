using System.Data;

namespace BSI.Integra.Persistencia.Infrastructure
{
    public interface IConnectionFactoryInteraccion
    {
        IDbConnection GetConnection { get; }
    }
}

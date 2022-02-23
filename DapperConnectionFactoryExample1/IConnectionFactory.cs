using System.Data;

namespace DapperConnectionFactoryExample1
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection Connection { get; }
    }
}

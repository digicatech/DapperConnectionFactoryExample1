using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperConnectionFactoryExample1
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection? _connection;


        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }


        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }




        public IDbConnection CreateConnection(DataAccessProviderTypes dataAccessProviderTypes)
        {
            try
            {
                if (Connection != null)
                {
                    Connection.Close();
                    Connection.Dispose();
                }
            }
            catch (Exception)
            {
                Connection = null;
            }


            if (dataAccessProviderTypes == DataAccessProviderTypes.Oracle)
            {
                Connection =  new OracleConnection(_connectionString);
            }
            else if (dataAccessProviderTypes == DataAccessProviderTypes.PostgreSql)
            {
                Connection =  new NpgsqlConnection(_connectionString);
            }
            else if (dataAccessProviderTypes == DataAccessProviderTypes.SqlServer)
            {
                Connection =  new SqlConnection(_connectionString);
            }
            else if (dataAccessProviderTypes == DataAccessProviderTypes.SqLite)
            {
                Connection =  new SqliteConnection(_connectionString);
            }
            else
            {
                throw new Exception("Unsupporte database provider");
            }

            
            return Connection;
        }



        public void Dispose()
        {
            try
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();

                    }
                    _connection.Dispose();
                }

            }
            catch (Exception)
            {
                _connection = null;
            }
        }
    }
}

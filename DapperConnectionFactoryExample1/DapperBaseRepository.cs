using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DapperConnectionFactoryExample1
{
    public class DapperBaseRepository
    {
        public string ConenctionString { get; set; }
        public DataAccessProviderTypes dataAccessProviderTypes { get; set; } = DataAccessProviderTypes.PostgreSql;

       

        public DapperBaseRepository()
        {
            
        }

        public IEnumerable<T> Query<T>(string sql, object parameters = null)
        {
            try
            {
                using (var connection = new ConnectionFactory(this.ConenctionString).GetConnection(this.dataAccessProviderTypes))
                {
                    connection.Open();
                    var result = connection.Query<T>(sql, parameters);
                    connection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        
        }



        public int DbExecute(string sqlStr, object parameters)
        {
            int result = 0;
            try
            {
                using (var connection = new ConnectionFactory(this.ConenctionString).GetConnection(this.dataAccessProviderTypes))
                {
                    connection.Open();
                    result = connection.Execute(sqlStr, parameters);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }


        /*public T QueryFirst<T>(string query, object parameters = null)
        {
            try
            {
                using (SqlConnection conn
                       = new SqlConnection("Your Connection String"))
                {
                    return conn.QueryFirst<T>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return default; //Or however you want to handle the return
            }
        }

        public T QueryFirstOrDefault<T>(string query, object parameters = null)
        {
            try
            {
                using (SqlConnection conn
                       = new SqlConnection("Your Connection String"))
                {
                    return conn.QueryFirstOrDefault<T>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return default; //Or however you want to handle the return
            }
        }

        public T QuerySingle<T>(string query, object parameters = null)
        {
            try
            {
                using (SqlConnection conn
                       = new SqlConnection("Your Connection String"))
                {
                    return conn.QuerySingle<T>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return default; //Or however you want to handle the return
            }
        }

        public T QuerySingleOrDefault<T>(string query, object parameters = null)
        {
            try
            {
                using (SqlConnection conn
                       = new SqlConnection("Your Connection String"))
                {
                    return conn.QuerySingleOrDefault<T>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return default; //Or however you want to handle the return
            }
        }


        public void Execute(string query, object parameters = null)
        {
            try
            {
                using (SqlConnection conn
                       = new SqlConnection("Your Connection String"))
                {
                    conn.Execute(query, parameters);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
            }
        }
        */

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoContact.Tools.Connections
{
    public static class DbConnectionExtensions
    {
        public static int ExecuteNonQuery(this DbConnection dbConnection, string query, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                if (dbConnection.State is ConnectionState.Closed)
                    dbConnection.Open();

                return dbCommand.ExecuteNonQuery();
            }
        }

        public static async Task<int> ExecuteNonQueryAsync(this DbConnection dbConnection, string query, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                if (dbConnection.State is ConnectionState.Closed)
                    dbConnection.Open();

                return await dbCommand.ExecuteNonQueryAsync();
            }
        }

        public static object? ExecuteScalar(this DbConnection dbConnection, string query, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                if (dbConnection.State is ConnectionState.Closed)
                    dbConnection.Open();

                object? result = dbCommand.ExecuteScalar();
                return result is DBNull ? null : result;
            }
        }

        public async static Task<object?> ExecuteScalarAsync(this DbConnection dbConnection, string query, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                if (dbConnection.State is ConnectionState.Closed)
                    dbConnection.Open();

                object? result = await dbCommand.ExecuteScalarAsync();
                return Task.FromResult(result is DBNull ? null : result);
            }
        }

        public static IEnumerable<TResult> ExecuteReader<TResult>(this DbConnection dbConnection, string query, Func<IDataRecord, TResult> selector, bool immediately, bool isStoredProcedure = false, object? parameters = null)
        {
            IEnumerable<TResult> result = dbConnection.ExecuteReader(query, selector, isStoredProcedure, parameters);
            return (!immediately) ? result : result.ToList();
        }

        public static IEnumerable<TResult> ExecuteReader<TResult>(this DbConnection dbConnection, string query, Func<IDataRecord, TResult> selector, bool isStoredProcedure = false, object? parameters = null)
        {
            ArgumentNullException.ThrowIfNull(selector);

            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                if (dbConnection.State is ConnectionState.Closed)
                    dbConnection.Open();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        yield return selector(dataReader);
                    }
                }
            }
        }

        private static DbCommand CreateCommand(DbConnection dbConnection, string query, bool isStoredProcedure, object? parameters)
        {
            ArgumentNullException.ThrowIfNull(query);

            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = query;

            if (isStoredProcedure)
                dbCommand.CommandType = CommandType.StoredProcedure;

            if (parameters is not null)
            {
                Type type = parameters.GetType();
                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    DbParameter dbParameter = dbCommand.CreateParameter();
                    dbParameter.ParameterName = propertyInfo.Name;
                    dbParameter.Value = propertyInfo.GetValue(parameters) ?? DBNull.Value;
                    dbCommand.Parameters.Add(dbParameter);
                }
            }

            return dbCommand;
        }
    }
}

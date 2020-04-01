using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using Pumpkin.Contract.DataServices;
using Pumpkin.Utils;

namespace Pumpkin.Data.DataServices.DataProviders
{
    public class SqlDataProvider : IDisposable
    {
        // private static ILog _logger;
        private readonly string _connectionString;

        public SqlDataProvider() : this(
            "Data Source=192.168.88.29;Initial Catalog=YourDbName;Persist Security Info=True;User ID=dba;Password=aD@9588K@m31;MultipleActiveResultSets=True;Application Name=CRM")
        {
        }

        public SqlDataProvider(string connectionString)
        {
            _connectionString = connectionString;

            //   if (_logger == null)
            //     _logger = LogManager.GetLogger(typeof(SqlDataProvider));
        }

        public List<T> ExecuteQueryCommand<T>(string command, Dictionary<string, object> parameters = null)
            where T : class
        {
            SqlConnection con = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    var res = con.Query<T>(new CommandDefinition(command, parameters));
                    return res.ToList();
                }
            }
            catch (Exception ex)
            {
                // _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con.Dispose();
                }
                catch (Exception innerEx)
                {
                    //  _logger.Fatal(innerEx.Message, innerEx);
                }
            }
        }

        public async Task<List<T>> ExecuteQueryCommandAsync<T>(string command,
            Dictionary<string, object> parameters = null)
            where T : class
        {
            SqlConnection con = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    var res = await con.QueryAsync<T>(new CommandDefinition(command, parameters));
                    return res.ToList();
                }
            }
            catch (Exception ex)
            {
                //  _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con.Dispose();
                }
                catch (Exception innerEx)
                {
                    // _logger.Fatal(innerEx.Message, innerEx);
                }
            }
        }

        public T ExecuteSingleRecordQueryCommand<T>(string command, Dictionary<string, object> parameters = null)
            where T : class
        {
            SqlConnection con = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    var res = con.QueryFirstOrDefault<T>(new CommandDefinition(command, parameters));
                    return res;
                }
            }
            catch (Exception ex)
            {
                //  _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con.Dispose();
                }
                catch (Exception innerEx)
                {
                    // _logger.Fatal(innerEx.Message, innerEx);
                }
            }
        }

        public async Task<T> ExecuteSingleRecordQueryCommandAsync<T>(string command,
            Dictionary<string, object> parameters = null)
            where T : class
        {
            SqlConnection con = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    var res = await con.QueryFirstOrDefaultAsync<T>(new CommandDefinition(command, parameters));
                    return res;
                }
            }
            catch (Exception ex)
            {
                // _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con.Dispose();
                }
                catch (Exception innerEx)
                {
                    //  _logger.Fatal(innerEx.Message, innerEx);
                }
            }
        }

        public IDataResult ExecuteQueryCommand(string command, Dictionary<string, object> parameters = null)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    using (cmd = new SqlCommand(command, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30000000;
                        if (parameters != null && parameters.Any())
                        {
                            var p = CastParameters(cmd, parameters);
                            cmd.Parameters.AddRange(p);
                        }

                        DataTable dt = new DataTable();
                        
                        int count = 0;
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }

                        if (!reader.IsClosed)
                        {
                            if (reader.Read())
                                count = (int) reader[0];
                        }

                        List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            lst.Add(dataRow.Table.Columns
                                .Cast<DataColumn>()
                                .ToDictionary(c => c.ColumnName, c =>
                                {
                                    if (dataRow[c] is SqlGeometry)
                                    {
                                        var value = ((SqlGeometry) dataRow[c]);
                                        
                                        if (value.ToString() == "Null")
                                            return null;
                                        return new NetTopologySuite.IO.WKTReader().Read(dataRow[c] == null
                                            ? ""
                                            : dataRow[c].ToString());
                                    }

                                    return dataRow[c];
                                }));
                        }

                        return new DataResult(lst, count);
                    }
                }
            }
            catch (Exception ex)
            {
                // _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    // _logger.Fatal(innerEx.Message, ex);
                }
            }
        }

        public async Task<IDataResult> ExecuteQueryCommandAsync(string command,
            Dictionary<string, object> parameters = null)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    using (cmd = new SqlCommand(command, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30000000;
                        if (parameters != null && parameters.Any())
                        {
                            var p = CastParameters(cmd, parameters);
                            cmd.Parameters.AddRange(p);
                        }

                        DataTable dt = new DataTable();
                        int count = 0;
                        con.Open();

                        var reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }

                        if (!reader.IsClosed)
                        {
                            if (reader.Read())
                                count = (int) reader[0];
                        }

                        return new DataResult(dt, count);
                    }
                }
            }
            catch (Exception ex)
            {
                //  _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();
                }
                catch (Exception innerEx)
                {
                    //  _logger.Fatal(innerEx.Message, innerEx);
                }
            }
        }

        public int ExecuteNonQueryCommand(string command, Dictionary<string, object> parameters = null)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    using (cmd = new SqlCommand(command, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30000000;
                        if (parameters != null && parameters.Any())
                        {
                            var p = CastParameters(cmd, parameters);
                            cmd.Parameters.AddRange(p);
                        }

                        con.Open();
                        var res = cmd.ExecuteNonQuery();
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                //  _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
        }

        public async Task<int> ExecuteNonQueryCommandAsync(string command, Dictionary<string, object> parameters = null)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    using (cmd = new SqlCommand(command, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30000000;
                        if (parameters != null && parameters.Any())
                        {
                            var p = CastParameters(cmd, parameters);
                            cmd.Parameters.AddRange(p);
                        }

                        con.Open();
                        var res = await cmd.ExecuteNonQueryAsync();
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                // _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
        }

        public T ExecuteSingleValueQueryCommand<T>(string command, Dictionary<string, object> parameters = null)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    using (cmd = new SqlCommand(command, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30000000;
                        if (parameters != null && parameters.Any())
                        {
                            var p = CastParameters(cmd, parameters);
                            cmd.Parameters.AddRange(p);
                        }

                        con.Open();
                        var res = cmd.ExecuteScalar();
                        return (T) res;
                    }
                }
            }
            catch (Exception ex)
            {
                //  _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
        }

        public async Task<T> ExecuteSingleValueQueryCommandAsync<T>(string command,
            Dictionary<string, object> parameters = null)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    using (cmd = new SqlCommand(command, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30000000;
                        if (parameters != null && parameters.Any())
                        {
                            var p = CastParameters(cmd, parameters);
                            cmd.Parameters.AddRange(p);
                        }

                        con.Open();
                        var res = await cmd.ExecuteScalarAsync();
                        return (T) res;
                    }
                }
            }
            catch (Exception ex)
            {
                // _logger.Fatal(ex.Message, ex);
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
        }

        private SqlParameter[] CastParameters(SqlCommand cmd, Dictionary<string, object> parameters)
        {
            var p = new List<SqlParameter>();
            foreach (var (key, value) in parameters.Where(e => e.Value != null))
            {
                if (value.GetType().IsArray || value.GetType().IsList())
                {
                    var originalType = value.GetType();
                    Type type = null;
                    
                    if (originalType.IsArray)
                    {
                        type = originalType.GetElementType();
                    }
                    else if (originalType.IsList())
                    {
                        type = value.GetType().GetGenericArguments()[0];
                    }

                    if (type == typeof(int))
                    {
                        var x = (value as IEnumerable<int>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(long))
                    {
                        var x = (value as IEnumerable<long>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(short))
                    {
                        var x = (value as IEnumerable<short>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(byte))
                    {
                        var x = (value as IEnumerable<byte>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(double))
                    {
                        var x = (value as IEnumerable<double>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(decimal))
                    {
                        var x = (value as IEnumerable<decimal>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(ushort))
                    {
                        var x = (value as IEnumerable<ushort>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(uint))
                    {
                        var x = (value as IEnumerable<uint>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(ulong))
                    {
                        var x = (value as IEnumerable<ulong>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(string))
                    {
                        var x = (value as IEnumerable<string>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(Guid))
                    {
                        var x = (value as IEnumerable<Guid>);
                        cmd.AddArrayParameters(key, x);
                    }
                    else if (type == typeof(DateTime))
                    {
                        var x = (value as IEnumerable<DateTime>);
                        cmd.AddArrayParameters(key, x);
                    }
                }
                else
                {
                    p.Add(new SqlParameter
                    {
                        Value = value,
                        ParameterName = key
                    });
                }
            }

            return p.ToArray();
        }

        public void Dispose()
        {
        }
    }
}
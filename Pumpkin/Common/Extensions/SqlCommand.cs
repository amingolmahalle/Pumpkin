using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Pumpkin.Common.Extensions
{
    public static partial class Extensions
    {
        /// <param name="cmd">The SqlCommand object to add parameters to.</param>
        /// <param name="paramNameRoot">What the parameter should be named followed by a unique value for each value. This value surrounded by {} in the CommandText will be replaced.</param>
        /// <param name="values">The array of strings that need to be added as parameters.</param>
        /// <param name="dbType">One of the System.Data.SqlDbType values. If null, determines type based on T.</param>
        /// <param name="size">The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</param>
        public static void AddArrayParameters<T>(
            this SqlCommand cmd,
            string paramNameRoot,
            IEnumerable<T> values,
            SqlDbType? dbType = null,
            int? size = null)
        {
            var parameters = new List<SqlParameter>();
            var parameterNames = new List<string>();
            var paramNbr = 1;

            foreach (var value in values)
            {
                var paramName = $"@{paramNameRoot}{paramNbr++}";

                parameterNames.Add(paramName);

                var p = new SqlParameter(paramName, value);

                if (dbType.HasValue)
                    p.SqlDbType = dbType.Value;

                if (size.HasValue)
                    p.Size = size.Value;

                cmd.Parameters.Add(p);

                parameters.Add(p);
            }

            cmd.CommandText = cmd.CommandText
                .Replace("{" + paramNameRoot + "}", string.Join(",", parameterNames));

            parameters.ToArray();
        }
    }
}
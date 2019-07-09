using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Text;

namespace ChatAppUtils
{
    public static class SqlReaderExtensions
    {
        public static string GetStringInc(this SqlDataReader reader, ref int ordinal)
        {
            var result = reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
            ordinal++;
            return result;
        }

        public static long GetInt64Inc(this SqlDataReader reader, ref int ordinal)
        {
            var result = reader.GetInt64(ordinal);
            ordinal++;
            return result;
        }

        public static bool GetBooleanInc(this SqlDataReader reader, ref int ordinal)
        {
            var result = reader.GetBoolean(ordinal);
            ordinal++;
            return result;
        }

        public static DateTime GetDateTimeInc(this SqlDataReader reader, ref int ordinal)
        {
            var result = reader.GetDateTime(ordinal);
            ordinal++;
            return result;
        }
    }
}

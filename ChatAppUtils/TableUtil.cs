using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ChatAppUtils
{
    public static class TableUtil
    {
        public static SqlParameter CreateTableLong(string parameter, IEnumerable<long> values)
        {
            var param = new SqlParameter(parameter, SqlDbType.Structured);
            param.Value = CreateTable(values);
            param.TypeName = "[dbo].[ArrayBigint]";
            return param;
        }

        private static DataTable CreateTable(IEnumerable<long> ints)
        {
            var table = new DataTable();
            table.Columns.Add("val", typeof(long));
            if (ints != null)
            {
                foreach (var arg in ints)
                {
                    table.Rows.Add(arg);
                }
            }

            return table;
        }

        public static SqlParameter CreateTableInt(string parameter, IEnumerable<int> values)
        {
            var param = new SqlParameter(parameter, SqlDbType.Structured);
            param.Value = CreateTable(values);
            param.TypeName = "[dbo].[ArrayInt]";
            return param;
        }

        private static DataTable CreateTable(IEnumerable<int> ints)
        {
            var table = new DataTable();
            table.Columns.Add("val", typeof(int));
            if (ints != null)
            {
                foreach (var arg in ints)
                {
                    table.Rows.Add(arg);
                }
            }

            return table;
        }
    }
}

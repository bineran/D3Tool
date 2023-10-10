using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;

namespace POEService.common
{
    public class SQLHeplerSingle
    {
        private string connectionString { get; set; }
        public SQLHeplerSingle(string cnstr)
        {
            connectionString = cnstr;
        }
        private string PrepareSqlParameter(string SQLString, List<SqlParameter> alsp, params object[] ps)
        {

            if (alsp == null)
            {
                alsp = new List<SqlParameter>();
            }
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i] is SqlParameter)
                {
                    SqlParameter sqlParameter = ps[i] as SqlParameter;
                    SqlParameter cloneParameter = new SqlParameter(sqlParameter.ParameterName, sqlParameter.SqlDbType, sqlParameter.Size);
                    if (sqlParameter.Value == null)
                    {
                        cloneParameter.Value = DBNull.Value;
                    }
                    else
                    {
                        cloneParameter.Value = sqlParameter.Value;
                    }
                    alsp.Add(cloneParameter);

                }
                else if (SQLString.Contains("{" + i + "}"))
                {

                    SQLString = SQLString.Replace("{" + i + "}", "@auto_param_" + i);
                    if (ps[i] == null)
                    {
                        alsp.Add(new SqlParameter("@auto_param_" + i, DBNull.Value));
                    }
                    else
                    {
                        alsp.Add(new SqlParameter("@auto_param_" + i, ps[i]));
                    }
                }
            }
            return SQLString;

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString, params object[] ps)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> alsp = new List<SqlParameter>();
            SQLString = PrepareSqlParameter(SQLString, alsp, ps);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                if (alsp.Count > 0)
                {
                    command.SelectCommand.Parameters.AddRange(alsp.ToArray());
                }
                command.SelectCommand.CommandTimeout = 120;
                command.Fill(ds, "ds");
            }

            return ds;
        }

        #region 执行SQL语句，返回影响的记录数
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteNonQuery(string SQLString, params object[] ps)
        {
            int rows = 0;
            List<SqlParameter> alsp = new List<SqlParameter>();
            SQLString = PrepareSqlParameter(SQLString, alsp, ps);
            using (TransactionScope ts = new TransactionScope())
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQLString, connection);
                    if (alsp.Count > 0)
                    {
                        cmd.Parameters.AddRange(alsp.ToArray());
                    }
                    rows = cmd.ExecuteNonQuery();
                }
                ts.Complete();
                return rows;
            }
        }
        #endregion

    }

}

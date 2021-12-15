using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace project
{
    class clsDatabase
    {

        SqlConnection sqlConn;
        SqlDataAdapter da;
        DataSet ds;
        public clsDatabase()
        {
            String strCnn = @"Data Source=THAIBAO\SQLEXPRESS;Initial Catalog=QL_QUANCAFE;Integrated Security=True";
            sqlConn = new SqlConnection(strCnn);
        }
        public DataTable Docbang(String sqlStr)
        {
            da = new SqlDataAdapter(sqlStr, sqlConn);
            ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
        public void ThucHienLenh(String strSql)
        {
            SqlCommand sqlcmd = new SqlCommand(strSql, sqlConn);
            sqlConn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlConn.Close();
        }
    }
}

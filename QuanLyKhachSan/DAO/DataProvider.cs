using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan.DAO
{
    public class DataProvider
    {

        private static DataProvider instance;

        private string strconn = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True";

        public static DataProvider Instance { 
            get {
                if (instance == null) instance = new DataProvider(); 
                return DataProvider.instance; 
            }
            private set { DataProvider.instance = value; } 
        }


        private DataProvider() { }
       

        public DataTable ExecuteQuery(string sqlQuery, object[] parameter = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(strconn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                if(parameter != null )
                {
                    string[] listPara = sqlQuery.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if(item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                conn.Close();
            }
            return dt;
        }

        public int ExecuteNonQuery(string sqlQuery, object[] parameter = null)
        {
            int dt = 0;
            using (SqlConnection conn = new SqlConnection(strconn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                if (parameter != null)
                {
                    string[] listPara = sqlQuery.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                dt = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return dt;
        }

        public object ExecuteScalar(string sqlQuery, object[] parameter = null)
        {
            object dt = 0;
            using (SqlConnection conn = new SqlConnection(strconn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                if (parameter != null)
                {
                    string[] listPara = sqlQuery.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                dt = cmd.ExecuteScalar();
                conn.Close();
            }
            return dt;
        }
    }
}

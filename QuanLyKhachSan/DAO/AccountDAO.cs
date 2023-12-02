using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace QuanLyKhachSan.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance {
            get
            {
                if (instance == null) instance = new AccountDAO();
                return instance;
            }
            private set {
                instance = value; 
            }
        }

        private AccountDAO() { }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("select username, fullname, type from tbl_users");
        }

        public bool Login(string username, string password)
        {

            string sqlQuery = "exec USP_Login @userName , @passWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(sqlQuery, new object[] {username, password});
            return result.Rows.Count > 0;
        }

        public ACcount GetAccountByUserName(string username)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from tbl_users where username = '"+ username +"'");

            foreach(DataRow item in dt.Rows)
            {
                return new ACcount(item);
            }

            return null;
        }

        public bool UpdateAccount(string username, string fullname, string passwd, string newPass)
        {
            int result =  DataProvider.Instance.ExecuteNonQuery("EXEC USP_UpdateAccount @username , @fullname , @passwd , @newPasswd ", new object[] {username, fullname, passwd, newPass});
            return result > 0;
        }



        public bool insertAccount(string username, string fullname, int type)
        {
            string sqlQuery = "Insert into tbl_users values (N'"+username+"', N'"+fullname+ "', N'123456', " + type+")";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }
        public bool updateAccount(string username, string fullname, int type)
        {
            string sqlQuery = "update tbl_users set fullname = N'" + fullname + "', type = "+type+" where username = N'"+username+"'";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }


        public bool deleteAccount(string username)
        {
            string sqlQuery = "Delete tbl_users where username = N'" +username+ "'";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }
        public bool ResetAccount(string username)
        {
            string sqlQuery = "Update tbl_users set passwd = N'123456' where username = N'" + username+"'";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }

        public bool checkAddAccount(string username)
        {
            string sqlQuery = "select * from tbl_users where username = N'" + username + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            bool ischeck = dt.Rows.Count > 0;


            if (ischeck)
            {
                return true;
            }
            return false;
        }
    }
}

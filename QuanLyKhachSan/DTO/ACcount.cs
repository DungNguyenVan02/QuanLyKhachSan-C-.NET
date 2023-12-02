using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class ACcount
    {


        public ACcount(string userName, string fullName, string passwd, int type)
        {
            this.UserName = userName;
            this.FullName = fullName;
            this.Passwd= passwd;
            this.Type= type;
        }

        public ACcount(DataRow row)
        {
            this.UserName = row["username"].ToString();
            this.FullName = row["fullname"].ToString();
            this.Passwd = row["passwd"].ToString();
            this.Type = (int)row["type"];
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private string passwd;
        public string Passwd
        {
            get { return passwd; }
            set { passwd = value; }
        }

        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

    }
}

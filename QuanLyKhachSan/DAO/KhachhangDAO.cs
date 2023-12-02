using DevExpress.Utils.Gesture;
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyKhachSan.DAO
{
    public class KhachhangDAO
    {
        public static KhachhangDAO instance;

        public static KhachhangDAO Instance
        {
            get
            {
                if (instance == null) instance = new KhachhangDAO();
                return KhachhangDAO.instance;
            }
            private set
            {
                KhachhangDAO.instance = value;
            }
        }

        private KhachhangDAO() { }




        public void InsertKhachhang(string hoten, string ngaysinh, string dienThoai, string email, string diaChi, string cccd, string gioitinh)
        {
            DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertKhachHang @hoten , @ngaysinh , @dienthoai , @email , @diachi , @cccd , @gioitinh", new object[] { hoten, ngaysinh, dienThoai,  email,  diaChi,  cccd , gioitinh});
        }

        public int GetMaxIdKhachHang()
        {
            try
            {
                string sqlQuery = "select MAX(id_khachhang) from tbl_khachhang";
                return (int)DataProvider.Instance.ExecuteScalar(sqlQuery);
            }
            catch
            {
                return 1;
            }
        }

        public int GetMaxIdKhachHangByCCCD(string cccd)
        {
            try
            {
                string sqlQuery = "select id_khachhang from tbl_khachhang where cccd = N'"+cccd+"'";
                return (int)DataProvider.Instance.ExecuteScalar(sqlQuery);
            }
            catch
            {
                return 1;
            }
        }

        public bool updateKhachhang(string name, string ngaysinh, string sdt, string email, string diachi, string cccd, string gioitinh, string id)
        {
            string sqlQuery = "Update tbl_khachhang set hoten = N'"+ name + "', ngaysinh = N'"+ngaysinh+"', dienthoai = N'"+sdt+"', email = N'"+email+"', diachi = N'"+diachi+"', cccd = N'"+cccd+"', gioitinh = N'"+gioitinh+"' where id_khachhang = N'"+id+"'";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }

        public bool deleteKhachhang(int id)
        {
            BillDAO.Instance.DeleteBillByIdKhachhang(id);
            string sqlQuery = "Delete tbl_khachhang where id_khachhang = " + id + "";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }

        public List<Khachhang> GetKhachHangList()
        {
            List<Khachhang> listKhachhang = new List<Khachhang>();

            string sqlQuery = "select *  from tbl_khachhang";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            foreach (DataRow item in dt.Rows)
            {
                Khachhang kh = new Khachhang(item);
                listKhachhang.Add(kh);
            }

            return listKhachhang;
        }

        public List<Khachhang> SearchKhachHangByCCCD(string cccd)
        {
            List<Khachhang> listKhachhang = new List<Khachhang>();

            string sqlQuery = "select * from tbl_khachhang where cccd like '%" + cccd + "%'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            foreach (DataRow item in dt.Rows)
            {
                Khachhang kh = new Khachhang(item);
                listKhachhang.Add(kh);
            }

            return listKhachhang;
        }

        

        public bool checkEmailKhachhang(string email)
        {
            string sqlQuery = "select *  from tbl_khachhang where email = N'" + email+"'";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            bool ischeck = dt.Rows.Count > 0;

            if (ischeck)
            {
                return true;
            }
            return false; 
        }
        public bool checkSDTKhachhang(string sdt)
        {
            string sqlQuery = "select * from tbl_khachhang where dienthoai = N'" + sdt + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            bool ischeck = dt.Rows.Count > 0;

            if (ischeck)
            {
                return true;
            }
            return false;
        }
        public bool checkCCCDKhachhang(string cccd)
        {
            string sqlQuery = "select * from tbl_khachhang where cccd = N'" + cccd + "'";
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

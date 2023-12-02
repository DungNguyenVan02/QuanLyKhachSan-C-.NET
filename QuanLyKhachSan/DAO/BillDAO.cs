using DevExpress.XtraEditors.Filtering.Templates;
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class BillDAO
    {
        public static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null) instance = new BillDAO();
                return BillDAO.instance;
            }
            private set
            {
                BillDAO.instance = value;
            }
        }

        private BillDAO() { }

        public int GetCheckIdKhachBill(int id)
        {

            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from tbl_hoadon where id_khachhang = " + id + " and trangthai = 0");

            bool ischeck = dt.Rows.Count > 0;

            if (ischeck)
            {
                Bill bill = new Bill(dt.Rows[0]);
                return bill.ID_hoadon; //Thành công trả vè id hóa đơn
            }
            return -1; //Không thành công trả về -1;
        }

        public bool GetCheckIdRoom(int id)
        {
            bool result = true;
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from tbl_hoadon where id_phong = " + id + " and trangthai = 0");

            bool ischeck = dt.Rows.Count > 0;

            if (ischeck)
            {
                result = false;
            }
            return result; //Không thành công trả về -1;
        }

        public List<Bill> GetBillByIdRoom(int id)
        {
            List<Bill> listBill = new List<Bill>();

            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from tbl_hoadon where id_phong = " + id + " and trangthai = 0");
            foreach (DataRow item in dt.Rows)
            {
                Bill info = new Bill(item);
                listBill.Add(info);
            }

            return listBill;
        }

        public List<Bill> GetBillByIdDichVu(int id)
        {
            List<Bill> listBill = new List<Bill>();

            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from tbl_hoadon where id_dichvu = " + id + " and trangthai = 0");
            foreach (DataRow item in dt.Rows)
            {
                Bill info = new Bill(item);
                listBill.Add(info);
            }

            return listBill;
        }



        public List<Bill> GetListBill(int id_hoadon)
        {
            List<Bill> listBill = new List<Bill>();

            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from tbl_hoadon where id_hoadon = " + id_hoadon + "");
            foreach (DataRow item in dt.Rows)
            {
                Bill info = new Bill(item);
                listBill.Add(info);
            }

            return listBill;
        }


        public void InsertBill(int id_khachhang, int id_phong, int id_dichvu, string ngayvao, string ngaytra)
        {
            string sqlQuery = "EXEC USP_InsertBill @id_khachhang , @id_phong , @id_dichvu , @ngayvao , @ngaytra";
            DataProvider.Instance.ExecuteNonQuery(sqlQuery, new object[] { id_khachhang, id_phong, id_dichvu, ngayvao, ngaytra, 0 });
        }

        public void CheckOut(int id, float tongtien)
        {
            string sqlQuery = "update tbl_hoadon set trangthai = 1, tongtien = "+tongtien+" where id_phong = "+id+"";
            DataProvider.Instance.ExecuteNonQuery(sqlQuery);
        }


        public DataTable  GetBillListByDate( DateTime ngayvao, DateTime ngayra)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_GetListBillBydate @ngayvao , @ngayra", new object[] { ngayvao, ngayra});
        }

        

        public void DeleteBillByIdRoom(int idRoom)
        {
            DataProvider.Instance.ExecuteQuery("Delete tbl_hoadon where id_phong = " + idRoom + "");
        }
        public void DeleteBillByIdDichvu(int idDichVu)
        {
            DataProvider.Instance.ExecuteQuery("Delete tbl_hoadon where id_dichvu = " + idDichVu + "");
        }

        public void DeleteBillByIdKhachhang(int idKhachhang)
        {
            DataProvider.Instance.ExecuteQuery("Delete tbl_hoadon where id_khachhang = " + idKhachhang + "");
        }

    }
}

using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class DichVuDAO
    {

        private static DichVuDAO instance;
        public static DichVuDAO Instance
        {
            get
            {
                if (instance == null) instance = new DichVuDAO();
                return DichVuDAO.instance;
            }
            private set
            {
                DichVuDAO.instance = value;
            }
        }

        private DichVuDAO() { }


        public bool checkAddDichvu(string tendichvu)
        {
            string sqlQuery = "select * from tbl_dichvu where tendichvu = N'" + tendichvu + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            bool ischeck = dt.Rows.Count > 0;

            if (ischeck)
            {
                return true;
            }
            return false;
        }

        public List<DichVu> GetDichVuList()
        {
            List<DichVu> listDichvu = new List<DichVu>();

            string sqlQuery = "select *  from tbl_dichvu";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            foreach (DataRow item in dt.Rows)
            {
                DichVu dv = new DichVu(item);
                listDichvu.Add(dv);
            }


            return listDichvu;
        }

        public List<DichVu> SearchDichVuByName(string tendichvu)
        {
            List<DichVu> listDichvu = new List<DichVu>();

            string sqlQuery = "select * from tbl_dichvu where tendichvu like '%" + tendichvu + "%'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            foreach (DataRow item in dt.Rows)
            {
                DichVu dichVu = new DichVu(item);
                listDichvu.Add(dichVu);
            }

            return listDichvu;
        }

        public int GetIdDichVu(string tendichvu)
        {
            try
            {   
                string sqlQuery = "EXEC USP_GetIdDichVubyName @tendichvu";
                return (int)DataProvider.Instance.ExecuteScalar(sqlQuery, new object[] {tendichvu});
            }
            catch
            {
                return 1;
            }
        }

        public bool insertDichVu(string tendichvu, float giadichvu)
        {
            string sqlQuery = "Insert into tbl_dichvu values (N'" + tendichvu + "', "+giadichvu+")";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }
        public bool updateDichVu(int idDichvu ,string tendichvu, float giadichvu)
        {
            string sqlQuery = "update tbl_dichvu set tendichvu = N'" + tendichvu + "', gia = " + giadichvu +" where id_dichvu = "+idDichvu+"";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }

        public float getGiaDV(int idDichvu)
        {
            string sqlQuery = "select gia from tbl_dichvu where id_dichvu = "+idDichvu+"";
            float result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result;
        }

        public bool deleteDichVu(int idDichvu)
        {
            BillDAO.Instance.DeleteBillByIdDichvu(idDichvu);
            string sqlQuery = "Delete tbl_dichvu where id_dichvu = " + idDichvu + "";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }



    }

}

using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class GetDichVuById
    {
        private static GetDichVuById instance;
        public static GetDichVuById Instance
        {
            get
            {
                if (instance == null) instance = new GetDichVuById();
                return GetDichVuById.instance;
            }
            private set
            {
                GetDichVuById.instance = value;
            }
        }

        private GetDichVuById() { }

        public List<MenuDichVu> GetListDVByIdRoom(int id)
        {
            List<MenuDichVu> listDV = new List<MenuDichVu>();


            string sqlQuery = "select a.tendichvu, a.gia, b.id_phong from tbl_dichvu as a, tbl_hoadon as b, tbl_khachhang as c where a.id_dichvu = b.id_dichvu and b.id_phong = " + id + " and b.id_khachhang = c.id_khachhang and b.trangthai = 0 group by a.tendichvu, a.gia, b.id_phong";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);
            foreach (DataRow item in dt.Rows)
            {
                MenuDichVu dichvu = new MenuDichVu(item);
                listDV.Add(dichvu);
            }

            return listDV;
        }


    }
}

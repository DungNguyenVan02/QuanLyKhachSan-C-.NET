using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class MenuDAO
    {

        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get
            {
                if (instance == null) instance = new MenuDAO();
                return MenuDAO.instance;
            }
            private set { MenuDAO.instance = value; }
        }

        private MenuDAO()
        {

        }

        public List<Menu> GetListMenuByRoom(int id)
        {
            List<Menu> listMenu = new List<Menu>();

            string sqlQuery = "select c.id_khachhang ,c.hoten, b.tenphong, b.tenloaiphong, d.gia as N'giaphong', DATEDIFF(Day,a.ngayvao, a.ngaytra) as N'songay', d.gia * (DATEDIFF(Day,a.ngayvao, a.ngaytra)) as N'tongtien'   from tbl_hoadon as a, tbl_phong as b, tbl_khachhang as c, tbl_loaiphong as d where b.tenloaiphong = d.tenloaiphong and a.id_khachhang = c.id_khachhang and a.id_phong = b.id_phong and a.trangthai = 0 and b.id_phong = "+id+" group by c.id_khachhang ,c.hoten, b.tenphong, b.tenloaiphong, d.gia, DATEDIFF(Day,a.ngayvao, a.ngaytra)";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            foreach (DataRow item in dt.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}

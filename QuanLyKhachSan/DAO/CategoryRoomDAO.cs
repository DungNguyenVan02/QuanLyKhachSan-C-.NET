using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class CategoryRoomDAO
    {

        private static CategoryRoomDAO instance;
        public static CategoryRoomDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryRoomDAO();
                }
                return CategoryRoomDAO.instance;
            }
            private set
            {
                CategoryRoomDAO.instance = value;
            }

        }

        private CategoryRoomDAO() { }

        public List<CategoryRoom> GetListCategoryRoom()
        {
            List<CategoryRoom> listCategoryRoom = new List<CategoryRoom>();

            string sqlQuery = "select * from tbl_loaiphong";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            foreach (DataRow item in dt.Rows)
            {
                CategoryRoom categoryRoom = new CategoryRoom(item);
                listCategoryRoom.Add(categoryRoom);
            }

            return listCategoryRoom;
        }

        
        public CategoryRoom GetCategoryById(string tenloaiphong)
        {
            CategoryRoom categoryRoom = null;

            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from tbl_loaiphong where tenloaiphong = "+tenloaiphong+"");

            foreach (DataRow item in dt.Rows)
            {
                categoryRoom = new CategoryRoom(item);
                return categoryRoom;
            }

            return categoryRoom;
        }

        
        public bool updateLoaiPhong(string tenloaiphong, float gia)
        {
            string sqlQuery = "update tbl_loaiphong set gia =  "+gia+" where tenloaiphong = N'" + tenloaiphong + "'";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }

    }
}
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class GetRoomByIdDAO
    {

        private static GetRoomByIdDAO instance;
        public static GetRoomByIdDAO Instance
        {
            get
            {
                if (instance == null) instance = new GetRoomByIdDAO();
                return GetRoomByIdDAO.instance;
            }
            private set
            {
                GetRoomByIdDAO.instance = value;
            }
        }

        private GetRoomByIdDAO() { }

        public List<Room> GetRoomByCategory(string id)
        {
            List<Room> roomListByctgry = new List<Room>();

            string sqlQuery = "select *  from tbl_phong where tenloaiphong = N'"+id+"' and trangthai = N'Trống'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            foreach (DataRow item in dt.Rows)
            {
                Room room = new Room(item);
                roomListByctgry.Add(room);
            }


            return roomListByctgry;
        }

    }
}

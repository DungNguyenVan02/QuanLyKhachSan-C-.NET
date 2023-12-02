using QuanLyKhachSan.DTO;
using System.Collections.Generic;
using System.Data;
using static DevExpress.Xpo.Helpers.CommandChannelHelper;

namespace QuanLyKhachSan.DAO
{
    public class RoomDAO
    {
        private static RoomDAO instance;

        public static RoomDAO Instance {
            get { 
                if (instance == null) instance = new RoomDAO();
                return RoomDAO.instance;
            }
            private set
            {
                RoomDAO.instance= value;
            } 
        }


        public static int RoomWidth = 90;
        public static int RoomHeight = 90;


        private RoomDAO() { }

        public List<Room> LoadRoomList()
        {
            List<Room> roomList = new List<Room>();

            string sqlQuery = "USP_GetRoomList";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            foreach (DataRow item in dt.Rows )
            {
                Room room = new Room(item);
                roomList.Add(room);
            }


            return roomList;
        }

        public List<Room> SearchRoomByName(string tenphong)
        {
            List<Room> roomList = new List<Room>();

            string sqlQuery = "select * from tbl_phong where tenphong like '%"+tenphong+"%'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            foreach (DataRow item in dt.Rows)
            {
                Room room = new Room(item);
                roomList.Add(room);
            }


            return roomList;
        }
        public int GetIdPhong(string tenphong)
        {
            try
            {
                string sqlQuery = "EXEC USP_GetIdRoombyName @tenphong";
                return (int)DataProvider.Instance.ExecuteScalar(sqlQuery, new object[] {tenphong});
            }
            catch
            {
                return 1;
            }
        }


        public bool checkAddRoom(string tenphong)
        {
            string sqlQuery = "select * from tbl_phong where tenphong = N'" + tenphong + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

            bool ischeck = dt.Rows.Count > 0;

            if (ischeck)
            {
                return true;
            }
            return false;
        }
        public void UpdateState(int id_phong)
        {
            string sqlQuery = "update tbl_phong set trangthai = N'Có người' where id_phong = "+id_phong+"";
            DataProvider.Instance.ExecuteNonQuery(sqlQuery);
        }

        public void UpdateStateWhenSwitch(int id_phong)
        {
            string sqlQuery = "update tbl_phong set trangthai = N'Trống' where id_phong = " + id_phong + "";
            DataProvider.Instance.ExecuteNonQuery(sqlQuery);
        }

        public void SwitchRoom(int idRoom1, int idRoom2)
        {
            DataProvider.Instance.ExecuteQuery("USP_SwitchRoom @idFristRoom , @idSeconrdRoom", new object[] { idRoom1, idRoom2 });
        }

        public bool insertRoom(string tenloaiphong, string tang, string tenPhong, int songuoi, int soluong)
        {
            string sqlQuery = "Insert into tbl_phong values (N'"+tenloaiphong+"', N'"+tang+"', N'"+tenPhong+"', "+songuoi+", "+soluong+", N'Trống')";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }
        public bool updateRoom(int idPhong,string tenloaiphong, string tang, string tenPhong, int songuoi, int soluong)
        {
            string sqlQuery = "update tbl_phong set tenloaiphong = N'"+tenloaiphong+"', tentang = N'"+tang+"', tenphong = N'"+tenPhong+"', songuoi = "+songuoi+", soluong = "+soluong+" where id_phong = "+idPhong+"";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }


        public bool deleteRoom(int idPhong)
        {
            BillDAO.Instance.DeleteBillByIdRoom(idPhong);
            string sqlQuery = "Delete tbl_phong where id_phong = "+idPhong+"";
            int result = DataProvider.Instance.ExecuteNonQuery(sqlQuery);
            return result > 0;
        }

        
    }
}

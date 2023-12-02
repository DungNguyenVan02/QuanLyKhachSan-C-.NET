using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class Room
    {
        
        public Room(int iD_Phong, string tenLoaiPhong, string tenTang, string tenPhong, int soNguoi, int soLuong, string trangThai) {
            this.ID_Phong = iD_Phong;
            this.TenLoaiPhong= tenLoaiPhong;
            this.TenTang= tenTang;
            this.TenPhong= tenPhong;
            this.SoNguoi= soNguoi;
            this.SoLuong= soLuong;
            this.TrangThai= trangThai;
        
        }

        public Room(DataRow row)
        {
            this.ID_Phong = (int)row["id_phong"];
            this.TenLoaiPhong = row["tenloaiphong"].ToString();
            this.TenTang = row["tentang"].ToString();
            this.TenPhong = row["tenphong"].ToString();
            this.SoNguoi = (int)row["songuoi"];
            this.SoLuong = (int)row["soluong"];
            this.TrangThai = row["trangthai"].ToString();
        }



        private string trangThai;

        public string TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }


        private int soNguoi;

        public int SoNguoi
        {
            get { return soNguoi; }
            set { soNguoi = value; }
        }

        private int soLuong;

        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }


        private string tenTang;

        public string TenTang
        {
            get { return tenTang; }
            set { tenTang = value; }
        }


        private string tenLoaiPhong;

        public string TenLoaiPhong
        {
            get { return tenLoaiPhong; }
            set { tenLoaiPhong = value; }
        }



        private string tenPhong;

        public string TenPhong
        {
            get { return tenPhong; }
            set { tenPhong = value; }
        }

        private int iD_Phong;

        public int ID_Phong
        {
            get { return iD_Phong; }
            set { iD_Phong = value; }
        }

    }
}

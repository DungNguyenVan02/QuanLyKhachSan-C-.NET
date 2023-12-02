using DevExpress.ClipboardSource.SpreadsheetML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class Menu
    {


        public Menu(string tenPhong,
                    string tenLoaiPhong,
                    float giaPhong,
                    float tongTien,
                    string tenKhachHang,
                    int maKhachhang,
                    string soNgay
        )
        {
            this.TenPhong = tenPhong;
            this.TenLoaiPhong= tenLoaiPhong;
            this.GiaPhong= giaPhong;
            this.TenKhachHang= tenKhachHang;
            this.MaKhachHang = maKhachhang;
            this.TongTien = tongTien;
            this.SoNgay= soNgay;
        }

        public Menu(DataRow row)
        {
            this.TenPhong = row["tenphong"].ToString();
            this.TenLoaiPhong = row["tenloaiphong"].ToString();
            this.TenKhachHang = row["hoten"].ToString();
            this.SoNgay = row["songay"].ToString();
            this.GiaPhong = (float)Convert.ToDouble(row["giaphong"].ToString());
            this.MaKhachHang = (int)Convert.ToDouble(row["id_khachhang"].ToString());
            this.TongTien = (float)Convert.ToDouble(row["tongtien"].ToString());
        }


        private string tenPhong;

        public string TenPhong {
            get { return tenPhong; }
            set { tenPhong = value; }
        }
        private string tenLoaiPhong;

        public string TenLoaiPhong
        {
            get { return tenLoaiPhong; }
            set { tenLoaiPhong = value; }
        }
        private float giaPhong;

        public float GiaPhong
        {
            get { return giaPhong; }
            set { giaPhong = value; }
        }

        private string tenKhachHang;

        public string TenKhachHang
        {
            get { return tenKhachHang; }
            set { tenKhachHang = value; }
        }


        private int maKhachHang;

        public int MaKhachHang
        {
            get { return maKhachHang; }
            set { maKhachHang = value; }
        }


        
        private float tongTien;

        public float TongTien
        {
            get { return tongTien; }
            set { tongTien = value; }
        }

        private string soNgay;

        public string SoNgay
        {
            get { return soNgay; }
            set { soNgay = value; }
        }
    }
}

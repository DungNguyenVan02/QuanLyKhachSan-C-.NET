using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class Khachhang
    {
        
        
        public Khachhang(int iD_Khachhang, string ten_Khachhang, string ngaySinh, string gioiTinh, string dienThoai, string diaChi, string email, string cccd) { 
            this.ID_Khachhang= iD_Khachhang;
            this.Ten_Khachhang= ten_Khachhang;
            this.NgaySinh= ngaySinh;
            this.GioiTinh= gioiTinh;
            this.DienThoai= dienThoai;
            this.DiaChi= diaChi;
            this.Email= email;
            this.Cccd= cccd;
        }

        public Khachhang(DataRow row)
        {
            this.ID_Khachhang = (int)row["id_khachhang"];
            this.Ten_Khachhang = row["hoten"].ToString();
            this.NgaySinh = row["ngaysinh"].ToString();
            this.GioiTinh = row["gioitinh"].ToString();
            this.DienThoai = row["dienthoai"].ToString();
            this.DiaChi = row["diachi"].ToString();
            this.Email = row["email"].ToString();
            this.Cccd = row["cccd"].ToString();
        }


        private int iD_Khachhang;

        public int ID_Khachhang
        {
            get { return iD_Khachhang; }
            set { iD_Khachhang = value; }
        }

        private string ten_Khachhang;

        public string Ten_Khachhang
        {
            get { return ten_Khachhang; }
            set { ten_Khachhang = value; }
        }

        private string ngaySinh;

        public string NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        private string gioiTinh;

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        private string dienThoai;
        public string DienThoai
        {
            get { return dienThoai; }
            set { dienThoai = value; }
        }

        private string diaChi;
        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string cccd;
        public string Cccd
        {
            get { return cccd; }
            set { cccd = value; }
        }
    }
}

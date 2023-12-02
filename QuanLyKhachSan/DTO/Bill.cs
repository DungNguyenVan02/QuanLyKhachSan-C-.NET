using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class Bill
    {

        

        public Bill(int iD_hoadon, int iD_khachang, int iD_phong,
           int iD_dichvu,  DateTime? ngayVao, DateTime? ngayTra, int trangthai
        )
        {
            this.ID_hoadon = iD_hoadon;
            this.ID_khachhang = iD_khachang;
            this.ID_phong = iD_phong;
            this.ID_dichvu = iD_dichvu;
            this.NgayVao = ngayVao;
            this.NgayTra = ngayTra;
            this.Trangthai = trangthai;
        }

        public Bill(DataRow row)
        {
            this.ID_hoadon = (int)row["id_hoadon"];
            this.ID_khachhang = (int)row["id_khachhang"];
            this.ID_phong = (int)row["id_phong"];
            this.ID_dichvu = (int)row["id_dichvu"];
            this.NgayVao = (DateTime?)row["ngayvao"];
            var ngayTraTemp = row["ngaytra"];

            if (ngayTraTemp.ToString() != "")
            {
                this.NgayTra = (DateTime?)ngayTraTemp;

            }
            this.Trangthai = (int)row["trangthai"];
        }

        private int iD_hoadon;
        public int ID_hoadon
        {
            get { return iD_hoadon; }
            set { iD_hoadon = value; }
        }

        private int iD_khachhang;
        public int ID_khachhang
        {
            get { return iD_khachhang; }
            set { iD_khachhang = value; }
        }

        private int iD_phong;
        public int ID_phong
        {
            get { return iD_phong; }
            set { iD_phong = value; }
        }

        private int iD_dichvu;
        public int ID_dichvu
        {
            get { return iD_dichvu; }
            set { iD_dichvu = value; }
        }


        private DateTime? ngayVao;
        public DateTime? NgayVao
        {
            get { return ngayVao; }
            set { ngayVao = value; }
        }

        private DateTime? ngayTra;
        public DateTime? NgayTra
        {
            get { return ngayTra; }
            set { ngayTra = value; }
        }

        private int trangthai;
        public int Trangthai
        {
            get { return trangthai; }
            set { trangthai = value; }
        }

    }
}

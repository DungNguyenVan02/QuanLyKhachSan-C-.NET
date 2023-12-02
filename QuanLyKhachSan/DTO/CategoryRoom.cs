using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class CategoryRoom
    {
        public CategoryRoom(string tenLoaiPhong, float gia)
        {

            this.TenLoaiPhong = tenLoaiPhong;
            this.Gia = gia;
        }

        public CategoryRoom(DataRow row)
        {
            this.TenLoaiPhong = row["tenloaiphong"].ToString();
            this.Gia = (float)Convert.ToDouble(row["gia"].ToString());
        }

        public string tenLoaiPhong;
        public string TenLoaiPhong
        {
            get { return tenLoaiPhong; }
            set { tenLoaiPhong = value; }
        }

        public float gia;
        public float Gia
        {
            get { return gia; }
            set { gia = value; }
        }
    }
}

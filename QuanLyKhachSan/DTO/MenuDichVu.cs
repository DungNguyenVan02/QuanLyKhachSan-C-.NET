using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class MenuDichVu
    {
        public MenuDichVu(string tenDichVu, float giaDichVu, int iDPhong)
        {
            this.TenDichVu = tenDichVu;
            this.GiaDichVu = giaDichVu;
            this.IDPhong = iDPhong;
        }

        public MenuDichVu(DataRow row)
        {
            this.TenDichVu = row["tendichvu"].ToString();
            this.GiaDichVu = (float)Convert.ToDouble(row["gia"].ToString());
            this.IDPhong = (int)Convert.ToDouble(row["id_phong"].ToString());

        }
        private string tenDichVu;

        public string TenDichVu
        {
            get { return tenDichVu; }
            set { tenDichVu = value; }
        }

        private float giaDichVu;

        public float GiaDichVu
        {
            get { return giaDichVu; }
            set { giaDichVu = value; }
        }


        private int iDPhong;
        public int IDPhong
        {
            get { return iDPhong; }
            set
            {
                iDPhong = value;
            }
        }
    }
}

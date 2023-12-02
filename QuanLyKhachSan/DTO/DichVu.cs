using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class DichVu
    {
        public DichVu(int id_DichVu,string tenDichVu, float giaDichVu)
        {
            this.ID_DichVu= id_DichVu;
            this.TenDichVu = tenDichVu;
            this.GiaDichVu = giaDichVu;
            
        }

        public DichVu(DataRow row)
        {
            this.ID_DichVu = (int)row["id_dichvu"];
            this.TenDichVu = row["tendichvu"].ToString();
            this.GiaDichVu = (float)Convert.ToDouble(row["gia"].ToString());
            

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

        private int iD_DichVu;

        public int ID_DichVu
        {
            get { return iD_DichVu; }
            set { iD_DichVu = value; }
        }


    }
}

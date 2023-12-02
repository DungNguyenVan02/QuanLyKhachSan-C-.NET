using DevExpress.XtraEditors.Mask.Design;
using QuanLyKhachSan.DAO;
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Data.Helpers.FindSearchRichParser;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QuanLyKhachSan
{
    public partial class fDatPhong : Form
    {


        public fDatPhong()
        {
            InitializeComponent();
            loadLoaiPhong();
            loadDichVu();
            LoadListKhachhang();
        }

        void LoadListKhachhang()
        {
            dtgvKhachHangCu.DataSource = KhachhangDAO.Instance.GetKhachHangList();
        }

        void loadLoaiPhong()
        {
            List<CategoryRoom> listCategoryRoom = CategoryRoomDAO.Instance.GetListCategoryRoom();
            cbLoaiPhong.DataSource = listCategoryRoom;
            cbLoaiPhong.DisplayMember = "tenloaiphong";
            cbLoaiPhongFKhachCu.DataSource = listCategoryRoom;
            cbLoaiPhongFKhachCu.DisplayMember = "tenloaiphong";
        }

        void loadPhongByLoaiPhong(string id)
        {
            List<Room> litRoom = GetRoomByIdDAO.Instance.GetRoomByCategory(id);
            cbPhong.DataSource = litRoom;
            cbPhong.DisplayMember = "tenphong";
            cbPhongFKhachCu.DataSource = litRoom;
            cbPhongFKhachCu.DisplayMember = "tenphong";
        }
        void loadDichVu()
        {
            List<DichVu> listDichVu = DichVuDAO.Instance.GetDichVuList();
            cbDichVu.DataSource = listDichVu;
            cbDichVu.DisplayMember = "tendichvu";
            cbDichVuFKhachCu.DataSource = listDichVu;
            cbDichVuFKhachCu.DisplayMember = "tendichvu";
        }


        private void cbLoaiPhong_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            CategoryRoom selected = cb.SelectedItem as CategoryRoom;
            string id = selected.TenLoaiPhong;
            loadPhongByLoaiPhong(id);

        }


        private void btnDatPhong_Click_1(object sender, EventArgs e)
        {
            string hoTen = txtTenKhachHang.Text;

            string gioitinh = "";

            if (rdoNam.Checked)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            string dienThoai = txtDienthoai.Text;
            string Email = txtEmail.Text;
            string diaChi = txtDiachi.Text;
            string cccd = txtCCCD.Text;
            string loaiphong = cbLoaiPhong.SelectedItem.ToString();
            string ngaysinh = txtNgaySinh.Text;
            string ngayvao = dtNgayDat.Value.ToString("yyyy-MM-dd");
            string ngaytra = dtNgayTra.Value.ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(gioitinh) || string.IsNullOrEmpty(dienThoai) || string.IsNullOrEmpty(Email)
                || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(loaiphong) || string.IsNullOrEmpty(ngayvao) || string.IsNullOrEmpty(ngaytra) || string.IsNullOrEmpty(cccd)
            )
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (KhachhangDAO.Instance.checkSDTKhachhang(dienThoai))
            {
                MessageBox.Show("Số điện thoại đã đã có trong cở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (KhachhangDAO.Instance.checkCCCDKhachhang(cccd))
            {
                MessageBox.Show("Căn cước công dân đã đã có trong cở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (KhachhangDAO.Instance.checkEmailKhachhang(Email))
            {
                MessageBox.Show("Email đã đã có trong cở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(KhachhangDAO.Instance.checkSDTKhachhang(dienThoai) == false 
                && KhachhangDAO.Instance.checkCCCDKhachhang(cccd) == false
               && KhachhangDAO.Instance.checkEmailKhachhang(Email) == false)
            {
                KhachhangDAO.Instance.InsertKhachhang(hoTen, ngaysinh, dienThoai, Email, diaChi, cccd, gioitinh);
                int idKhachHang = KhachhangDAO.Instance.GetMaxIdKhachHang();
                int id_phong = RoomDAO.Instance.GetIdPhong(cbPhong.Text);
                int id_dichvu = DichVuDAO.Instance.GetIdDichVu(cbDichVu.Text);
                BillDAO.Instance.InsertBill(idKhachHang, id_phong, id_dichvu, ngayvao, ngaytra);
                MessageBox.Show("Đặt phòng thành công");
                RoomDAO.Instance.UpdateState(id_phong);

                this.Close();
            }
        }

        


        private void btnHuyDV_Click(object sender, EventArgs e)
        {

        }

        private void dtgvKhachHangCu_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtHoTenKhachCu.Text = dtgvKhachHangCu.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDienThoaiKhachCu.Text = dtgvKhachHangCu.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtGioiTinhKhachCu.Text = dtgvKhachHangCu.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDiaChiKhachCu.Text = dtgvKhachHangCu.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtNgaySinhKhachCu.Text = dtgvKhachHangCu.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmailKhachCu.Text = dtgvKhachHangCu.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtCCCDKhachCu.Text = dtgvKhachHangCu.Rows[e.RowIndex].Cells[7].Value.ToString();
        }


        List<Khachhang> searchKhachHangByCCCD(string cccd)
        {
            List<Khachhang> listKH = new List<Khachhang>();

            listKH = KhachhangDAO.Instance.SearchKhachHangByCCCD(cccd);

            return listKH;
        }

        private void btnTimKiemKhachHangCu_Click(object sender, EventArgs e)
        {
            string valueSearch = txtSearchCCCD.Text;
            dtgvKhachHangCu.DataSource = searchKhachHangByCCCD(valueSearch);

        }

        private void cbLoaiPhongFKhachCu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            CategoryRoom selected = cb.SelectedItem as CategoryRoom;
            string id = selected.TenLoaiPhong;
            loadPhongByLoaiPhong(id);
        }

        private void btnDatPhongOld_Click(object sender, EventArgs e)
        {
            string ngayvao = dtpkNgayden.Value.ToString("yyyy-MM-dd");
            string ngaytra = dtpkNgaydi.Value.ToString("yyyy-MM-dd");
            int idKhachHang = KhachhangDAO.Instance.GetMaxIdKhachHangByCCCD(txtCCCDKhachCu.Text);
            int id_phong = RoomDAO.Instance.GetIdPhong(cbPhong.Text);
            int id_dichvu = DichVuDAO.Instance.GetIdDichVu(cbDichVu.Text);
            BillDAO.Instance.InsertBill(idKhachHang, id_phong, id_dichvu, ngayvao, ngaytra);
            MessageBox.Show("Đặt phòng thành công");
            RoomDAO.Instance.UpdateState(id_phong);

            this.Close();
        }
    }
}

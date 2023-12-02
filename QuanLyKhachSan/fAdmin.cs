using DevExpress.XtraEditors.Filtering.Templates;
using QuanLyKhachSan.DAO;
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyKhachSan
{
    public partial class fAdmin : Form
    {

        BindingSource roomList = new BindingSource();
        BindingSource dichvuList = new BindingSource();
        BindingSource accountList = new BindingSource();


        public ACcount loginAccount;

        public fAdmin()
        {
            InitializeComponent();
            Hienthi();
        }


        #region methods

        void Hienthi()
        {
            dtgvDichVu.DataSource = dichvuList;
            dtgvPhong.DataSource = roomList;
            dtgvTaiKhoan.DataSource = accountList;

            loadDateTimePakerBill();
            LoadListBillByDay(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListRoom();
            LoadAccount();
            AddRoomBinding();
            AdddichVuBinding();
            AdddAccountBinding();
            loadLoaiPhong();
            LoadListDichVu();
            LoadListKhachhang();

        }

        void loadLoaiPhong()
        {
            List<CategoryRoom> listCategoryRoom = CategoryRoomDAO.Instance.GetListCategoryRoom();
            cbLoaiPhong.DataSource = listCategoryRoom;
            cbLoaiPhong.DisplayMember = "tenloaiphong";
            cbFormLoaiphong.DataSource = listCategoryRoom;
            cbFormLoaiphong.DisplayMember = "tenloaiphong";
            dtgvLoaiPhong.DataSource = listCategoryRoom;
        }

        void loadDateTimePakerBill()
        {
            DateTime datetime = DateTime.Now;
            dtpkFromDate.Value = new DateTime(datetime.Year, datetime.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDay(DateTime fromdate, DateTime toDate)
        {
            dtgvHoaDon.DataSource = BillDAO.Instance.GetBillListByDate(fromdate, toDate);
        }


        void LoadListRoom()
        {
            roomList.DataSource = RoomDAO.Instance.LoadRoomList();
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void LoadListDichVu()
        {
            dichvuList.DataSource = DichVuDAO.Instance.GetDichVuList();
        }

        void LoadListKhachhang()
        {
            dtgvKhachhang.DataSource = KhachhangDAO.Instance.GetKhachHangList();
        }


        void AdddichVuBinding()
        {
            txtDichVuID.DataBindings.Add(new Binding("text", dtgvDichVu.DataSource, "id_dichvu", true, DataSourceUpdateMode.Never));
            txtTendichvu.DataBindings.Add(new Binding("text", dtgvDichVu.DataSource, "tendichvu", true, DataSourceUpdateMode.Never));
            nmGiaDichVu.DataBindings.Add(new Binding("text", dtgvDichVu.DataSource, "giadichvu", true, DataSourceUpdateMode.Never));
        }

        void AdddAccountBinding()
        {
            txtTenTaiKhoan.DataBindings.Add(new Binding("text", dtgvTaiKhoan.DataSource, "username", true, DataSourceUpdateMode.Never));
            txtTenHienThi.DataBindings.Add(new Binding("text", dtgvTaiKhoan.DataSource, "fullname", true, DataSourceUpdateMode.Never));

        }
        void AddRoomBinding()
        {
            txtSoPhong.DataBindings.Add(new Binding("text", dtgvPhong.DataSource, "tenphong", true, DataSourceUpdateMode.Never));
            txtPhongID.DataBindings.Add(new Binding("text", dtgvPhong.DataSource, "id_phong", true, DataSourceUpdateMode.Never));
            cbLoaiPhong.DataBindings.Add(new Binding("text", dtgvPhong.DataSource, "tenloaiphong", true, DataSourceUpdateMode.Never));
            nmSoluong.DataBindings.Add(new Binding("value", dtgvPhong.DataSource, "soluong", true, DataSourceUpdateMode.Never));
            nmSoNguoi.DataBindings.Add(new Binding("value", dtgvPhong.DataSource, "songuoi", true, DataSourceUpdateMode.Never));
            nmTangPhong.DataBindings.Add(new Binding("value", dtgvPhong.DataSource, "tentang", true, DataSourceUpdateMode.Never));
        }



        List<Room> searchRoomByName(string name)
        {
            List<Room> listRoom = new List<Room>();

            listRoom = RoomDAO.Instance.SearchRoomByName(name);

            return listRoom;
        }

        List<DichVu> searchDichVuByName(string name)
        {
            List<DichVu> listDichVu = new List<DichVu>();

            listDichVu = DichVuDAO.Instance.SearchDichVuByName(name);

            return listDichVu;
        }



        void AddACcount(string username, string fullname, int type)
        {
            if (AccountDAO.Instance.insertAccount(username, fullname, type))
            {

                MessageBox.Show("Thêm tài khoản thành công");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
            }
        }

        void UpdateACcount(string username, string fullname, int type)
        {
            if (AccountDAO.Instance.updateAccount(username, fullname, type))
            {

                MessageBox.Show("Cập nhật tài khoản thành công");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
            }
        }

        void DeleteACcount(string username)
        {
            if (loginAccount.UserName.Equals(username))
            {
                MessageBox.Show("Trùng tài khoản của bạn rồi");
                return;
            }
            if (AccountDAO.Instance.deleteAccount(username))
            {

                MessageBox.Show("Xóa tài khoản thành công");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
            }
        }



        void ResetAccount(string username)
        {
            if (AccountDAO.Instance.ResetAccount(username))
            {

                MessageBox.Show("Resset tài khoản thành công, Mật khẩu mặc định là 123456");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
            }
        }

        #endregion



        #region events

        //---------------------------------Xử lý tài khoản----------------------//
        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            string username = txtTenTaiKhoan.Text;
            string fullname = txtTenHienThi.Text;
            int type;
            if (rdoStaff.Checked)
            {
                type = 0;
            }
            else
            {
                type = 1;
            }
            if (AccountDAO.Instance.checkAddAccount(username))
            {
                MessageBox.Show("Tên tài khoản đã tồn tại", "Thông báo");
            }
            else
            {
                AddACcount(username, fullname, type);
            }
        }
        private void btnSuaTaiKhoan_Click(object sender, EventArgs e)
        {
            string username = txtTenTaiKhoan.Text;
            string fullname = txtTenHienThi.Text;
            int type;
            if (rdoStaff.Checked)
            {
                type = 0;
            }
            else
            {
                type = 1;
            }
            
             UpdateACcount(username, fullname, type);
        }
        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            string username = txtTenTaiKhoan.Text;

            if (MessageBox.Show("Bạn có thự sự muốn xóa tài khoản: " + username + "", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {

                DeleteACcount(username);
            }

        }

        private void btnXemTaiKhoan_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }


        private void btnResetPasswd_Click(object sender, EventArgs e)
        {
            string username = txtTenTaiKhoan.Text;

            ResetAccount(username);
        }


        private void btnXemDoanhThu_Click(object sender, EventArgs e)
        {
            LoadListBillByDay(dtpkFromDate.Value, dtpkToDate.Value);
        }

        //------------------------------------- Xử lý phòng---------------------------------------//


        private void btnXemPhong_Click(object sender, EventArgs e)
        {
            LoadListRoom();
        }
        private void btnTimKiemPhong_Click(object sender, EventArgs e)
        {
            roomList.DataSource = searchRoomByName(txtTimKiemTenPhong.Text);
        }
        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            string tenphong = txtSoPhong.Text;
            string tenloaiphong = cbLoaiPhong.Text;
            int songuoi = (int)nmSoNguoi.Value;
            int soluong = (int)nmSoluong.Value;
            string tang = nmTangPhong.Value.ToString();


            if (RoomDAO.Instance.checkAddRoom(tenphong))
            {
                MessageBox.Show("Trùng tên phòng! Vui lòng nhập tên khác");
            }
            else
            {
                if (RoomDAO.Instance.insertRoom(tenloaiphong, tang, tenphong, songuoi, soluong))
                {
                    MessageBox.Show("Thêm phòng thành công");
                    LoadListRoom();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
                }

            }
        }

        private void btnSuaPhong_Click(object sender, EventArgs e)
        {
            string tenphong = txtSoPhong.Text;
            string tenloaiphong = cbLoaiPhong.Text;
            int songuoi = (int)nmSoNguoi.Value;
            int soluong = (int)nmSoluong.Value;
            string tang = nmTangPhong.Value.ToString();
            int idPhong = Convert.ToInt32(txtPhongID.Text);


            if (RoomDAO.Instance.checkAddRoom(tenphong))
            {
                MessageBox.Show("Trùng tên phòng! Vui lòng nhập tên khác");
            }
            else
            {
                if (RoomDAO.Instance.updateRoom(idPhong, tenloaiphong, tang, tenphong, songuoi, soluong))
                {
                    MessageBox.Show("Cập nhật phòng thành công");
                    LoadListRoom();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
                }

            }
        }

        private void btnXoaPhong_Click(object sender, EventArgs e)
        {

            int idPhong = Convert.ToInt32(txtPhongID.Text);
            string tenphong = txtSoPhong.Text;
            if (MessageBox.Show("Bạn có thự sự muốn xóa phòng: " + tenphong + "", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (RoomDAO.Instance.deleteRoom(idPhong))
                {
                    MessageBox.Show("Xóa phòng thành công");
                    LoadListRoom();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
                }
            }
        }

        //-----------------------------------------Xử lý dịch vụ-------------------------------//
        private void btnTimKiemDichVu_Click(object sender, EventArgs e)
        {
            dichvuList.DataSource = searchDichVuByName(txtTimkiemDichVu.Text);
        }
        private void btnXemDichVu_Click(object sender, EventArgs e)
        {
            LoadListDichVu();
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            string tendichvu = txtTendichvu.Text;
            float giadichvu = (float)nmGiaDichVu.Value;

            if (giadichvu <= 0)
            {
                MessageBox.Show("Giá dịch vụ lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DichVuDAO.Instance.checkAddDichvu(tendichvu))
            {
                MessageBox.Show("Trùng tên dịch vụ! Vui lòng nhập tên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (DichVuDAO.Instance.insertDichVu(tendichvu, giadichvu))
                {
                    MessageBox.Show("Thêm dịch vụ thành công");
                    LoadListDichVu();
                    if (insertDichVu != null)
                    {
                        insertDichVu(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
                }
            }


        }

        private void btnSuaDichVu_Click(object sender, EventArgs e)
        {
            int idDichVu = Convert.ToInt32(txtDichVuID.Text);
            string tendichvu = txtTendichvu.Text;
            float giadichvu = (float)nmGiaDichVu.Value;

            if (giadichvu <= 0)
            {
                MessageBox.Show("Giá dịch vụ lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (DichVuDAO.Instance.checkAddDichvu(tendichvu) && DichVuDAO.Instance.getGiaDV(DichVuDAO.Instance.GetIdDichVu(tendichvu)) == giadichvu)
            {
                MessageBox.Show("Trùng tên dịch vụ! Vui lòng nhập tên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (DichVuDAO.Instance.updateDichVu(idDichVu, tendichvu, giadichvu))
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công");
                    LoadListDichVu();
                    if (updateDichVu != null)
                    {
                        updateDichVu(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
                }
            }


        }
        private void btnXoaDichVu_Click(object sender, EventArgs e)
        {
            int idDichVu = Convert.ToInt32(txtDichVuID.Text);
            string tendichvu = txtTendichvu.Text;

            if (MessageBox.Show("Bạn có thự sự muốn xóa dịch vụ:  " + tendichvu + "", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (DichVuDAO.Instance.deleteDichVu(idDichVu))
                {
                    MessageBox.Show("Xóa dịch vụ thành công");
                    LoadListDichVu();
                    if (deleteDichVu != null)
                    {
                        deleteDichVu(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
                }
            }

        }



        //----------------------------------------Xử lý loại phòng-------------------------------//
        private void btnXemLoaiPhong_Click(object sender, EventArgs e)
        {
            loadLoaiPhong();
        }

        private void btnSuaLoaiPhong_Click(object sender, EventArgs e)
        {
            string tenloaiphong = cbFormLoaiphong.Text;
            float gia = (float)nmGiaPhong.Value;

            if (CategoryRoomDAO.Instance.updateLoaiPhong(tenloaiphong, gia))
            {
                MessageBox.Show("Cập nhật loại phòng thành công");
                loadLoaiPhong();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
            }
        }


       



        private event EventHandler insertDichVu;
        public event EventHandler InsertDichVu
        {
            add { insertDichVu += value; }
            remove { insertDichVu -= value; }
        }
        private event EventHandler deleteDichVu;
        public event EventHandler DeleteDichVu
        {
            add { deleteDichVu += value; }
            remove { deleteDichVu -= value; }
        }
        private event EventHandler updateDichVu;
        public event EventHandler UpdateDichVu
        {
            add { updateDichVu += value; }
            remove { updateDichVu -= value; }
        }

        #endregion

        private void txtPhongID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvPhong.SelectedCells.Count > 0)
            {
                string tenloaiphong = (string)dtgvPhong.SelectedCells[0].OwningRow.Cells["tenloaiphong"].Value;


                cbLoaiPhong.Text = tenloaiphong;

            }
        }

        private void dtgvKhachhang_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtIDKhachhang.Text = dtgvKhachhang.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtHotenkhachhang.Text = dtgvKhachhang.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtNgaysinhkhachhang.Text = dtgvKhachhang.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtGioitinhkhachhang.Text = dtgvKhachhang.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDienthoaikhachhang.Text = dtgvKhachhang.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDiachikhachhang.Text = dtgvKhachhang.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtEmailkhachhang.Text = dtgvKhachhang.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtCCCDkhachhang.Text = dtgvKhachhang.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        List<Khachhang> searchKhachHangByCCCD(string cccd)
        {
            List<Khachhang> listKH = new List<Khachhang>();

            listKH = KhachhangDAO.Instance.SearchKhachHangByCCCD(cccd);

            return listKH;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string valueSearch = txtCCCDSearch.Text;
            dtgvKhachhang.DataSource = searchKhachHangByCCCD(valueSearch);
        }

        private void btnXoaKhachhang_Click(object sender, EventArgs e)
        {
            int idKhach = Convert.ToInt32(txtIDKhachhang.Text);
            string ten = txtHotenkhachhang.Text;
            if (MessageBox.Show("Bạn có thự sự muốn xóa khách hàng: " + ten + "", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (KhachhangDAO.Instance.deleteKhachhang(idKhach))
                {
                    MessageBox.Show("Xóa thành công");
                    LoadListKhachhang();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
                }
            }

        }

        private void btnSuakhachhang_Click(object sender, EventArgs e)
        {
            string name = txtHotenkhachhang.Text;
            string ngaysinh = txtNgaysinhkhachhang.Text;
            string sdt = txtDienthoaikhachhang.Text;
            string email = txtEmailkhachhang.Text;
            string diachi = txtDiachikhachhang.Text;
            string cccd = txtCCCDkhachhang.Text;
            string gioitinh = txtGioitinhkhachhang.Text;
            string idkh = txtIDKhachhang.Text;

            if(KhachhangDAO.Instance.updateKhachhang(name, ngaysinh, sdt, email, diachi, cccd, gioitinh, idkh))
            {
                MessageBox.Show("Cập nhật thành công");
                LoadListKhachhang();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo");
            }


        }
    }
}

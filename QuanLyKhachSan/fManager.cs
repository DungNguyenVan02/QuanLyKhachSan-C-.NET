using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.XtraPrinting.Native.Properties;
using QuanLyKhachSan.DAO;
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class fManager : Form
    {
        private ACcount loginAccount;

        public ACcount LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount.Type);
            }
        }

        public fManager(ACcount acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;

            LoadRoom();
            loadDichVu();
            loadLoaiPhong();
        }
        private void thuêPhòngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fDatPhong f = new fDatPhong();
            this.Hide();
            f.ShowDialog();
            this.Show();
            LoadRoom();
        }

        private void TTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAcountProfile f = new fAcountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }

        private void f_UpdateAccount(object sender, AccountEvent e)
        {
            hệThốngToolStripMenuItem.Text = "Hệ thống (" + e.Acc.FullName + ")";
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount= LoginAccount;
            f.InsertDichVu += f_InsertDichVu;
            f.DeleteDichVu += f_DeleteDichVu;
            f.UpdateDichVu += f_UpdateDichVu;
            f.ShowDialog();
        }

        private void f_UpdateDichVu(object sender, EventArgs e)
        {
            int idRoom = 0;
            List<Bill> listBill = BillDAO.Instance.GetBillByIdDichVu((cbDichVu.SelectedItem as DichVu).ID_DichVu);
            foreach (Bill item in listBill)
            {
                idRoom = item.ID_phong;
                showHoaDon(idRoom);
                LoadRoom();
                loadDichVu();

            }
        }

        private void f_DeleteDichVu(object sender, EventArgs e)
        {
            int idRoom = 0;
            List<Bill> listBill = BillDAO.Instance.GetBillByIdDichVu((cbDichVu.SelectedItem as DichVu).ID_DichVu);
            foreach (Bill item in listBill)
            {
                idRoom = item.ID_phong;
                showHoaDon(idRoom);
                LoadRoom();
                loadDichVu();

            }
        }

        private void f_InsertDichVu(object sender, EventArgs e)
        {
            int idRoom = 0;
            List<Bill> listBill = BillDAO.Instance.GetBillByIdDichVu((cbDichVu.SelectedItem as DichVu).ID_DichVu);
            foreach (Bill item in listBill)
            {
                idRoom = item.ID_phong;
                showHoaDon(idRoom);
                LoadRoom();
                loadDichVu();

            }
        }




        #region Method

        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            hệThốngToolStripMenuItem.Text += "(" + LoginAccount.FullName + ")";

        }

        void loadDichVu()
        {
            List<DichVu> listDichVu = DichVuDAO.Instance.GetDichVuList();
            cbDichVu.DataSource = listDichVu;
            cbDichVu.DisplayMember = "tendichvu";
        }



        void loadLoaiPhong()
        {
            List<CategoryRoom> listCategoryRoom = CategoryRoomDAO.Instance.GetListCategoryRoom();
            cbLoaiPhong.DataSource = listCategoryRoom;
            cbLoaiPhong.DisplayMember = "tenloaiphong";
        }

        private void cbLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            CategoryRoom selected = cb.SelectedItem as CategoryRoom;
            string tenloaiphong = selected.TenLoaiPhong;
            loadPhongByLoaiPhong(tenloaiphong);
        }
        void loadPhongByLoaiPhong(string id)
        {
            List<Room> litRoom = GetRoomByIdDAO.Instance.GetRoomByCategory(id);
            cbChuyenPhong.DataSource = litRoom;
            cbChuyenPhong.DisplayMember = "tenphong";
        }

        void LoadRoom()
        {
            flpPhong.Controls.Clear();
            List<Room> roomList = RoomDAO.Instance.LoadRoomList();



            foreach (Room item in roomList)
            {
                Button btn = new Button()
                {
                    Width = RoomDAO.RoomWidth,
                    Height = RoomDAO.RoomHeight,
                };

                btn.Click += Btn_Click;
                btn.Tag = item;

                btn.Text = item.TenPhong + "\n" + item.TrangThai;

                if (item.TrangThai == "Trống")
                {
                    btn.BackColor = Color.DeepSkyBlue;
                }
                else
                {
                    btn.BackColor = Color.PaleVioletRed;
                }

                flpPhong.Controls.Add(btn);
            }
        }

        void showHoaDon(int id)
        {
            listViewHoaDon.Items.Clear();
            List<DTO.Menu> menuList = MenuDAO.Instance.GetListMenuByRoom(id);
            float giaPhong = 0;
            float soNgayThue = 0;
            foreach (DTO.Menu item in menuList)
            {

                ListViewItem lsvItem = new ListViewItem(item.MaKhachHang.ToString());
                lsvItem.SubItems.Add(item.TenKhachHang.ToString());
                lsvItem.SubItems.Add(item.TenPhong.ToString());
                lsvItem.SubItems.Add(item.TenLoaiPhong.ToString());
                lsvItem.SubItems.Add(item.GiaPhong.ToString());
                lsvItem.SubItems.Add(item.SoNgay.ToString());
                lsvItem.SubItems.Add(item.TongTien.ToString());

                soNgayThue = (float)Convert.ToDouble(item.SoNgay);

                giaPhong = item.GiaPhong;

                listViewHoaDon.Items.Add(lsvItem);
            }
            //(float)Convert.ToDouble(row["giaphong"].ToString());



            float tongtienphong = soNgayThue * giaPhong;

            txtTongTienPhong.Text = tongtienphong.ToString();
        }

        void showDichVu(int id)
        {
            listViewDichVu.Items.Clear();
            List<MenuDichVu> menuList = GetDichVuById.Instance.GetListDVByIdRoom(id);

            float tongtien = 0;

            foreach (MenuDichVu menu in menuList)
            {
                tongtien += menu.GiaDichVu;
            }

            foreach (MenuDichVu item in menuList)
            {
                ListViewItem lsvItem = new ListViewItem(item.TenDichVu.ToString());
                lsvItem.SubItems.Add(item.GiaDichVu.ToString());
                lsvItem.SubItems.Add(item.IDPhong.ToString());


                listViewDichVu.Items.Add(lsvItem);
            }


            txtTongTienDV.Text = tongtien.ToString();
        }



        #endregion


        #region Events

        private void Btn_Click(object sender, EventArgs e)
        {
            int roomId = ((sender as Button).Tag as Room).ID_Phong;
            listViewHoaDon.Tag = (sender as Button).Tag;
            
            showHoaDon(roomId);
            showDichVu(roomId);

            if (BillDAO.Instance.GetCheckIdRoom(roomId))
            {
                btnDatDichVu.Enabled = false;
                btnThanhToan.Enabled = false;
            }
            else
            {
                btnDatDichVu.Enabled = true;
                btnThanhToan.Enabled = true;
            }

            float totalRoom = (float)Convert.ToDouble(txtTongTienPhong.Text);
            float totalDV = (float)Convert.ToDouble(txtTongTienDV.Text);
            //CultureInfo culture = new CultureInfo("vi-VN");
            //txtThanhTien.Text = (totalRoom + totalDV).ToString("c", culture);
            txtThanhTien.Text = (totalRoom + totalDV).ToString();
        }


        private void btnDatDichVu_Click(object sender, EventArgs e)
        {


            Room room = listViewHoaDon.Tag as Room;

            int idRoom = room.ID_Phong;

            List<Bill> billList = BillDAO.Instance.GetBillByIdRoom(idRoom);

            int idDichvu = DichVuDAO.Instance.GetIdDichVu(cbDichVu.Text);
            string ngayvao = billList[0].NgayVao.ToString();
            string ngaytra = billList[0].NgayTra.ToString();
            int idKhachhang = billList[0].ID_khachhang;

            int isThem = BillDAO.Instance.GetCheckIdKhachBill(idKhachhang);


            if (isThem != -1)
            {
                BillDAO.Instance.InsertBill(idKhachhang, idRoom, idDichvu, ngayvao, ngaytra);
            }

            showHoaDon(idRoom);
            showDichVu(idRoom);
            float totalRoom = (float)Convert.ToDouble(txtTongTienPhong.Text);
            float totalDV = (float)Convert.ToDouble(txtTongTienDV.Text);
            //CultureInfo culture = new CultureInfo("vi-VN");
            //txtThanhTien.Text = (totalRoom + totalDV).ToString("c", culture);
            txtThanhTien.Text = (totalRoom + totalDV).ToString();
        }


        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            Room room = listViewHoaDon.Tag as Room;
            int idRoom = room.ID_Phong;
            
            List<Bill> billList = BillDAO.Instance.GetBillByIdRoom(idRoom);
            int idKhachhang = billList[0].ID_khachhang;

            int isThem = BillDAO.Instance.GetCheckIdKhachBill(idKhachhang);

            float tongtien = float.Parse(txtThanhTien.Text);


            if (isThem != -1)
            {
                if (MessageBox.Show("Bạn có thự sự muốn thanh toán cho phòng: " + room.TenPhong, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idRoom, tongtien);
                    showHoaDon(idRoom);
                    showDichVu(idRoom);
                    txtThanhTien.Clear();
                    LoadRoom();
                }
            }
        }

        private void btnChuyenPhong_Click(object sender, EventArgs e)
        {

            int idRoom1 = (listViewHoaDon.Tag as Room).ID_Phong;
            RoomDAO.Instance.UpdateStateWhenSwitch(idRoom1);

            int idRoom2 = (cbChuyenPhong.SelectedItem as Room).ID_Phong;
            if (MessageBox.Show(string.Format("Bạn có thật sự chuyển phòng từ {0} sang phòng {1}", (listViewHoaDon.Tag as Room).TenPhong, (cbChuyenPhong.SelectedItem as Room).TenPhong), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {

                RoomDAO.Instance.SwitchRoom(idRoom1, idRoom2);
                LoadRoom();
            }

        }


        private void thanhToanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnThanhToan_Click(this, new EventArgs());
        }

        #endregion

        

        
    }
}

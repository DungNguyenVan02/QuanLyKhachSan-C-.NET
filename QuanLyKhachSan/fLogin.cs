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


namespace QuanLyKhachSan
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text;
            string passWord = txtPassword.Text;
            if(string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản của bạn!");
                return;
            }
            if (string.IsNullOrEmpty(passWord))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu của bạn!");
                return;
            }
            if (login(userName, passWord))
            {
                ACcount loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
                fManager f = new fManager(loginAccount);
                this.Hide();
                f.ShowDialog();
                this.Show();
            } else
            {
                MessageBox.Show("sai tài khoản hoặc mật khẩu!");
            }
        }
        bool login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (
                MessageBox.Show("Bạn có thực sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel)
                != System.Windows.Forms.DialogResult.OK
             )
            {
                e.Cancel = true;
            }
        }
    }
}

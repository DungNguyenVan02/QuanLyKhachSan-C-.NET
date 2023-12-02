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
    public partial class fAcountProfile : Form
    {
        private ACcount loginAccount;
        internal Action<object, EventArgs> updateAcount;

        public ACcount LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount);
            }
        }
        public fAcountProfile(ACcount acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        void ChangeAccount(ACcount acc)
        {
            txtUsername.Text = LoginAccount.UserName;
            txtFullname.Text = LoginAccount.FullName;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }
             
             
        void UpdateAccountInfo()
        {   
            string username = txtUsername.Text.Trim();
            string fullname = txtFullname.Text.Trim();
            string password = txtPassword.Text.Trim();
            string newpassword = txtNewPass.Text.Trim();
            string passwordConfirm = txtPassConfirm.Text.Trim();

            if(!newpassword.Equals(passwordConfirm))
            {
                MessageBox.Show("Mật khẩu nhập lại chưa chính xác", "Thông Báo", MessageBoxButtons.OK);
            } else
            {
                if(AccountDAO.Instance.UpdateAccount(username, fullname, password, newpassword))
                {
                    MessageBox.Show("Cập nhật thành công", "Thông Báo", MessageBoxButtons.OK);
                    if(updateAccount != null)
                    {
                        updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(username)));
                    }
                    txtPassword.Clear();
                    txtNewPass.Clear();
                    txtPassConfirm.Clear();
                } else
                {
                    MessageBox.Show("Mật khẩu chưa chính xác", "Thông Báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }
    }
    
    public class AccountEvent:EventArgs
    {
        private ACcount acc;

        public ACcount Acc {
            get { return acc; }
            set { acc = value; }
        }

        public AccountEvent(ACcount acc)
        {
            this.Acc = acc;
        }

    }
}

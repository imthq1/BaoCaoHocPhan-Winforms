using BUS;
using DAL;
using GUI.GUI.User_HocSinh;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Login : Form
    {
        private readonly TaiKhoanServices taiKhoanServices = new TaiKhoanServices();
        public Login()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                                                  "Xác nhận thoát",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            var user = taiKhoanServices.Login(txtUsername.Text, txtPassword.Text);
            if (user != null)
            {
                Form formToDisplay=null;
                string TenQuyen = taiKhoanServices.GetTenQuyen(user);
                switch (TenQuyen)
                {
                    case "Admin":
                        formToDisplay = new AdminQuanLy();
                        break;
                    case "User":
                        formToDisplay = new UserMain(user);
                        break;
                    default:
                        throw new ArgumentException($"Role không hợp lệ: {TenQuyen}");
                }
                this.Hide();
                formToDisplay.Closed += (s, args) => this.Show();
                formToDisplay.Show();
            }
            else
            {
                MessageBox.Show("Sai mật khẩu hoặc tài khoản!");
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        
    }
}

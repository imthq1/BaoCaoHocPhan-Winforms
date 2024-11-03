using BUS;
using DAL;
using GUI.GUI.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.GUI.User_HocSinh
{
    public partial class UserMain : Form
    {   
        private TaiKhoanServices taiKhoanServices=new TaiKhoanServices();
        private readonly MonHocServices monHocServices = new MonHocServices();
        public TaiKhoan user;
        public UserMain(TaiKhoan tk)
        {
            InitializeComponent();
            user = tk;
        }
        private Image ConvertByteToImage(byte[] photo)
        {
            Image newImage;
            if (photo == null)
            {
                return null;
            }
            using (MemoryStream ms = new MemoryStream(photo, 0, photo.Length))
            {
                ms.Write(photo, 0, photo.Length);
                newImage = Image.FromStream(ms, true);
            }
            return newImage;
        }
        private void UserMain_Load(object sender, EventArgs e)
        {
            LoadAvatar(user.sMssv);
            lbHoTen.Text = user.sHoTen;
            lbGioiTinh.Text = user.sGioiTinh;
            lbMssv.Text = user.sMssv;
            lbNgaySinh.Text = user.dNgaySinh.HasValue ? user.dNgaySinh.Value.ToString("dd 'tháng' MM 'năm' yyyy") : "Chưa xác định";
            picAvatar.Image = ConvertByteToImage(user.AvatarID);
            var listMonHoc = monHocServices.GetMonHocCuaKhoa(user.sMaKhoa);

            if (listMonHoc.Count == 0)
            {
                MessageBox.Show("Không có môn học nào được tìm thấy.");
                return;
            }

            for (int i = 0; i < listMonHoc.Count; i++)
            {
                var currentMonHoc = listMonHoc[i];
                Label label = new Label
                {
                    Text = currentMonHoc.sTenMonHoc,
                    Location = new Point(10, 30 * i),
                    Font = new Font("Arial", 14),
                    AutoSize = true
                };

                label.MouseEnter += (s, args) =>
                {
                    label.ForeColor = Color.Blue;
                };

                label.MouseLeave += (s, args) =>
                {
                    label.ForeColor = Color.Black;
                };

                label.Click += (s, args) =>
                {
                    Form formToDisplay = new QuizzQuesstion(currentMonHoc.sMaMonHoc,user.sHoTen);
                    this.Hide();
                    formToDisplay.Closed += (s2, args2) =>
                    {
                        this.Show();
                    };
                    formToDisplay.Show();
                };

                Panel.Controls.Add(label);
            }

        }
        private void LoadAvatar(string studentID)
        {
           
        }
        private void lbMssv_Click(object sender, EventArgs e)
        {

        }

        private void UserMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
    }
}

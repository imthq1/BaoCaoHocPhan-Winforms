using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using BCrypt.Net;
namespace GUI.GUI.Admin
{
    public partial class AdminAdd : Form
    {
        private readonly KhoaServices khoaServices = new KhoaServices();
        private readonly TaiKhoanServices taiKhoanServices = new TaiKhoanServices();
        private string avatarFilePath=null;
        byte[] avatar = null;
        public AdminAdd()
        {
            InitializeComponent();
        }

        private void AdminAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        private void FillKhoaCombobox(List<Khoa> list)
        {
            this.cmbKhoa.DataSource = list;
            this.cmbKhoa.DisplayMember = "sTenKhoa";
            this.cmbKhoa.ValueMember = "sMaKhoa";
        }
        private void AdminAdd_Load(object sender, EventArgs e)
        {
            dTime.CustomFormat = "dd 'tháng' MM 'năm' yyyy";
            dTime.Format = DateTimePickerFormat.Custom;
            try
            {
                List<Khoa> listKhoa = khoaServices.GetAll();
                FillKhoaCombobox(listKhoa);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private byte[] ConvertFileToByte(string path)
        {
            byte[] data = null;
            FileInfo fIno = new FileInfo(path);
            long numBytes = fIno.Length;
            FileStream fileStream = new FileStream(path,FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader br = new BinaryReader(fileStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtMssv.Text, @"^\d+$"))
            {
                MessageBox.Show("MSSV phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(txtPassword.Text, @"^[a-z0-9]+$"))
            {
                MessageBox.Show("Mật khẩu chỉ được chứa ký tự từ a-z và 0-9!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtGioiTinh.Text != "Nam" && txtGioiTinh.Text != "Nữ")
            {
                MessageBox.Show("Giới tính phải là 'Nam' hoặc 'Nữ'!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);

            if (!string.IsNullOrEmpty(this.picAvatar.ImageLocation))
            {
                avatar = ConvertFileToByte(this.picAvatar.ImageLocation);
            }
            else
            {
                avatar = null;
            }

            bool result = taiKhoanServices.AddTaiKhoan(
                txtMssv.Text, hashedPassword, txtName.Text,
                cmbKhoa.Text, dTime.Value, txtGioiTinh.Text, 1, avatar);

            if (result)
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                avatarFilePath = null;
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string SaveAvatar(string sourceFilePath, string studentID)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fileExtension = Path.GetExtension(sourceFilePath);
                string targetFilePath = Path.Combine(folderPath, $"{studentID}{fileExtension}");

                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Không tìm thấy file: {sourceFilePath}");
                }

                File.Copy(sourceFilePath, targetFilePath, true);

                return $"{studentID}{fileExtension}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving avatar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
        "Bạn có chắc chắn muốn xóa avatar này?",
        "Xác nhận xóa",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                picAvatar.Image = null;  
                avatarFilePath = null;    
                avatar = null;            
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files ( *. jpg; *. jpeg; *. png) | *. jpg; *. jpeg; *. png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    this.picAvatar.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

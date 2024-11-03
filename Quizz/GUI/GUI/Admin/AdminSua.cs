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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class AdminSua : Form
    {
        private readonly KhoaServices khoaServices = new KhoaServices();
        private readonly TaiKhoanServices taiKhoanServices = new TaiKhoanServices();
        private string mssv;
        private string avatarFilePath=null;
        private byte[] avatar=null;
        private bool isAvatarRemoved = false;
        public AdminSua(string smssv)
        {
            InitializeComponent();
            mssv = smssv;
        }

        private Image ConvertByteToImage(byte[] photo)
        {
            Image newImage;
            if (photo == null)
            {
                return null;
            }
            using (MemoryStream ms = new MemoryStream(photo,0, photo.Length))
            {
                ms.Write(photo, 0, photo.Length);
                newImage = Image.FromStream(ms,true);
            }
            return newImage;
        }
        private byte[] ConvertFileToByte(string path)
        {
            byte[] data = null;
            FileInfo fIno = new FileInfo(path);
            long numBytes = fIno.Length;
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader br = new BinaryReader(fileStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }
        private byte[] ConvertImageToByte(Image image)
        {
            if (image==null)
                return null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();            
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mssv))
            {
                MessageBox.Show("MSSV không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (isAvatarRemoved)
            {
                avatar = null;
            }
            else if (!string.IsNullOrEmpty(avatarFilePath))
            {
                avatar = ConvertFileToByte(avatarFilePath);
            }
            else
            {
                TaiKhoan currentUser = taiKhoanServices.GetUser(mssv);
                avatar = currentUser.AvatarID;
            }
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);
            var newTaiKhoan = new TaiKhoan
            {
                sMssv = txtMssv.Text,
                sMatKhau = hashPassword,
                sHoTen = txtName.Text,
                dNgaySinh = dTime.Value,
                sGioiTinh = txtGioiTinh.Text,
                sMaKhoa = khoaServices.NameToMa(cmbKhoa.Text),
                iMaQuyen = 1,
                AvatarID = avatar
            };

            bool result = taiKhoanServices.SuaTaiKhoan(mssv, newTaiKhoan);

            if (result)
            {
                MessageBox.Show("Sửa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa tài khoản thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void AdminSua_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        private void FillKhoaCombobox(List<Khoa> list)
        {
            this.cmbKhoa.DataSource = list;
            this.cmbKhoa.DisplayMember = "sTenKhoa";
            this.cmbKhoa.ValueMember = "sMaKhoa";
        }
        private void AdminSua_Load(object sender, EventArgs e)
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
            
            TaiKhoan user = taiKhoanServices.GetUser(mssv);
            if (user != null)
            {
                txtMssv.Text = user.sMssv;
                txtPassword.Text = user.sMssv;
                txtName.Text = user.sHoTen;
                dTime.Value = user.dNgaySinh.Value;
                txtGioiTinh.Text = user.sGioiTinh;
                cmbKhoa.Text = khoaServices.MatoName(user.sMaKhoa);
                this.picAvatar.Image = ConvertByteToImage(user.AvatarID);
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }


        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png) | *.jpg; *.jpeg; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avatarFilePath = openFileDialog.FileName; 
                    this.picAvatar.ImageLocation = avatarFilePath;
                }
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
                isAvatarRemoved = true; 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

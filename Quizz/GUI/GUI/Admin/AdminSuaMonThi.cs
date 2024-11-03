using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI.GUI.Admin
{
    public partial class AdminSuaMonThi : Form
    {
        private string maMh;
        private KhoaServices khoaServices=new KhoaServices();
        private MonHocServices monHocServices=new MonHocServices();
        public AdminSuaMonThi(string Ma)
        {
            InitializeComponent();
            this.maMh = Ma;
        }

        private void FillKhoaCombobox(List<Khoa> list)
        {
            this.cmbKhoa.DataSource = list;
            this.cmbKhoa.DisplayMember = "sTenKhoa";
            this.cmbKhoa.ValueMember = "sMaKhoa";
        }
        private void AdminSuaMonThi_Load(object sender, EventArgs e)
        {
            try
            {

                var MonHoc = monHocServices.getMonHocById(maMh);
                if (MonHoc != null)
                {
                    txtMaMH.Text = MonHoc.sMaMonHoc;
                    txtTenMH.Text = MonHoc.sTenMonHoc;
                    cmbKhoa.Text = khoaServices.MatoName(MonHoc.sMaKhoa);
                    txtTime.Text = MonHoc.tGianThi.ToString();     
                }
                else {
                    MessageBox.Show("Không tìm thấy sinh viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                List<Khoa> listKhoa = khoaServices.GetAll();
                FillKhoaCombobox(listKhoa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maMh))
            {
                MessageBox.Show("Mã môn thi không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var monHoc = new MonHoc
            {
                sMaMonHoc=txtMaMH.Text,
                sTenMonHoc=txtTenMH.Text,
                tGianThi= TimeSpan.Parse(txtTime.Text),
                sMaKhoa= khoaServices.NameToMa(cmbKhoa.Text)
            };
            bool result = monHocServices.AdminSuaMonHoc(maMh, monHoc);

            if (result)
            {
                MessageBox.Show("Sửa môn thi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa môn thi thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

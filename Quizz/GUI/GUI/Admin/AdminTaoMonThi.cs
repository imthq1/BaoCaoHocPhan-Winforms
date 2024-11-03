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
    public partial class AdminTaoMonThi : Form
    {
        private KhoaServices khoaServices=new KhoaServices();
        private MonHocServices monHocServices=new MonHocServices();
        public AdminTaoMonThi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool result = monHocServices.TaoMonHoc(
                txtMaMH.Text,txtTenMH.Text,cmbKhoa.Text,TimeSpan.Parse(txtTime.Text));

            if (result)
            {
                MessageBox.Show("Thêm môn thi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm môn thi thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FillKhoaCombobox(List<Khoa> list)
        {
            this.cmbKhoa.DataSource = list;
            this.cmbKhoa.DisplayMember = "sTenKhoa";
            this.cmbKhoa.ValueMember = "sMaKhoa";
        }
        private void AdminTaoMonThi_Load(object sender, EventArgs e)
        {
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

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaMH_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

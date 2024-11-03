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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace GUI.GUI.Admin
{
    public partial class AdminAddCauHoi : Form
    {
        private readonly MonHocServices monHocServices = new MonHocServices();
        private readonly CauHoiServices cauHoiServices = new CauHoiServices();
        string id;
        private void AdminAddCauHoi_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        public AdminAddCauHoi(string monhocid)
        {
            InitializeComponent();
            id = monhocid;

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int GetSelectedRadioButton()
        {
            if (radioA.Checked)
                return 1;
            else if (radioB.Checked)
                return 2;
            else if (radioC.Checked)
                return 3;
            else if (radioD.Checked)
                return 4;

            return 0;
        }

        private void AdminAddCauHoi_Load(object sender, EventArgs e)
        {
            lb_NameMonHoc.Text = monHocServices.GetTenMonHoc(id);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var cauHoi = new CauHoi
            {
                sNoiDungCauHoi = txtNoiDung.Text,
                sCauTraLoi1 = txtA.Text,
                sCauTraLoi2 = txtB.Text,
                sCauTraLoi3 = txtC.Text,
                sCauTraLoi4 = txtD.Text,
                iDapAn = GetSelectedRadioButton(),
                sMaMonHoc = id
            };
            bool result = cauHoiServices.AdminAddCauHoi(cauHoi);

            if (result)
            {
                MessageBox.Show("Thêm câu hỏi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm câu hỏi thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

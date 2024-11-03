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

namespace GUI.GUI.Admin
{
    public partial class AdminSuaCauHoi : Form
    {
        private readonly MonHocServices monHocServices = new MonHocServices();
        private readonly CauHoiServices cauHoiServices = new CauHoiServices();
        string id;
        public AdminSuaCauHoi(string monhocid)
        {
            InitializeComponent();
            id = monhocid;
            lb_NameMonHoc.Text = monhocid;
            //lb_NameMonHoc.Text = monHocServices.GetTenMonHoc(monhocid);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminSuaCauHoi_FormClosed(object sender, EventArgs e)
        {
            this.Dispose();
        }
        void TickBox(int? i)
        {
            switch (i)
            {
                case 1:
                    radioA.Checked = true;
                    break;
                case 2:
                    radioB.Checked = true;
                    break;
                case 3:
                    radioC.Checked = true;
                    break;
                case 4:
                    radioD.Checked = true;
                    break;
                default:
                    MessageBox.Show("Giá trị không hợp lệ");
                    break;
            }
        }
        private void AdminSuaCauHoi_Load(object sender, EventArgs e)
        {
            CauHoi cauHoi = cauHoiServices.GetCauHoi(id);
            if (cauHoi != null)
            {
                txtNoiDung.Text = cauHoi.sNoiDungCauHoi;
                txtA.Text = cauHoi.sCauTraLoi1;
                txtB.Text = cauHoi.sCauTraLoi2;
                txtC.Text = cauHoi.sCauTraLoi3;
                txtD.Text = cauHoi.sCauTraLoi4;
                TickBox(cauHoi.iDapAn);
                lb_NameMonHoc.Text = monHocServices.GetTenMonHoc(cauHoi.sMaMonHoc);
            }
            else
            {
                MessageBox.Show("Không tìm thấy câu hỏi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
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
        private void btnEdit_Click(object sender, EventArgs e)
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
            bool result = cauHoiServices.AdminSuaCauHoi(id, cauHoi);

            if (result)
            {
                MessageBox.Show("Sửa câu hỏi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa câu hỏi thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

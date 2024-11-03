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

namespace GUI.GUI.User_HocSinh
{
    public partial class DapAn : Form
    {
        public List<int> list;
        string name;
        DiemService diemService;
        MonHocServices monHocService;
        MonHoc monHoc;
        public DapAn(List<int> l,string tenUser,MonHoc monHoc)
        {
            InitializeComponent();
            list = l;
            name = tenUser;
            diemService = new DiemService();
            this.monHoc = monHoc;
        }

        private void DapAn_Load(object sender, EventArgs e)
        {
            int dapandung = 0;
            int dapansai = 0;
            int tongsocau = 0;
            groupbox.Controls.Clear();
            int groupBoxWidth = groupbox.Width;
            int groupBoxHeight = groupbox.Height;
            int currentYPosition = 20;
            int currentXPosition = 10;
            int radioButtonWidth = 80;
            int spacing = 10;
            for (int i = 0; i < list.Count; i++)
            {
                tongsocau++;
                RadioButton rb = new RadioButton
                {
                    Text = "Câu " + (i + 1),
                    Checked = list[i] == 1, 
                    AutoCheck = false
                };
                if (rb.Checked == true)
                {
                    dapandung++;
                }
                else
                {
                    dapansai++;
                }
                if (currentXPosition + radioButtonWidth > groupBoxWidth - spacing)
                {
                    currentYPosition += 30;
                    currentXPosition = 10;
                }
                rb.Location = new Point(currentXPosition, currentYPosition);
                rb.Size = new Size(radioButtonWidth, 20);
                groupbox.Controls.Add(rb);
                currentXPosition += radioButtonWidth + spacing;
            }

            groupbox.Refresh();
            lbDapAnDung.Text = dapandung.ToString();
            lbDapAnSai.Text = dapansai.ToString();
            if (tongsocau != 0)
            {
                double diem = ((double)dapandung / tongsocau) * 10;
                lbDapAnDiem.Text = diem.ToString("0.##"); 
                diemService.AddDiem(name, monHoc.sMaMonHoc,diem);
            }
            else
            {
                lbDapAnDiem.Text = "Không có 0 câu hỏi";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

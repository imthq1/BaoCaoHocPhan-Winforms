using BUS;
using DAL;
using GUI.GUI.Admin;
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
    public partial class QuizzQuesstion : Form
    {
        bool blockclose = true;
        MonHocServices monHocServices = new MonHocServices();
        CauHoiServices cauHoiServices = new CauHoiServices();
        List<CauHoi> list;
        private TimeSpan remainingTime;

        string id;
        string name;
        public QuizzQuesstion(string mamonhoc,string tenUser)
        {
            InitializeComponent();
            id = mamonhoc;
            name = tenUser;
        }
        private void QuizzQuesstion_Load(object sender, EventArgs e)
        {
            list = cauHoiServices.GetAllInMonHoc(id);
            int spacing = 10;
            int labelSpacing = 20;
            int currentYPosition = 10;
            var MonHoc = monHocServices.getMonHocById(id);


            if (MonHoc.tGianThi.HasValue)
            {
                remainingTime = MonHoc.tGianThi.Value; 
            }
            else
            {
                remainingTime = TimeSpan.Zero; 
            }

            label2.Text = remainingTime.ToString(@"hh\:mm\:ss");

            timer1.Interval = 1000;
            timer1.Start();

            // Tạo các GroupBox cho câu hỏi
            for (int i = 0; i < list.Count; i++)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Text = "Câu Hỏi " + (i + 1).ToString();
                groupBox.Location = new Point(10, currentYPosition);
                groupBox.Size = new Size(900, 120);

                Label lblNoiDung = new Label();
                lblNoiDung.Text = "Nội dung: " + list[i].sNoiDungCauHoi;
                lblNoiDung.Location = new Point(10, 20);
                lblNoiDung.AutoSize = true;
                groupBox.Controls.Add(lblNoiDung);
                for (int j = 1; j <= 4; j++)
                {
                    RadioButton rbCauTraLoi = new RadioButton();
                    rbCauTraLoi.Text = (j == 1 ? "A: " + list[i].sCauTraLoi1 :
                                        j == 2 ? "B: " + list[i].sCauTraLoi2 :
                                        j == 3 ? "C: " + list[i].sCauTraLoi3 :
                                                 "D: " + list[i].sCauTraLoi4);
                    rbCauTraLoi.Location = new Point(10, 40 + (j - 1) * labelSpacing);
                    
                    rbCauTraLoi.AutoSize = true;
                    groupBox.Controls.Add(rbCauTraLoi);
                }

                int requiredHeight = 40 + (4 * labelSpacing);
                groupBox.Height = Math.Max(groupBox.Height, requiredHeight + 20);
                currentYPosition += groupBox.Height + spacing;
                Panel.Controls.Add(groupBox);
            }
            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem submitMenuItem = new ToolStripMenuItem("Nộp Bài");
            submitMenuItem.Font = new Font("Arial", 10);
            submitMenuItem.Click += BtnSubmit_Click; 
            menuStrip.Items.Add(submitMenuItem);
            this.MainMenuStrip = menuStrip;
            menuStrip.Location = new Point(0, this.ClientSize.Height - menuStrip.Height);
            menuStrip.Dock = DockStyle.Bottom; 
            this.Controls.Add(menuStrip);
            Panel.Refresh();

        }
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            MonHoc monHoc = monHocServices.getMonHocById(id);
            List<int> results = new List<int>();

            foreach (GroupBox groupBox in Panel.Controls.OfType<GroupBox>())
            {
                int selectedIndex = -1;
                for (int j = 0; j < groupBox.Controls.OfType<RadioButton>().Count(); j++)
                {
                    RadioButton rb = (RadioButton)groupBox.Controls.OfType<RadioButton>().ElementAt(j);
                    if (rb.Checked)
                    {
                        selectedIndex = j + 1; 
                        break;
                    }
                }
                int questionIndex = int.Parse(groupBox.Text.Split(' ')[2]) - 1;
                int correctAnswer = (int)list[questionIndex].iDapAn;

                if (selectedIndex != -1)
                {
                    results.Add(selectedIndex == correctAnswer ? 1 : 0);
                }
                else
                {
                    results.Add(0);
                }
            }
            Form formToDisplay = new DapAn(results,name,monHoc);
            formToDisplay.Closed += (s, args) =>
            {
                blockclose = false;
                this.Close();
                formToDisplay.Dispose();
            };
            this.Hide();
            formToDisplay.Show();
        }

        private void QuizzQuesstion_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void QuizzQuesstion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (blockclose == true)
            {
                this.BtnSubmit_Click(sender, e);
                e.Cancel = true;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));

            // Hiển thị thời gian dưới dạng hh:mm:ss
            label2.Text = remainingTime.ToString(@"hh\:mm\:ss");

            // Dừng Timer và thông báo khi hết giờ
            if (remainingTime.TotalSeconds <= 0)
            {
                timer1.Stop();
                label2.Text = "Time's up!";
                this.BtnSubmit_Click(sender, e); 
            }
        }
    }
}

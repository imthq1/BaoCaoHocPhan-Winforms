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
using System.IO;
using ClosedXML.Excel;

namespace GUI.GUI.Admin
{
    public partial class AdminTaoCauHoi : Form
    {
        private readonly KhoaServices khoaServices = new KhoaServices();
        private readonly MonHocServices monHocServices = new MonHocServices();
        private readonly TaiKhoanServices taiKhoanServices = new TaiKhoanServices();
        private readonly CauHoiServices cauHoiServices = new CauHoiServices();
        public AdminTaoCauHoi()
        {
            InitializeComponent();
        }

        private void AdminTaoCauHoi_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        private void FillKhoaCombobox(List<Khoa> list)
        {
            this.cmbKhoa.DataSource = list;
            this.cmbKhoa.DisplayMember = "sTenKhoa";
            this.cmbKhoa.ValueMember = "sMaKhoa";
        }
        private void FillMonHocCombobox(List<MonHoc> list)
        {
            this.cmbMonHoc.DataSource = list;
            this.cmbMonHoc.DisplayMember = "sTenMonHoc";
            this.cmbMonHoc.ValueMember = "sMaMonHoc";
        }
        string DapAnToString(int? i)
        {
            switch (i)
            {
                case 1:
                    return "A";
                    break;
                case 2:
                    return "B";
                    break;
                case 3:
                    return "C";
                    break;
                case 4:
                    return "D";
                    break;
                default:
                    return "Error!";
                    break;
            }
        }
        private void BindGrid(List<CauHoi> list)
        {
            dgv.Rows.Clear();
            foreach (var item in list)
            {
                int index = dgv.Rows.Add();
                dgv.Rows[index].Cells[0].Value = item.iMaCauHoi;
                dgv.Rows[index].Cells[1].Value = item.sNoiDungCauHoi;
                dgv.Rows[index].Cells[2].Value = item.sCauTraLoi1;
                dgv.Rows[index].Cells[3].Value = item.sCauTraLoi2;
                dgv.Rows[index].Cells[4].Value = item.sCauTraLoi3;
                dgv.Rows[index].Cells[5].Value = item.sCauTraLoi4;
                dgv.Rows[index].Cells[6].Value = DapAnToString(item.iDapAn);


            }
        }
        void LoadData()
        {
            try
            {
                MonHoc selectedMonHoc = cmbMonHoc.SelectedItem as MonHoc;
                if (selectedMonHoc != null)
                {
                    List<CauHoi> list = cauHoiServices.GetAllInMonHoc(selectedMonHoc.sMaMonHoc);
                    BindGrid(list);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AdminTaoCauHoi_Load(object sender, EventArgs e)
        {
            Misc.setGridViewStyle(dgv);
            try
            {
                List<Khoa> listKhoa = khoaServices.GetAll();
                FillKhoaCombobox(listKhoa);
                Khoa selectedKhoa = cmbKhoa.SelectedItem as Khoa;
                if (selectedKhoa != null)
                {
                    var listMonHoc = monHocServices.GetMonHocCuaKhoa(selectedKhoa.sMaKhoa);
                    FillMonHocCombobox(listMonHoc);
                    LoadData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Khoa selectedKhoa = cmbKhoa.SelectedItem as Khoa;
            if (selectedKhoa != null)
            {
                var listMonHoc = monHocServices.GetMonHocCuaKhoa(selectedKhoa.sMaKhoa);
                FillMonHocCombobox(listMonHoc);
                LoadData();
            }
        }

        private void cmbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
            if (txtFind.Text != "")
                txtFind_TextChanged(null, null);
            if (txtfindName.Text != "")
                txtfindName_TextChanged(null, null);
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text != "")
            {
                try
                {
                    if (cbfindAll.Checked == true)
                    {
                        List<CauHoi> listUser = cauHoiServices.GetFind(txtFind.Text);
                        BindGrid(listUser);
                    }
                    else
                    {
                        MonHoc selectedMonHoc = cmbMonHoc.SelectedItem as MonHoc;
                        if (selectedMonHoc != null)
                        {
                            List<CauHoi> list = cauHoiServices.GetAllInMonHocFind(selectedMonHoc.sMaMonHoc, txtFind.Text);
                            BindGrid(list);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                LoadData();
            }
        }

        private void txtfindName_TextChanged(object sender, EventArgs e)
        {
            if (txtfindName.Text != "")
            {
                try
                {
                    if (cbfindAll.Checked == true)
                    {
                        List<CauHoi> listUser = cauHoiServices.GetFindName(txtfindName.Text);
                        BindGrid(listUser);
                    }
                    else
                    {
                        MonHoc selectedMonHoc = cmbMonHoc.SelectedItem as MonHoc;
                        if (selectedMonHoc != null)
                        {
                            List<CauHoi> list = cauHoiServices.GetAllInMonHocFindName(selectedMonHoc.sMaMonHoc, txtfindName.Text);
                            BindGrid(list);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                LoadData();
            }
        }

        private void cbfindAll_CheckedChanged(object sender, EventArgs e)
        {
            if (txtFind.Text != "")
                txtFind_TextChanged(null, null);
            if (txtfindName.Text != "")
                txtfindName_TextChanged(null, null);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Form formToDisplay = new AdminAddCauHoi(monHocServices.GetMaMonHoc(cmbMonHoc.Text));
            this.Hide();
            formToDisplay.Closed += (s, args) =>
            {
                LoadData();
                this.Show();
            };
            formToDisplay.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                Form formToDisplay = new AdminSuaCauHoi(dgv.SelectedRows[0].Cells[0].Value.ToString());
                this.Hide();
                formToDisplay.Closed += (s, args) =>
                {
                    LoadData();
                    this.Show();
                };
                formToDisplay.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void excel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                openFileDialog.Title = "Chọn file Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string monHoc = monHocServices.GetMaMonHoc(cmbMonHoc.Text);
                    ImportExcel(filePath,monHoc);
                }
            }
        }

        private void ImportExcel(string filePath, string maMonHoc)
        {
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1); 
                    int rowCount = worksheet.RowsUsed().Count(); 

                    CauHoiServices cauHoiService = new CauHoiServices();

                    for (int row = 2; row <= rowCount; row++) 
                    {
                  
                        string noiDung = worksheet.Cell(row, 1).GetValue<string>(); 
                        string dapAnA = worksheet.Cell(row, 2).GetValue<string>();
                        string dapAnB = worksheet.Cell(row, 3).GetValue<string>();
                        string dapAnC = worksheet.Cell(row, 4).GetValue<string>();
                        string dapAnD = worksheet.Cell(row, 5).GetValue<string>();
                        string dapAnDung = worksheet.Cell(row, 6).GetValue<string>();

                        if (string.IsNullOrWhiteSpace(noiDung) || string.IsNullOrWhiteSpace(dapAnDung))
                            continue; 

                        int dapAnDungInt = ConvertDapAnToInt(dapAnDung);

                   
                        CauHoi cauHoi = new CauHoi
                        {
                            sMaMonHoc = maMonHoc,
                            sNoiDungCauHoi = noiDung,
                            sCauTraLoi1 = dapAnA,
                            sCauTraLoi2 = dapAnB,
                            sCauTraLoi3 = dapAnC,
                            sCauTraLoi4 = dapAnD,
                            iDapAn = dapAnDungInt
                        };

    
                        bool isSuccess = cauHoiService.AddCauHoi(cauHoi);
                        if (!isSuccess)
                        {
                            MessageBox.Show($"Lỗi khi thêm câu hỏi dòng {row}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    MessageBox.Show("Nhập dữ liệu từ Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nhập dữ liệu: " + ex.Message + "\n" + ex.StackTrace, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ConvertDapAnToInt(string dapAn)
        {
            switch (dapAn.Trim().ToUpper())
            {
                case "A": return 1;
                case "B": return 2;
                case "C": return 3;
                case "D": return 4;
                default: return 0; 
            }
        }
    }
}
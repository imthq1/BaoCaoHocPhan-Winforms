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
                            List<CauHoi> list = cauHoiServices.GetAllInMonHocFind(selectedMonHoc.sMaMonHoc,txtFind.Text);
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
                            List<CauHoi> list = cauHoiServices.GetAllInMonHocFindName(selectedMonHoc.sMaMonHoc,txtfindName.Text);
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
                txtFind_TextChanged(null,null);
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
    }
}

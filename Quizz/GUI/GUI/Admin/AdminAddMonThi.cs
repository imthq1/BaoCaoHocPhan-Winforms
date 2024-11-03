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
    public partial class AdminAddMonThi : Form
    {
        private readonly MonHocServices monHocServices=new MonHocServices();
        public AdminAddMonThi()
        {
            InitializeComponent();
        }

        private void AdminAddMonThi_Load(object sender, EventArgs e)
        {
            Misc.setGridViewStyle(dgv_ds);
            dgv_ds.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            LoadData();
        }
        private void BindGrid(List<MonHoc> list)
        {
            dgv_ds.Rows.Clear();
            foreach (var item in list)
            {
                int index = dgv_ds.Rows.Add();
                dgv_ds.Rows[index].Cells[0].Value = item.sMaMonHoc;
                dgv_ds.Rows[index].Cells[1].Value = item.sTenMonHoc;
                dgv_ds.Rows[index].Cells[2].Value = item.Khoa.sTenKhoa;
                dgv_ds.Rows[index].Cells[3].Value = item.tGianThi;
            }
        }

        private void LoadData(string filter = null)
        {
            try
            {
                List<MonHoc> list;
                if (string.IsNullOrEmpty(filter))
                    list = monHocServices.GetAll();
                else
                    list = monHocServices.GetFiltered(filter);

                BindGrid(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void AdminAddMonThi_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<MonHoc> list = monHocServices.GetFind(txtFind.Text);
                BindGrid(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtfindName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<MonHoc> list = monHocServices.GetFindByMonHocName(txtfindName.Text);
                BindGrid(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgv_ds.SelectedRows.Count > 0)
            {
                string maMH = dgv_ds.SelectedRows[0].Cells[0].Value.ToString();
                var confirmResult = MessageBox.Show("Bạn có chắc muốn xóa môn thi này?",
                                                     "Xác nhận xóa",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    bool result = monHocServices.DeleteMonHoc(maMH);

                    if (result)
                    {
                        MessageBox.Show("Xoá môn thi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xoá môn thi thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một môn thi để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Form formToDisplay = new AdminTaoMonThi();
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
            if (dgv_ds.SelectedRows.Count > 0)
            {
                string maMH = dgv_ds.SelectedRows[0].Cells[0].Value.ToString();
                Form formToDisplay = new AdminSuaMonThi(maMH);
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
                MessageBox.Show("Vui lòng chọn một môn học để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
                  this.Close();
        }
    }
}

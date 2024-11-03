using BUS;
using DAL;
using GUI.GUI.Admin;
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
using System.Xml.Linq;

namespace GUI
{
    public partial class AdminQuanLy : Form
    {
        private readonly KhoaServices khoaServices = new KhoaServices();
        private readonly TaiKhoanServices taiKhoanServices = new TaiKhoanServices();
        public AdminQuanLy()
        {
            InitializeComponent();
        }

        private void AdminQuanLy_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        private void BindGrid(List<TaiKhoan> list)
        {
            dgv_ds.Rows.Clear();
            foreach (var item in list)
            {
                int index = dgv_ds.Rows.Add();
                dgv_ds.Rows[index].Cells[0].Value = item.sMssv;
                dgv_ds.Rows[index].Cells[1].Value = item.sHoTen;
                dgv_ds.Rows[index].Cells[2].Value = item.Khoa;
                if (item.Khoa != null)
                    dgv_ds.Rows[index].Cells[2].Value = item.Khoa.sTenKhoa;
                dgv_ds.Rows[index].Cells[3].Value = item.dNgaySinh;
                dgv_ds.Rows[index].Cells[4].Value = item.sGioiTinh;


            }
        }
        void LoadData()
        {
            try
            {
                List<TaiKhoan> listUser = taiKhoanServices.GetAllNoAdmin();
                BindGrid(listUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AdminQuanLy_Load(object sender, EventArgs e)
        {
            Misc.setGridViewStyle(dgv_ds);
            dgv_ds.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_ds.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Form formToDisplay = new AdminAdd();
            this.Hide();
            formToDisplay.Closed += (s, args) =>
            {
                LoadData();
                this.Show();
            };
            formToDisplay.Show();
        }

        private void dgv_ds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dgv_ds.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dgv_ds.Rows[e.RowIndex];

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgv_ds.SelectedRows.Count > 0)
            {
                string studentID = dgv_ds.SelectedRows[0].Cells[0].Value.ToString();
                var confirmResult = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?",
                                                     "Xác nhận xóa",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    bool result = taiKhoanServices.DeleteTaiKhoan(studentID);

                    if (result)
                    {
                        MessageBox.Show("Xoá tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xoá tài khoản thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgv_ds.SelectedRows.Count > 0)
            {
                string studentID = dgv_ds.SelectedRows[0].Cells[0].Value.ToString();
                Form formToDisplay = new AdminSua(studentID);
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Form formToDisplay = new AdminTaoCauHoi();
            this.Hide();
            formToDisplay.Closed += (s, args) =>
            {
                LoadData();
                this.Show();
            };
            formToDisplay.Show();
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<TaiKhoan> listUser = taiKhoanServices.GetFind(txtFind.Text);
                BindGrid(listUser);
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
                List<TaiKhoan> listUser = taiKhoanServices.GetFindName(txtfindName.Text);
                BindGrid(listUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDiem_Click(object sender, EventArgs e)
        {
            Form formToDisplay = new AdminDiemSV();
            this.Hide();
            formToDisplay.Closed += (s, args) =>
            {
                LoadData();
                this.Show();
            };
            formToDisplay.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formToDisplay = new AdminAddMonThi();
            this.Hide();
            formToDisplay.Closed += (s, args) =>
            {
                LoadData();
                this.Show();
            };
            formToDisplay.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Bạn có chắc muốn đăng xuất",
                                                     "Xác nhận! ",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }    
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                                                     "Xác nhận! ",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}

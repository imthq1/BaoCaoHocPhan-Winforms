using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI.GUI.Admin
{
    public partial class AdminDiemSV : Form
    {
        private readonly DiemService diemService = new DiemService();
        
        public AdminDiemSV()
        {
            InitializeComponent();
            cbxBoloc.SelectedIndexChanged += cbxBoloc_SelectedIndexChanged;
        }

        private void BindGrid(List<Diem> list)
        {
            dgv_ds.Rows.Clear();
            foreach (var item in list)
            {
                int index = dgv_ds.Rows.Add();
                dgv_ds.Rows[index].Cells[0].Value = item.sMssv;
                dgv_ds.Rows[index].Cells[1].Value = item.TaiKhoan.sHoTen;  
                dgv_ds.Rows[index].Cells[2].Value = item.MonHoc.sTenMonHoc;  
                dgv_ds.Rows[index].Cells[3].Value = item.dDiem;
            }
        }

        private void LoadData(string filter = null)
        {
            try
            {
                List<Diem> listDiem;
                if (string.IsNullOrEmpty(filter))
                    listDiem = diemService.getAll();
                else
                    listDiem = diemService.GetFiltered(filter);

                BindGrid(listDiem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxBoloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = cbxBoloc.SelectedItem?.ToString();
            LoadData(selectedFilter);
        }


        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<Diem> listUser = diemService.GetFind(txtFind.Text);
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
                List<Diem> listUser = diemService.GetFindByMonHocName(txtfindName.Text);
                BindGrid(listUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdminDiemSV_Load(object sender, EventArgs e)
        {
            Misc.setGridViewStyle(dgv_ds);
            dgv_ds.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cbxBoloc.Items.Clear();
            cbxBoloc.Items.Add("Tăng");
            cbxBoloc.Items.Add("Giảm");
            cbxBoloc.Items.Add("Đạt");
            cbxBoloc.Items.Add("Rớt");
            LoadData();
        }

        private void AdminDiemSV_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void cbxBoloc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadData();
            if (txtFind.Text != "")
                txtFind_TextChanged(null, null);
            if (txtfindName.Text != "")
                txtfindName_TextChanged(null, null);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

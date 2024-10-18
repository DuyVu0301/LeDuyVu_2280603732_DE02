using BUS;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class Form1 : Form
    {
        private readonly SanPhamService spService = new SanPhamService();
        private readonly LoaiSPService loaiService = new LoaiSPService();
        public Form1()
        {
            InitializeComponent();
        }
        private void FillLoaiCombobox(List<LoaiSP> listLoaiSP)
        {
            this.cbxLoai.DataSource = listLoaiSP;
            this.cbxLoai.DisplayMember = "TenLoai";
            this.cbxLoai.ValueMember = "MaLoai";
        }
        private void BindGrid(List<Sanpham> listSP)
        {
            dgvSP.Rows.Clear();
            foreach (var item in listSP)
            {
                int index = dgvSP.Rows.Add();
                dgvSP.Rows[index].Cells[0].Value = item.MaSP;
                dgvSP.Rows[index].Cells[1].Value = item.TenSP;
                dgvSP.Rows[index].Cells[2].Value = item.Ngaynhap;
                dgvSP.Rows[index].Cells[3].Value = item.LoaiSP.TenLoai;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var loaiSPs = loaiService.GetAll();
                var listSanPhams = spService.GetAll();
                FillLoaiCombobox(loaiSPs);
                BindGrid(listSanPhams);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvSP.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    txtMaSP.Text = dgvSP.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtTenSP.Text = dgvSP.Rows[e.RowIndex].Cells[1].Value.ToString();
                    dateNgayNhap.Text = dgvSP.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cbxLoai.Text = dgvSP.Rows[e.RowIndex].Cells[3].Value.ToString();

                }

            }
        }
        private void SaveStudent()
        {
            try
            {
                // Tạo đối tượng SinhVien từ dữ liệu người dùng nhập
                Sanpham sanpham = new Sanpham()
                {
                    MaSP = txtMaSP.Text,
                    TenSP = txtTenSP.Text,
                    Ngaynhap = dateNgayNhap.Value,
                    MaLoai = (string)cbxLoai.SelectedValue,

                };

                // Gọi phương thức InsertUpdate để thêm hoặc cập nhật sinh viên
                SanPhamService.InsertUpdate();

                // Thông báo thành công
                MessageBox.Show("Lưu thành công!");

                // Làm mới danh sách sinh viên sau khi thêm mới
                RefreshSanPhamList();
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
            }
        }
        private void RefreshSanPhamList()
        {
            try
            {
                // Lấy danh sách sinh viên từ database
                var listSanPhams = spService.GetAll();

                // Gọi hàm BindGrid để cập nhật DataGridView
                BindGrid(listSanPhams);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi tải danh sách sinh viên: {ex.Message}");
            }
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            SaveStudent();
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
          
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
           
        }


      
    }
}

using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class SanPhamService
    {
        public List<Sanpham> GetAll()
        {
            Model1 Model1 = new Model1();
            return Model1.Sanphams.ToList();
        }
        public void InsertUpdate(Sanpham s)
        {
            Model1 context = new Model1();

            // Kiểm tra xem sinh viên đã tồn tại hay chưa
            var existingStudent = context.Sanphams.FirstOrDefault(x => x.MaSP == s.MaSP);

            if (existingStudent != null)
            {
                // Cập nhật sinh viên đã tồn tại
                context.Entry(existingStudent).CurrentValues.SetValues(s);
            }
            else
            {
                // Thêm sinh viên mới
                context.Sanphams.Add(s);
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            context.SaveChanges();
        }
        public void Delete(string MaSP)
        {
            Model1 context = new Model1();

            // Tìm sinh viên trong cơ sở dữ liệu dựa vào MSSV
            var student = context.Sanphams.FirstOrDefault(x => x.MaSP == MaSP);

            if (student != null)
            {
                // Nếu sinh viên tồn tại, tiến hành xóa
                context.Sanphams.Remove(student);
                context.SaveChanges();
            }
            else
            {
                // Nếu sinh viên không tồn tại, có thể báo lỗi hoặc thông báo không tìm thấy
                throw new Exception("Sinh viên không tồn tại.");
            }
        }

        public static void InsertUpdate()
        {
            throw new NotImplementedException();
        }
    }

}

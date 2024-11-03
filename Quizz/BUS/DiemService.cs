using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;

namespace BUS
{
    public class DiemService
    {
        public bool AddDiem(string ten, string maMonHoc, double diem)
        {
            using (var db = new QuizzDB()) 
            {
                var user = db.TaiKhoans.FirstOrDefault(t => t.sHoTen == ten);
                var monHoc = db.MonHocs.FirstOrDefault(m => m.sMaMonHoc == maMonHoc);

                if (user == null || monHoc == null)
                {
                    Console.WriteLine("Không tìm thấy tài khoản hoặc môn học.");
                    return false;
                }

                var DiemHS = new Diem
                {
                    sMssv = user.sMssv,
                    sMaMonHoc = monHoc.sMaMonHoc,
                    dDiem = (decimal)diem
                };

                try
                {
                    db.Diems.Add(DiemHS);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public List<Diem> GetFind(string find)
        {
            QuizzDB context = new QuizzDB();
            return context.Diems
                          .Where(p=>p.sMssv.StartsWith(find))
                          .ToList();
        }
        public List<Diem> GetFindByMonHocName(string find)
        {
            QuizzDB context = new QuizzDB();
            {
                return context.Diems
                              .Include(d => d.MonHoc)
                              .Where(d => d.MonHoc.sTenMonHoc.Contains(find))
                              .ToList(); 
            }
        }

        public List<Diem> GetFiltered(string filter)
        {
            using (var context = new QuizzDB())
            {
                var query = context.Diems
                    .Include(d => d.MonHoc)
                    .Include(d => d.TaiKhoan)
                    .AsQueryable();

                switch (filter)
                {
                    case "Tăng":
                        return query.OrderBy(d => d.dDiem).ToList();
                    case "Giảm":
                        return query.OrderByDescending(d => d.dDiem).ToList();
                    case "Đạt":
                        return query.Where(d => d.dDiem >= 4).ToList(); 
                    case "Rớt":
                        return query.Where(d => d.dDiem < 4).ToList();
                    default:
                        return query.ToList();
                }
            }
        }
        public List<Diem> getAll()
        {
            using (var db = new QuizzDB())
            {
                return db.Diems
                         .Include(d => d.TaiKhoan)  
                         .Include(d => d.MonHoc)
                         .ToList();
            }
        }
    }
}

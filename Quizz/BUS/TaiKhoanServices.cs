using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BCrypt.Net;

namespace BUS
{
    
    public class TaiKhoanServices
    {
        public TaiKhoan Login(string username, string password)
        {
            using (QuizzDB dB = new QuizzDB())
            {
                var user = dB.TaiKhoans.FirstOrDefault(u => u.sMssv == username);
                if (user != null)
                {
                    bool isPasswordHashed = user.sMatKhau.StartsWith("$2a$") || user.sMatKhau.StartsWith("$2b$") || user.sMatKhau.StartsWith("$2y$");

                    if (isPasswordHashed && BCrypt.Net.BCrypt.Verify(password, user.sMatKhau))
                    {
                        return user;
                    }
                    else if (!isPasswordHashed && user.sMatKhau == password)
                    {
                        user.sMatKhau = BCrypt.Net.BCrypt.HashPassword(password);
                        dB.SaveChanges();
                        return user;
                    }
                }
                return null;
            }
        }
        public int? GetMaQuyen(string maTaiKhoan)
        {
            using (QuizzDB dB = new QuizzDB())
            {
                var user = dB.TaiKhoans.FirstOrDefault(u => u.sMssv == maTaiKhoan);
                return user.iMaQuyen;
            }
        }
        public TaiKhoan GetUser(string maTaiKhoan)
        {
            using (QuizzDB dB = new QuizzDB())
            {
                var user = dB.TaiKhoans.FirstOrDefault(u => u.sMssv == maTaiKhoan);
                return user;
            }
        }
        public int? GetMaQuyen(TaiKhoan user)
        {
            return user.iMaQuyen;
        }
        public string GetTenQuyen(TaiKhoan user)
        {
            using (var context = new QuizzDB())
            {
                var nguoidung = context.TaiKhoans
                                 .FirstOrDefault(u => u.sMssv == user.sMssv);

                if (nguoidung != null)
                {
                    return nguoidung.Quyen.sTenQuyen;
                }
                return null;
            }
        }
        public List<TaiKhoan> GetAll()
        {
            QuizzDB context = new QuizzDB();
            return context.TaiKhoans.ToList();
        }
        public List<TaiKhoan> GetAllNoAdmin()
        {
            QuizzDB context = new QuizzDB();
            return context.TaiKhoans.Where(p => p.Quyen.sTenQuyen != "Admin" ).ToList();
        }
        public List<TaiKhoan> GetFind(string find)
        {
            QuizzDB context = new QuizzDB();
            return context.TaiKhoans
                          .Where(p => p.Quyen.sTenQuyen != "Admin" && p.sMssv.StartsWith(find))
                          .ToList();
        }
        public List<TaiKhoan> GetFindName(string find)
        {
            QuizzDB context = new QuizzDB();
            return context.TaiKhoans
                          .Where(p => p.Quyen.sTenQuyen != "Admin" && p.sHoTen.Contains(find))
                          .ToList();
        }

        public bool AddTaiKhoan(TaiKhoan taiKhoan)
        {
            try
            {
                QuizzDB context = new QuizzDB();
                context.TaiKhoans.Add(taiKhoan);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool AddTaiKhoan(string mssv, string pass, string hoten, string tenKhoa, DateTime time, string gioitinh, int quyen, byte[] avatar)
        {
            using (var db = new QuizzDB())
            {
                if (db.TaiKhoans.Any(t => t.sMssv == mssv))
                {
                    return false;
                }

                var khoa = db.Khoas.FirstOrDefault(k => k.sTenKhoa == tenKhoa);
                if (khoa != null)
                {
                    var taiKhoanMoi = new TaiKhoan
                    {
                        sMssv = mssv,
                        sMatKhau = pass,
                        sHoTen = hoten,
                        sMaKhoa = khoa.sMaKhoa,
                        dNgaySinh = time,
                        sGioiTinh = gioitinh,
                        iMaQuyen = quyen,
                        AvatarID = avatar
                    };

                    try
                    {
                        db.TaiKhoans.Add(taiKhoanMoi);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        public bool DeleteTaiKhoan(string mssv)
        {
            using (var db = new QuizzDB())
            {
                var diemList = db.Diems.Where(d => d.sMssv == mssv).ToList();
                if (diemList.Any())
                {
                    db.Diems.RemoveRange(diemList);
                    db.SaveChanges();
                }
                var student = db.TaiKhoans.FirstOrDefault(s => s.sMssv == mssv);
                if (student != null)
                {
                    db.TaiKhoans.Remove(student);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public bool SuaTaiKhoan(string mssv,TaiKhoan newTaiKhoan)
        {
            using (var db = new QuizzDB())
            {
                var student = db.TaiKhoans.FirstOrDefault(s => s.sMssv == mssv);
                if (student != null)
                {
                    var khoa = db.Khoas.FirstOrDefault(k => k.sMaKhoa == newTaiKhoan.sMaKhoa);
                    if (khoa != null)
                    {
                        student.sMatKhau = newTaiKhoan.sMatKhau;
                        student.sHoTen = newTaiKhoan.sHoTen;
                        student.sMaKhoa = newTaiKhoan.sMaKhoa;
                        student.dNgaySinh = newTaiKhoan.dNgaySinh;
                        student.sGioiTinh = newTaiKhoan.sGioiTinh;
                        student.iMaQuyen = newTaiKhoan.iMaQuyen;
                        student.AvatarID = newTaiKhoan.AvatarID;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

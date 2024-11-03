using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace BUS
{
    public class MonHocServices
    {

        public bool TaoMonHoc(string ma,string ten,string khoa,TimeSpan tg)
        {
            using (var db = new QuizzDB())
            {
                if (db.MonHocs.Any(t => t.sMaMonHoc == ma))
                {
                    return false;
                }

                var kh = db.Khoas.FirstOrDefault(k => k.sTenKhoa == khoa);
                if (khoa != null)
                {
                    var monHocMoi = new MonHoc
                    {
                        sMaMonHoc = ma,
                        sTenMonHoc = ten,
                        sMaKhoa = kh.sMaKhoa,
                        tGianThi=tg
                    };

                    try
                    {
                        db.MonHocs.Add(monHocMoi);
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
        public bool AdminSuaMonHoc(string ma,MonHoc newMonHoc)
        {
            QuizzDB db = new QuizzDB();
            var MonHocCu=db.MonHocs.FirstOrDefault(m=>m.sMaMonHoc==ma);
            if(MonHocCu==null)
            {
                return false ;
            }
            MonHocCu.sMaMonHoc = newMonHoc.sMaMonHoc;
            MonHocCu.sTenMonHoc = newMonHoc.sTenMonHoc;
            MonHocCu.tGianThi = newMonHoc.tGianThi;
            MonHocCu.sMaKhoa = newMonHoc.sMaKhoa;
            db.SaveChanges();
            return true;
            
        }


        public List<MonHoc> GetAll()
        {
            QuizzDB context = new QuizzDB();
            return context.MonHocs.ToList();
        }
        public List<MonHoc> GetMonHocCuaKhoa(string makhoa)
        {
            QuizzDB context = new QuizzDB();
            return context.MonHocs.Where(m => m.sMaKhoa == makhoa).ToList();
        }
        public string GetMaMonHoc(string tenmonhoc)
        {
            QuizzDB context = new QuizzDB();
            var monhoc = context.MonHocs.FirstOrDefault(m => m.sTenMonHoc == tenmonhoc);
            return monhoc.sMaMonHoc;
        }
        public string GetTenMonHoc(string mamonhoc)
        {
            QuizzDB context = new QuizzDB();
            var monhoc = context.MonHocs.FirstOrDefault(m => m.sMaMonHoc == mamonhoc);
            return monhoc.sTenMonHoc;
        }
        public string GetTenMonHocCuaKhoa(string makhoa)
        {
            QuizzDB context = new QuizzDB();
            var monhoc = context.MonHocs.FirstOrDefault(m => m.sMaKhoa == makhoa);
            return monhoc.sTenMonHoc;
        }
        public MonHoc getMonHocById(string id)
        {
            QuizzDB context= new QuizzDB();
            return context.MonHocs.FirstOrDefault(m=>m.sMaMonHoc==id);
        }
        public List<MonHoc> GetFiltered(string filter)
        {
            QuizzDB context = new QuizzDB();
            return context.MonHocs.ToList();
        }
        public List<MonHoc> GetFind(string find)
        {
            QuizzDB context = new QuizzDB();
            return context.MonHocs
                          .Where(p => p.sMaMonHoc.StartsWith(find))
                          .ToList();
        }
        public List<MonHoc> GetFindByMonHocName(string find)
        {
            QuizzDB context = new QuizzDB();
            {
                return context.MonHocs
                              .Where(d => d.sTenMonHoc.Contains(find))
                              .ToList();
            }
        }
        public bool DeleteMonHoc(string maMh)
        {
            using (var db = new QuizzDB())
            {
                var listMonThi = db.CauHois.Where(s => s.sMaMonHoc == maMh).ToList();
                if(listMonThi.Any())
                {
                    db.CauHois.RemoveRange(listMonThi);
                    db.SaveChanges();
                }
                var monthi = db.MonHocs.FirstOrDefault(m => m.sMaMonHoc == maMh);
                if (monthi != null)
                {
                    db.MonHocs.Remove(monthi);
                    db.SaveChanges();
                    return true;
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
    }
}

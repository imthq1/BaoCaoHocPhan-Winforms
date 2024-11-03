using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class CauHoiServices
    {
        public List<CauHoi> GetAll()
        {
            QuizzDB context = new QuizzDB();
            return context.CauHois.ToList();
        }
        public List<CauHoi> GetAllInMonHoc(string maMonHoc)
        {
            QuizzDB context = new QuizzDB();
            return context.CauHois.Where(c => c.sMaMonHoc == maMonHoc).ToList();
        }
        public List<CauHoi> GetAllInMonHocFind(string maMonHoc, string find)
        {
            QuizzDB context = new QuizzDB();
            return context.CauHois.Where(c => c.sMaMonHoc == maMonHoc && c.iMaCauHoi.ToString() == find).ToList();
        }
        public List<CauHoi> GetAllInMonHocFindName(string maMonHoc, string find)
        {
            QuizzDB context = new QuizzDB();
            return context.CauHois.Where(c => c.sMaMonHoc == maMonHoc && c.sNoiDungCauHoi.Contains(find)).ToList();
        }
        public List<CauHoi> GetFind(string find)
        {
            QuizzDB context = new QuizzDB();
            return context.CauHois
                          .Where(c => c.iMaCauHoi.ToString() == find)
                          .ToList();
        }
        public List<CauHoi> GetFindName(string find)
        {
            QuizzDB context = new QuizzDB();
            return context.CauHois
                          .Where(c => c.sNoiDungCauHoi.Contains(find))
                          .ToList();
        }
        public bool AddCauHoi(CauHoi cauHoi)
        {
            try
            {
                QuizzDB context = new QuizzDB();
                context.CauHois.Add(cauHoi);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool AdminAddCauHoi(CauHoi CauHoi)
        {
            try
            {
                QuizzDB context = new QuizzDB();
                context.CauHois.Add(CauHoi);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public CauHoi GetCauHoi(string maCauHoi)
        {
            using (QuizzDB dB = new QuizzDB())
            {
                var cauHoi = dB.CauHois.FirstOrDefault(u => u.iMaCauHoi.ToString() == maCauHoi);
                return cauHoi;
            }
        }
        public bool AdminSuaCauHoi(string id, CauHoi newCauHoi)
        {
            using (var db = new QuizzDB())
            {
                var cauHoi = db.CauHois.FirstOrDefault(s => s.iMaCauHoi.ToString() == id);
                if (cauHoi != null)
                {
                    cauHoi.sNoiDungCauHoi = newCauHoi.sNoiDungCauHoi;
                    cauHoi.sCauTraLoi1 = newCauHoi.sCauTraLoi1;
                    cauHoi.sCauTraLoi2 = newCauHoi.sCauTraLoi2;
                    cauHoi.sCauTraLoi3 = newCauHoi.sCauTraLoi3;
                    cauHoi.sCauTraLoi4 = newCauHoi.sCauTraLoi4;
                    cauHoi.iDapAn = newCauHoi.iDapAn;
                    db.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
    }
}

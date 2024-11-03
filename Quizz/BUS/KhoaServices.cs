using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class KhoaServices
    {
        public List<Khoa> GetAll()
        {
            
            QuizzDB context = new QuizzDB();
            return context.Khoas.ToList();
        }
        public string MatoName(string sMaKhoa)
        {
            var db = new QuizzDB();
            var khoa = db.Khoas.FirstOrDefault(k => k.sMaKhoa == sMaKhoa);
            return khoa.sTenKhoa;
        }
        public string NameToMa(string sName)
        {
            var db = new QuizzDB();
            var khoa = db.Khoas.FirstOrDefault(k => k.sTenKhoa == sName);
            return khoa.sMaKhoa;
        }
    }
}

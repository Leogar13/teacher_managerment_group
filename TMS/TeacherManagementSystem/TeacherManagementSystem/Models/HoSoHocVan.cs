using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class HoSoHocVan
    {
        public int MaHocVan { get; set; }
        public string TenHocVi { get; set; }
        public string TenTrinhDo { get; set; }
        public string NuocDaoTao { get; set; }
        public string HeDaoTao { get; set; }
        public string NoiDaoTao { get; set; }
        public int NamCap { get; set; }
        public DateTime ThoiDiemNhan { get; set; }
        public DateTime ThoiDiemKetThuc { get; set; }
    }
}
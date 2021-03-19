using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class HocHam
    {
        public int MaHS_HocHam { get; set; }
        public int MaHocHam { get; set; }
        public string TenHocHam { get; set; }
        public int MaDMDT { get; set; }
        public int MaDMNC { get; set; }
        public DateTime ThoiDiemNhan { get; set; }
        public DateTime ThoiDiemKetThuc { get; set; }

    }
}
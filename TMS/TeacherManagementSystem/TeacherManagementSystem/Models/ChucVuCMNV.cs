using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class ChucVuCMNV
    {
        public int MaHS_CVCMNV { get; set; }
        public int MaCVCMNV { get; set; }
        public int MaDMDT { get; set; }
        public int MaDMNC { get; set; }
        public string TenCVCMNV { get; set; }
        public DateTime ThoiDiemNhan { get; set; }
        public DateTime ThoiDiemKetThuc { get; set; }

    }
}
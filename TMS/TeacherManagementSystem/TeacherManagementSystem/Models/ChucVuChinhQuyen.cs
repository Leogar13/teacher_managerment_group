using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class ChucVuChinhQuyen
    {
        public int MaHS_CVCQ { get; set; }
        public int MaCVCQ { get; set; }
        public string TenCVCQ { get; set; }
        public int MaMienGiam { get; set; }
        public DateTime ThoiDiemNhan { get; set; }
        public DateTime ThoiDiemKetThuc { get; set; }
    }
}
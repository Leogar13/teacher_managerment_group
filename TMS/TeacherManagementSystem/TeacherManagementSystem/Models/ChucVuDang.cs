using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class ChucVuDang
    {
        public int MaHS_CVD { get; set; }
        public int MaCVD { get; set; }
        public string TenCVD { get; set; }
        public int MaMienGiam { get; set; }
        public DateTime ThoiDiemNhan { get; set; }
        public DateTime ThoiDiemKetThuc { get; set; }
    }
}
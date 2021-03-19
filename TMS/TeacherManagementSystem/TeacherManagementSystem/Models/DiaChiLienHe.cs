using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class DiaChiLienHe
    {
        public int MaGV { get; set; }
        public int MaLienHe { get; set; }
        public DateTime ThoiGian { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public string ThanhPho { get; set; }
        public string DTDiDong { get; set; }
        public string DTNhaRieng { get; set; }
        public string Email { get; set; }
    }
}
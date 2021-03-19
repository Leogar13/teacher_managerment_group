using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class ThanNhan
    {
        public int MaThanNhan { get; set; }
        public string TenThanNhan { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string NgheNghiep { get; set; }
    }

}
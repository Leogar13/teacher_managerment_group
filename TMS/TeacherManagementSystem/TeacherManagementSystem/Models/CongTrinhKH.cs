using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class CongTrinhKH
    {
        public int id { get; set; }
        public string name { get; set; }
        public int MaloaiCT { get; set; }
        public int MaloaiNC { get; set; }
        public int MaDVT { get; set; }
        public int So { get; set; }
        public int Sotacgia { get; set; }
        public string nameloaiCT { get; set; }
        public string nameloaiNC { get; set; }
        public string namedonvitinh { get; set; }        
    }
}
using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class DanhSachCVCMNV
    {
        public List<ChucVuCMNV> list = new List<ChucVuCMNV>();
        public DanhSachCVCMNV()
        {
            if (list.Count > 0)
            {
                list.Clear();
            }
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "Select * from CV_ChuyenMonNghiepVu;";
                var cmd = new SqlCommand(q, c);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cvc = new ChucVuCMNV();
                        cvc.MaCVCMNV = reader.GetInt32(0);
                        cvc.MaDMDT = reader.GetInt32(1);
                        cvc.MaDMNC = reader.GetInt32(2);
                        cvc.TenCVCMNV = reader.GetString(3);
                        list.Add(cvc);
                    }
                }
                reader.Close();
            }

        }
    }
}
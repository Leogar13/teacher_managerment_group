using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class DanhSachCVD
    {
        public List<ChucVuDang> list = new List<ChucVuDang>();
        public DanhSachCVD()
        {
            if (list.Count > 0)
            {
                list.Clear();
            }
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "Select * from ChucVuDang;";
                var cmd = new SqlCommand(q, c);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cvd = new ChucVuDang();
                        cvd.MaCVD = reader.GetInt32(0);
                        cvd.MaMienGiam = reader.GetInt32(1);
                        cvd.TenCVD = reader.GetString(2);
                        list.Add(cvd);
                    }
                }
                reader.Close();
            }

        }
    }
}
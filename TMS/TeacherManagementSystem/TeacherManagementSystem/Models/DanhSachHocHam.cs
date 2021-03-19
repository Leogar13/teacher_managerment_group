using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class DanhSachHocHam
    {
        public List<HocHam> list = new List<HocHam>();
        public DanhSachHocHam()
        {
            if (list.Count > 0)
            {
                list.Clear();
            }
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "Select * from HocHam;";
                var cmd = new SqlCommand(q, c);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var h = new HocHam();
                        h.MaHocHam = reader.GetInt32(0);
                        h.MaDMDT = reader.GetInt32(1);
                        h.MaDMNC = reader.GetInt32(2);
                        h.TenHocHam = reader.GetString(3);
                        list.Add(h);
                    }
                }
                reader.Close();
            }

        }
    }
}
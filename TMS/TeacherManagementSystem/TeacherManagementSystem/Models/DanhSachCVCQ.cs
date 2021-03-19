using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class DanhSachCVCQ
    {
        public List<ChucVuChinhQuyen> list = new List<ChucVuChinhQuyen>();
        public DanhSachCVCQ()
        {
            if(list.Count > 0)
            {
                list.Clear();
            }
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using(var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "Select * from ChucVuChinhQuyen;";
                var cmd = new SqlCommand(q, c);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cvcq = new ChucVuChinhQuyen();
                        cvcq.MaCVCQ = reader.GetInt32(0);
                        cvcq.MaMienGiam = reader.GetInt32(1);
                        cvcq.TenCVCQ = reader.GetString(2);
                        list.Add(cvcq);
                    }
                }
                reader.Close();
            }

        }
    }
}
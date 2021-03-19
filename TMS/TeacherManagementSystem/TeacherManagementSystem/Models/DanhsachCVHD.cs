using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class DanhsachCVHD
    {
        public List<string> danhsachHD = new List<string>();
        public DanhsachCVHD()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (danhsachHD.Count > 0)
            {
                danhsachHD.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var CVHDQuery = "select MaCVHDan, TenCongViec from HuongDan order by MaCVHDan desc";
                SqlCommand sqlCommand = new SqlCommand(CVHDQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        danhsachHD.Add(reader.GetInt32(0) + "_" + reader.GetString(1));
                    }
                }
                reader.Close();

            }
        }
    }
}
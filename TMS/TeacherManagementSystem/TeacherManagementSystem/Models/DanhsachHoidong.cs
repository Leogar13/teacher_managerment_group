using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class DanhsachHoidong
    {
        public List<string> danhsachHDong = new List<string>();
        public DanhsachHoidong()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (danhsachHDong.Count > 0)
            {
                danhsachHDong.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var CVKTQuery = "select MaCVHD, TenCongViec from HoiDong order by MaCVHD desc";
                SqlCommand sqlCommand = new SqlCommand(CVKTQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        danhsachHDong.Add(reader.GetInt32(0) + "_" + reader.GetString(1));
                    }
                }
                reader.Close();

            }
        }

    }
}
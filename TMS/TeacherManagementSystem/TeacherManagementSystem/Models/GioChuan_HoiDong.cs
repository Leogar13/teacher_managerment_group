using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TeacherManagementSystem.Models
{
    public class GioChuan_HoiDong
    {
        public List<string> giochuan_HoiDong = new List<string>();
        public GioChuan_HoiDong()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (giochuan_HoiDong.Count > 0)
            {
                giochuan_HoiDong.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var TenGCHDQuery = "select TenGioChuan from GioChuan_HoiDong";
                SqlCommand sqlCommand = new SqlCommand(TenGCHDQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        giochuan_HoiDong.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
        }
    }
}
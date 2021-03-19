using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TeacherManagementSystem.Models
{
    public class GioChuan_HuongDan
    {
        public List<string> giochuan_HD = new List<string>();
        public GioChuan_HuongDan()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (giochuan_HD.Count > 0)
            {
                giochuan_HD.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var TenGCHDQuery = "select TenGioChuan from GioChuan_HuongDan";
                SqlCommand sqlCommand = new SqlCommand(TenGCHDQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        giochuan_HD.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
        }

    }
}
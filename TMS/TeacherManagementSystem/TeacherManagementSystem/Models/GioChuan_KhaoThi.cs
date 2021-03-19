using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TeacherManagementSystem.Models
{
    public class GioChuan_KhaoThi
    {
        public List<string> giochuan_KhaoThi = new List<string>();
        public GioChuan_KhaoThi()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (giochuan_KhaoThi.Count > 0)
            {
                giochuan_KhaoThi.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var TenGCKTQuery = "select TenGioChuan from GioChuan_KhaoThi";
                SqlCommand sqlCommand = new SqlCommand(TenGCKTQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        giochuan_KhaoThi.Add(reader.GetString(0));
                    }
                }
                reader.Close();

            }
        }
    }
}
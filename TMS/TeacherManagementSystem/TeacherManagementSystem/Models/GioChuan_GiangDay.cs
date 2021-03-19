using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TeacherManagementSystem.Models
{
    public class GioChuan_GiangDay
    {
        public List<string> giochuan_GD = new List<string>();
        public GioChuan_GiangDay()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if(giochuan_GD.Count > 0)
            {
                giochuan_GD.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var TenGCGDQuery = "select TenGioChuan from GioChuan_GiangDay";
                SqlCommand sqlCommand = new SqlCommand(TenGCGDQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        giochuan_GD.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
        } 

    }
}
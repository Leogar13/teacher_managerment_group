using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TeacherManagementSystem.Models
{
    public class HocVi
    {
        public List<string> hocVi = new List<string>();
        public HocVi()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using(var c = new SqlConnection(cn))
            {
                if(hocVi.Count > 0)
                {
                    hocVi.Clear();
                }
                c.Open();
                var q = "select * from HocVi";
                var cmd = new SqlCommand(q, c);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        hocVi.Add(reader.GetString(1));
                    }
                }
            }


        }


    }
}
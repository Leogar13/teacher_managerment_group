using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TeacherManagementSystem.Models
{
    public class HocPhan
    {
        public List<string> hocPhan = new List<string>();
        public HocPhan()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (hocPhan.Count > 0)
            {
                hocPhan.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var TenHocPhanQuery = "select TenHocPhan from HocPhan";
                SqlCommand sqlCommand = new SqlCommand(TenHocPhanQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        hocPhan.Add(reader.GetString(0));
                    }
                }
                reader.Close();

            }
        }
    }
}
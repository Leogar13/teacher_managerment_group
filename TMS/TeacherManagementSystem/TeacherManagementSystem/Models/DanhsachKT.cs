using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class DanhsachKT
    {
        public List<string> danhsachKT = new List<string>();
        public DanhsachKT()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (danhsachKT.Count > 0)
            {
                danhsachKT.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var CVKTQuery = "select MaCVKT, TenCongViec from KhaoThi order by MaCVKT desc";
                SqlCommand sqlCommand = new SqlCommand(CVKTQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        danhsachKT.Add(reader.GetInt32(0) + "_" + reader.GetString(1));
                    }
                }
                reader.Close();

            }
        }
    }
}
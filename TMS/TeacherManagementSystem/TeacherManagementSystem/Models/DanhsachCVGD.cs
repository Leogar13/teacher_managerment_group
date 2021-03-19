using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class DanhsachCVGD
    {
        public List<string> danhsachGD = new List<string>();
        public DanhsachCVGD()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (danhsachGD.Count > 0)
            {
                danhsachGD.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var CVGDQuery = "select MaCVGD, TenCongViec from GiangDay order by MaCVGD desc";
                SqlCommand sqlCommand = new SqlCommand(CVGDQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        danhsachGD.Add(reader.GetInt32(0) + "_" + reader.GetString(1));
                    }
                }
                reader.Close();

            }
        }
    }
}
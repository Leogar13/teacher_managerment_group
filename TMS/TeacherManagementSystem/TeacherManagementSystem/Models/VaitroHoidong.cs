using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TeacherManagementSystem.Models
{
    public class VaitroHoidong
    {
        public List<string> vaitroHoidong = new List<string>();
        public VaitroHoidong()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (vaitroHoidong.Count > 0)
            {
                vaitroHoidong.Clear();
            }

            using (var con = new SqlConnection(cn))
            {
                con.Open();
                var HdongQuery = "select TenVaiTro from VaiTroHoiDong";
                SqlCommand sqlCommand = new SqlCommand(HdongQuery, con);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vaitroHoidong.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
        }
    }
}
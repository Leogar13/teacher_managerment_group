using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class TenLop
    {
        public List<string> tenLop = new List<string>()
        {
            "HTTT",
            "KTPN",
            "CNTT",
            "MMT",
            "KHMT",
            "Dien tu vien thong",
            "Co khi",
            "Hoa ly ki thuat",
            "Hang khong vu tru"
        };
        public List<string> lopdangco = new List<string>();
        public TenLop()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (lopdangco.Count > 0)
            {
                lopdangco.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var TenLopQuery = "select TenLop from Lop";
                SqlCommand sqlCommand = new SqlCommand(TenLopQuery, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lopdangco.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace TeacherManagementSystem.Models
{
    public class LichSuDiaChiLienHe
    {
        public List<DiaChiLienHe> lichSu = new List<DiaChiLienHe>();

        public LichSuDiaChiLienHe()
        {

        }

        public LichSuDiaChiLienHe(int mgv)
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using(var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "exec DanhSachDiaChiGiaoVienTheoId @MGV = @id";
                SqlCommand cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", mgv);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var dchi = new DiaChiLienHe()
                        {
                            MaGV = mgv,
                            MaLienHe = reader.GetInt32(1),
                            ThoiGian = reader.GetDateTime(2),
                            Phuong = reader.GetString(3),
                            Quan = reader.GetString(4),
                            ThanhPho = reader.GetString(5),
                            DTDiDong = reader.GetString(6),
                            DTNhaRieng = reader.GetString(7),
                            Email = reader.GetString(8)
                        };
                        lichSu.Add(dchi);
                    }
                }

            }
        }
    }
}
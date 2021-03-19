using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace TeacherManagementSystem.Models
{
    public class DinhMuc_Tai
    {
        public int DinhMucDaoTao { get; set; }
        public int DinhMucNghienCuu { get; set; }
        public double TaiYeuCau_DaoTao { get; set; }
        public double TaiYeuCau_NghienCuu { get; set; }
        //Dao Tao
        public double TaiGiangDay { get; set; }
        public double TaiHoiDong { get; set; }
        public double TaiKhaoThi { get; set; }
        public double TaiHuongDan { get; set; }

        //Nghien Cuu

        public double ThucTai_DaoTao { get; set; }
        public double ThucTai_NghienCuu { get; set; }


        public DinhMuc_Tai()
        {

        }

        public DinhMuc_Tai(int id)
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            TinhDinhMuc(id, cn);
            TinhTaiDaoTao(id, cn);
            TinhTaiNghienCuu(id, cn);


        }

        public void TinhTaiDaoTao(int id, string cn)
        {
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "exec TinhTaiDaoTao @MGV = @id";
                var cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TaiGiangDay = reader.GetDouble(3);
                        TaiHoiDong = reader.GetDouble(4);
                        TaiKhaoThi = reader.GetDouble(5);
                        TaiHuongDan = reader.GetDouble(6);
                        ThucTai_DaoTao = reader.GetDouble(7);
                    }
                }
                reader.Close();
            }
        }
        public void TinhTaiNghienCuu(int id, string cn)
        {
            ThucTai_NghienCuu = 0;
             using(var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "select * from DanhSachCongTrinh_GV where MaGV = @id";
                var cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var maCTKH = reader.GetInt32(0);
                        var maVT = reader.GetInt32(2);
                        var maLoaiCT = reader.GetInt32(3);
                        var gioChuan = reader.GetInt32(4);
                        var maLoaiNC = reader.GetInt32(5);
                        var So = reader.GetInt32(6);
                        var soTacGia = reader.GetInt32(7);
                        switch (maLoaiCT)
                        {
                            case 1: case 2: case 3: case 4: case 5:
                                    ThucTai_NghienCuu += gioChuan * So / soTacGia;
                                    break;
                            case 6: case 8: case 9:
                                var gc = gioChuan * So;
                                if(maVT == 5)
                                {
                                    ThucTai_NghienCuu += gc * 0.2 + (gc * 0.8) / soTacGia;
                                }
                                if(maVT == 6 || maVT == 7)
                                {
                                    ThucTai_NghienCuu += (gc * 0.8) / soTacGia;
                                }
                                break;
                            case 10:
                                ThucTai_NghienCuu += gioChuan * So / soTacGia;
                                break;
                            case 11: case 12: case 13: case 16:
                                gc = gioChuan * So;
                                if(maVT == 8)
                                {
                                    ThucTai_NghienCuu += gc * 0.2 + (gc * 0.8) / soTacGia;
                                }
                                if(maVT == 9)
                                {
                                    ThucTai_NghienCuu += (gc * 0.8) / soTacGia;
                                }
                                break;
                        }
                    }
                }


            }
        }

        public void TinhDinhMuc(int id, string cn)
        {
            using(var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "exec TinhDinhMuc @MGV = @id";
                var cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DinhMucDaoTao = reader.GetInt32(3);
                        DinhMucNghienCuu = reader.GetInt32(4);
                        TaiYeuCau_DaoTao = reader.GetDouble(5);
                        TaiYeuCau_NghienCuu = reader.GetDouble(6);
                    }
                }
                reader.Close();                
            }
        }


    }
}
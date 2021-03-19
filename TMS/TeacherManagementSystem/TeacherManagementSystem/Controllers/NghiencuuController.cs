using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherManagementSystem.Controllers;
using TeacherManagementSystem.Models;

namespace TeacherManagementSystem.Controllers
{
    public class NghiencuuController : Controller
    {
        public string cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
        
        //GET: Nghiencuu/ThemNghiencuu
        public ActionResult ThemNghienCuu()
        {
            List<string> NC = new List<string>();
            List<string> CT = new List<string>();
            Donvitinh dvt = new Donvitinh();
            using (var connection = new SqlConnection(cn))
            {
                //Tên loại nghiên cứu
                connection.Open();
                var TenloaiQuery = "select TenLoaiNC from LoaiNghienCuu";
                SqlCommand sqlCommand = new SqlCommand(TenloaiQuery, connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        NC.Add(reader.GetString(0));
                    }
                }
                reader.Close();            
                connection.Close();
                ViewBag.TenloaiNC = NC;

                //Tên loại công trình tương ứng
                
                connection.Open();
                var TenCTQuery = "select TenLoaiCT from LoaiCongTrinh lct, LoaiNC_LoaiCT, LoaiNghienCuu lnc where lct.MaLoaiCT = LoaiNC_LoaiCT.MaLoaiCT and lnc.MaLoaiNC = LoaiNC_LoaiCT.MaLoaiNC and lnc.TenLoaiNC = N'" + NC.FirstOrDefault() +"'";
                sqlCommand = new SqlCommand(TenCTQuery, connection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        CT.Add(read.GetString(0));
                    }
                }
                read.Close();
                connection.Close();
                ViewBag.TenloaiCT = CT;
                ViewBag.Dvtinh = dvt.Dvtinh;

                //List giao vien
                List<GiaoVien> GV = new List<GiaoVien>();
                connection.Open();
                var GVQuery = "Select MaGV, TenGiaoVien from GiaoVien";
                sqlCommand = new SqlCommand(GVQuery, connection);
                SqlDataReader readgv = sqlCommand.ExecuteReader();
                if (readgv.HasRows)
                {
                    while (readgv.Read())
                    {
                        var id = readgv.GetInt32(0);
                        var name = readgv.GetString(1);
                        var teacher = new GiaoVien() { MaGV = id, HoTen = name };
                        GV.Add(teacher);
                    }
                }
                readgv.Close();
                connection.Close();
                ViewBag.ListGV = GV;
            }
            return View();
        }        
        
        //GET: Nghiencuu/PhancongNghiencuu
        public ActionResult PhancongNghiencuu()
        {
            //List ten cong trinh
            using (var connection = new SqlConnection(cn))
            {                
                connection.Open();
                var queryso = "Select count(MaCTKH) from CongTrinhKhoaHoc where MaLoaiNC = 1";
                SqlCommand sqlCommand = new SqlCommand(queryso, connection);
                var so = Convert.ToInt32(sqlCommand.ExecuteScalar());
                connection.Close();
                if (so == 0)
                {
                    return RedirectToAction("ThemNghienCuu", "Nghiencuu");
                }

            }
            
            //List ten cong trinh
            using (var connection = new SqlConnection(cn))
            {
                List<string> NC = new List<string>();
                //Tên loại nghiên cứu
                connection.Open();
                var TenloaiNCQuery = "select TenLoaiNC from LoaiNghienCuu";
                SqlCommand sqlCommand = new SqlCommand(TenloaiNCQuery, connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        NC.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                connection.Close();
                ViewBag.TenloaiNC = NC;
                
                List<CongTrinhKH> CT = new List<CongTrinhKH>();
                
                //Tên loại nghiên cứu
                connection.Open();
                var TenCTQuery = "select CongTrinhKhoaHoc.MaCTKH, CongTrinhKhoaHoc.TenCTNC from CongTrinhKhoaHoc ,LoaiNghienCuu where CongTrinhKhoaHoc.MaLoaiNC = LoaiNghienCuu.MaLoaiNC and LoaiNghienCuu.TenLoaiNC = N'"+ NC.FirstOrDefault() +"'";
                sqlCommand = new SqlCommand(TenCTQuery, connection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        var ma = read.GetInt32(0);
                        var ten = read.GetString(1);
                        var congtrinh = new CongTrinhKH() { id = ma, name = ten};
                        CT.Add(congtrinh);
                    }
                }
                read.Close();
                connection.Close();
                ViewBag.TenCTNC = CT;

                //Vai trò trong nghiên cứu
                List<VaiTro> VT = new List<VaiTro>();
                connection.Open();
                var TenVTQuery = "select VaiTro.MaVT ,VaiTro.TenVaiTro from VaiTro inner join LoaiNghienCuu on VaiTro.MaLoaiNC = LoaiNghienCuu.MaLoaiNC and LoaiNghienCuu.TenLoaiNC = N'" + NC.FirstOrDefault() + "'";
                sqlCommand = new SqlCommand(TenVTQuery, connection);
                SqlDataReader rd = sqlCommand.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        var ma = rd.GetInt32(0);
                        var ten = rd.GetString(1);
                        var vaitro = new VaiTro { MaVaitro = ma, TenVaitro = ten };
                        VT.Add(vaitro);
                    }
                }
                rd.Close();
                connection.Close();
                ViewBag.TenVaitro = VT;

                //Get đơn vị tính                
                connection.Open();
                var query = "select DonViTinh.DonVi from DonViTinh, CongTrinhKhoaHoc where CongTrinhKhoaHoc.MaDVT = DonViTinh.MaDVT and CongTrinhKhoaHoc.MaCTKH = " + CT.FirstOrDefault().id + "";
                sqlCommand = new SqlCommand(query, connection);
                var dvt = Convert.ToString(sqlCommand.ExecuteScalar());
                connection.Close();
                ViewBag.Donvitinh = dvt;

                //Get số
                connection.Open();
                var queryso = "select DonViTinh.So from DonViTinh, CongTrinhKhoaHoc where CongTrinhKhoaHoc.MaDVT = DonViTinh.MaDVT and CongTrinhKhoaHoc.MaCTKH = " + CT.FirstOrDefault().id + "";
                sqlCommand = new SqlCommand(queryso, connection);
                var so = Convert.ToString(sqlCommand.ExecuteScalar());
                connection.Close();
                ViewBag.So = so;

                //List giao vien
                List<GiaoVien> GV = new List<GiaoVien>();
                connection.Open();
                var GVQuery = "Select MaGV, TenGiaoVien from GiaoVien";
                sqlCommand = new SqlCommand(GVQuery, connection);
                SqlDataReader readgv = sqlCommand.ExecuteReader();
                if (readgv.HasRows)
                {
                    while (readgv.Read())
                    {
                        var id = readgv.GetInt32(0);
                        var name = readgv.GetString(1);
                        var teacher = new GiaoVien() { MaGV = id, HoTen = name };
                        GV.Add(teacher);
                    }
                }
                readgv.Close();
                connection.Close();
                ViewBag.ListGV = GV;
            }
            return View();
        }

        //GET: Nghiencuu/NghiencuuHoanthanh
        public ActionResult Index()
        {
            using (var connection = new SqlConnection(cn))
            {                
                List<CongTrinhKH> CT = new List<CongTrinhKH>();
                //Tên Công trình nghiên cứu
                connection.Open();
                var TenCTQuery = "Select * from ShowCTKH";
                SqlCommand sqlCommand = new SqlCommand(TenCTQuery, connection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        var ma = read.GetInt32(0);
                        var ten = read.GetString(1);
                        var loaiCT = read.GetString(2);
                        var sotacgia = read.GetInt32(3);
                        var so = read.GetInt32(4);
                        var donvi = read.GetString(5);
                        var loaiNC = read.GetString(6);
                        var congtrinh = new CongTrinhKH() {
                            id = ma,
                            name = ten,
                            nameloaiCT = loaiCT,
                            Sotacgia = sotacgia,
                            So= so,
                            namedonvitinh = donvi,
                            nameloaiNC = loaiNC,
                        };
                        CT.Add(congtrinh);
                    }
                }
                read.Close();
                connection.Close();
                ViewBag.TenCTKH = CT;
            } 
            return View();
        }

        //POST: Nghiencuu/Themnghiencuu
        [HttpPost]
        public ActionResult ThemNghienCuu(FormCollection form)
        {
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var insertquery = @"exec Them_NVNC @tenloainghiencuu, @tenCTNC, @loaiCT, @so, @donvitinh, @sotacgia";
                using (SqlCommand cmd = new SqlCommand(insertquery, c))
                {                    
                    cmd.Parameters.AddWithValue("@tenloainghiencuu", form["tenloainghiencuu"]);
                    cmd.Parameters.AddWithValue("@tenCTNC", form["inputTencongtrinh"]);
                    cmd.Parameters.AddWithValue("@loaiCT", form["TenloaiCT"]);
                    cmd.Parameters.AddWithValue("@so", form["so"]);
                    cmd.Parameters.AddWithValue("@donvitinh", form["donvitinh"]);
                    cmd.Parameters.AddWithValue("@sotacgia", form["sogiaovien"]);
                    cmd.ExecuteNonQuery();
                }

            }            
            return RedirectToAction("ThemNghienCuu", "Nghiencuu");            
        }
        
        //POST: Nghiencuu/Themnghiencuu
        [HttpPost]
        public ActionResult PhancongNghiencuu(FormCollection form)
        {
            try
            {
                int count = 0;
                foreach (var key in form.Keys)
                {
                    if (key.ToString().StartsWith("PCNCTengiaovien_"))
                    {
                        count++;
                        string[] s = key.ToString().Split('_');
                        using (var c = new SqlConnection(cn))
                        {
                            c.Open();
                            var CT = "tenCTKH";
                            var GV = "PCNCTengiaovien_" + s[1];
                            var VT = "PCNCvaitro_" + s[1];
                            var insertquery = "insert into PhanCong_NghienCuu values (@MaGiaovien,@MaCTKH,@MaVaitro)";
                            using (SqlCommand cmd = new SqlCommand(insertquery, c))
                            {
                                var t1 = form[CT];
                                var t2 = form[GV];
                                var t3 = form[VT];
                                cmd.Parameters.AddWithValue("@MaCTKH", t1);
                                cmd.Parameters.AddWithValue("@MaGiaovien", form[GV]);
                                cmd.Parameters.AddWithValue("@MaVaitro", form[VT]);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }

                        }
                    }
                }

                int sotacgia;
                using (var connection = new SqlConnection(cn))
                {
                    connection.Open();
                    var query = "select SoTacGia from CongTrinhKhoaHoc where MaCTKH = " + form["tenCTKH"];
                    var cmd = new SqlCommand(query, connection);
                    sotacgia = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                }

                if (sotacgia != count)
                {
                    using (var c = new SqlConnection(cn))
                    {
                        c.Open();
                        var updatequery = @"update CongTrinhKhoaHoc set SoTacGia = " + count + " where MaCTKH = " + form["tenCTKH"];
                        using (SqlCommand cmd = new SqlCommand(updatequery, c))
                        {
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
                
                return RedirectToAction("PhancongNghiencuu", "Nghiencuu");
            }
            catch (Exception)
            {
                return RedirectToAction("PhancongNghiencuu", "Nghiencuu");
                throw new Exception("Something was wrong");                
            }
            
        }

        //AJAX: List loại công trình tương ứng loại nghiên cứu
        public JsonResult GetLoaiCongtrinh(string id)
        {
            List<string> CT = new List<string>();
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                var query = "select TenLoaiCT from LoaiCongTrinh lct, LoaiNC_LoaiCT, LoaiNghienCuu lnc where lct.MaLoaiCT = LoaiNC_LoaiCT.MaLoaiCT and lnc.MaLoaiNC = LoaiNC_LoaiCT.MaLoaiNC and lnc.TenLoaiNC = N'" + id + "'";
                var cmd = new SqlCommand(query, connection);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        CT.Add(read.GetString(0));
                    }
                }
                read.Close();
                connection.Close();
            }
            return Json(CT, JsonRequestBehavior.AllowGet);

        }

        //AJAX: List loại công trình tương ứng loại nghiên cứu
        public JsonResult GetTenCongtrinh(string id)
        {
            List<CongTrinhKH> tenCT = new List<CongTrinhKH>();
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                var TenCTQuery = "select CongTrinhKhoaHoc.MaCTKH, CongTrinhKhoaHoc.TenCTNC from CongTrinhKhoaHoc ,LoaiNghienCuu where CongTrinhKhoaHoc.MaLoaiNC = LoaiNghienCuu.MaLoaiNC and LoaiNghienCuu.TenLoaiNC = N'" + id + "'";
                SqlCommand sqlCommand = new SqlCommand(TenCTQuery, connection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        var ma = read.GetInt32(0);
                        var ten = read.GetString(1);
                        var congtrinh = new CongTrinhKH() { id = ma, name = ten };
                        tenCT.Add(congtrinh);
                    }
                }
                read.Close();
                connection.Close();
            }
            return Json(tenCT, JsonRequestBehavior.AllowGet);

        }

        //AJAX: List Ten vai tro
        public JsonResult GetTenVaitro(string id)
        {
            List<VaiTro> tenVT = new List<VaiTro>();
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                var TenCTQuery = "select VaiTro.MaVT ,VaiTro.TenVaiTro from VaiTro inner join LoaiNghienCuu on VaiTro.MaLoaiNC= LoaiNghienCuu.MaLoaiNC and LoaiNghienCuu.TenLoaiNC = N'" + id + "'";
                SqlCommand sqlCommand = new SqlCommand(TenCTQuery, connection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        var ma = read.GetInt32(0);
                        var ten = read.GetString(1);
                        var vaitro = new VaiTro { MaVaitro = ma, TenVaitro = ten };
                        tenVT.Add(vaitro);
                    }
                }
                read.Close();
                connection.Close();
            }
            return Json(tenVT, JsonRequestBehavior.AllowGet);
        }

        //AJAX: Đơn vị tính
        public JsonResult Getdonvitinh(int id)
        {
            string dvt;
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                var query = "select DonViTinh.DonVi from DonViTinh, CongTrinhKhoaHoc where CongTrinhKhoaHoc.MaDVT = DonViTinh.MaDVT and CongTrinhKhoaHoc.MaCTKH = " + Convert.ToString(id) + "";
                var cmd = new SqlCommand(query, connection);
                dvt = Convert.ToString(cmd.ExecuteScalar());
                connection.Close();
            }
            return Json(dvt, JsonRequestBehavior.AllowGet);

        }

        //AJAX: Get số
        public JsonResult Getso(int id)
        {
            string so;
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                var query = "select DonViTinh.So from DonViTinh, CongTrinhKhoaHoc where CongTrinhKhoaHoc.MaDVT = DonViTinh.MaDVT and CongTrinhKhoaHoc.MaCTKH = " + Convert.ToString(id) + "";
                var cmd = new SqlCommand(query, connection);
                so = Convert.ToString(cmd.ExecuteScalar());
                connection.Close();
            }
            return Json(so, JsonRequestBehavior.AllowGet);

        }

    }
}
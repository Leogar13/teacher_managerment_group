using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherManagementSystem.Models;

namespace TeacherManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public string cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
        DSCV_GiangDay gd = new DSCV_GiangDay();
        GioChuan_GiangDay tenGCDG = new GioChuan_GiangDay();
        TenLop tenLop = new TenLop();
        HocPhan hocPhan = new HocPhan();

        GioChuan_HuongDan tenGCHD = new GioChuan_HuongDan();
        DSCV_HuongDan hd = new DSCV_HuongDan();

        GioChuan_HoiDong tenHoiDong = new GioChuan_HoiDong();
        DSCV_HoiDong hdong = new DSCV_HoiDong();
        VaitroHoidong vaitro = new VaitroHoidong();

        GioChuan_KhaoThi tenKhaoThi = new GioChuan_KhaoThi();
        DSCV_KhaoThi kt = new DSCV_KhaoThi();

        GiaoVien gv = new GiaoVien();
        
        public ActionResult Index()
        {
            return View();
        }

        //GET: Home/ThemDaotao        
        public ActionResult ThemDaotao()
        {
            //lay id moi cua tat ca cac cong viec dao tao
            //Giang day
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT IDENT_CURRENT ('GiangDay')", connection);
                int maxId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                var checkExist = "select count(1) from GiangDay";
                var cmd = new SqlCommand(checkExist, connection);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                var newMaxId_GD = maxId;
                if (count != 0)
                {
                    newMaxId_GD = maxId + 1;
                }

                ViewBag.newId_GD = newMaxId_GD;
                connection.Close();
            }
            //Huong dan
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT IDENT_CURRENT ('HuongDan')", connection);
                int maxId_HD = Convert.ToInt32(sqlCommand.ExecuteScalar());
                var checkExist = "select count(1) from HuongDan";
                var cmd = new SqlCommand(checkExist, connection);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                var newMaxId_HD = maxId_HD;
                if (count != 0)
                {
                    newMaxId_HD = maxId_HD + 1;
                }

                ViewBag.newId_HD = newMaxId_HD;
                connection.Close();
            }
            //Khao Thi
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT IDENT_CURRENT ('KhaoThi')", connection);
                int maxId_KT = Convert.ToInt32(sqlCommand.ExecuteScalar());
                var checkExist = "select count(1) from KhaoThi";
                var cmd = new SqlCommand(checkExist, connection);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                var newMaxId_KT = maxId_KT;
                if (count != 0)
                {
                    newMaxId_KT = maxId_KT + 1;
                }

                ViewBag.newId_KT = newMaxId_KT;
                connection.Close();
            }
            //Hoi Dong
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT IDENT_CURRENT ('HoiDong')", connection);
                int maxId_HDONG = Convert.ToInt32(sqlCommand.ExecuteScalar());
                var checkExist = "select count(1) from HoiDong";
                var cmd = new SqlCommand(checkExist, connection);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                var newMaxId_HDONG = maxId_HDONG;
                if (count != 0)
                {
                    newMaxId_HDONG = maxId_HDONG + 1;
                }

                ViewBag.newId_HDONG = newMaxId_HDONG;
                connection.Close();
            }
            //ViewBag cua CVGD
            ViewBag.tenCVGD = gd.dscv_GiangDay;
            ViewBag.tenGCGD = tenGCDG.giochuan_GD;
            ViewBag.tenlop = tenLop.tenLop;
            ViewBag.tenHP = hocPhan.hocPhan;

            //ViewBag cua Huongdan
            ViewBag.tenCVHD = hd.dscv_HuongDan;
            ViewBag.tenGCHD = tenGCHD.giochuan_HD;
            ViewBag.lopDangCo = tenLop.lopdangco;

            //ViewBag cua HoiDong
            ViewBag.tenCVHDong = hdong.dscv_HoiDong;
            ViewBag.tenGCHdong = tenHoiDong.giochuan_HoiDong;

            //ViewBag cua KhaoThi
            ViewBag.tenCVKT = kt.dscv_KhaoThi;
            ViewBag.tenGCKT = tenKhaoThi.giochuan_KhaoThi;


            return View();
        }

        private object soTiet;
        private int soTinChi;

        public JsonResult GetSoTietHP(string id)
        {
           
            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                var query = "select SoTiet from HocPhan where TenHocPhan = N'"+ id + "'";
                var cmd = new SqlCommand(query, connection);
                soTiet = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();

            }
            return new JsonResult { Data = soTiet, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetSoTinChi(string id)
        {

            using (var connection = new SqlConnection(cn))
            {
                connection.Open();
                var query = "select SoTinChi from HocPhan where TenHocPhan = N'" + id + "'";
                var cmd = new SqlCommand(query, connection);
                soTinChi = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();

            }
            return new JsonResult { Data = soTinChi, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public DanhsachCVGD dsCVGD = new DanhsachCVGD();
        public DanhsachCVHD dsCVHD = new DanhsachCVHD();
        public DanhsachKT dsKT = new DanhsachKT();
        public DanhsachHoidong dsHDong = new DanhsachHoidong();

        //GET: Home/PhancongDaotao
        public ActionResult PhancongDaotao()
        {
            ViewBag.vaitro = vaitro.vaitroHoidong;

            ViewBag.dsCVGD = dsCVGD.danhsachGD;
            ViewBag.dsCVHD = dsCVHD.danhsachHD;
            ViewBag.dsKT = dsKT.danhsachKT;
            ViewBag.dsHDong = dsHDong.danhsachHDong;

            List<string> teachers = new List<string>();
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var listGiaoVien = @"select MaGV, TenGiaoVien from DanhSachGiaoVien;";
                SqlCommand sqlCommand = new SqlCommand(listGiaoVien, c);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        teachers.Add(reader.GetString(1) + "_" + reader.GetInt32(0));
                    }
                }
                reader.Close();
                ViewBag.Teachers = teachers;
            }

            return View();
        }

        private string QueryTenLop;
        private string QueryHocPhan;
        private int QuerySoTiet;

        public JsonResult PCGD_GetTenLop(string id)
        {
            string[] CVGD = id.Split('_');

            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var query = "select TenLop from Lop l, GiangDay gd where l.MaLop = gd.MaLop and gd.MaCVGD = " + CVGD[0];
                SqlCommand cmd = new SqlCommand(query, c);
                QueryTenLop = (string)cmd.ExecuteScalar();
                c.Close();
            }
            return new JsonResult { Data = QueryTenLop, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult PCGD_GetSoTiet(string id)
        {
            string[] CVGD = id.Split('_');

            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var query = "select SoTiet from Lop l, GiangDay gd, HocPhan hp where l.MaLop = gd.MaLop and hp.MaHocPhan = l.MaHocPhan and gd.MaCVGD = " + CVGD[0];
                SqlCommand cmd = new SqlCommand(query, c);
                QuerySoTiet =Convert.ToInt32(cmd.ExecuteScalar());
                c.Close();
            }
            return new JsonResult { Data = QuerySoTiet, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult PCGD_GetHocPhan(string id)
        {
            string[] CVGD = id.Split('_');

            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var query = "select TenHocPhan from Lop l, GiangDay gd, HocPhan hp where l.MaLop = gd.MaLop and hp.MaHocPhan = l.MaHocPhan and gd.MaCVGD = " + CVGD[0];
                SqlCommand cmd = new SqlCommand(query, c);
                QueryHocPhan = (string)cmd.ExecuteScalar();
                c.Close();
            }
            return new JsonResult { Data = QueryHocPhan, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private int QuerySoHV;
        public JsonResult PCHDan_GetSoHocVien(string id)
        {
            string[] CVHDan = id.Split('_');

            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var query = "select count(MaHocVien) from HuongDan_HocVien hh where hh.MaCVHDan = " + CVHDan[0];
                SqlCommand cmd = new SqlCommand(query, c);
                QuerySoHV = Convert.ToInt32(cmd.ExecuteScalar());
                c.Close();
            }
            return new JsonResult { Data = QuerySoHV, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private string KT_tenlop;
        public JsonResult PCKT_GetTenLop(string id)
        {
            string[] CVKT = id.Split('_');

            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var query = "select Tenlop from KhaoThi kt, Lop l where kt.MaLop = l.MaLop and kt.MaCVKT = " + CVKT[0];
                SqlCommand cmd = new SqlCommand(query, c);
                KT_tenlop = (string)cmd.ExecuteScalar();
                c.Close();
            }
            return new JsonResult { Data = KT_tenlop, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private string KT_HocPhan;
        public JsonResult PCKT_GetHocPhan(string id)
        {
            string[] CVKT = id.Split('_');

            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var query = "select TenHocPhan from HocPhan hp, Lop l, KhaoThi kt where hp.MaHocPhan = l.MaHocPhan and l.MaLop =  kt.Malop and kt.MaCVKT = " + CVKT[0];
                SqlCommand cmd = new SqlCommand(query, c);
                KT_HocPhan = (string)cmd.ExecuteScalar();
                c.Close();
            }
            return new JsonResult { Data = KT_HocPhan, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private string HDong_TenHDong;
        public JsonResult PCHDong_GetTen(string id)
        {
            string[] CVHDong = id.Split('_');
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var query = "select TenHoiDong from HoiDong hd where hd.MaCVHD = " + CVHDong[0];
                SqlCommand cmd = new SqlCommand(query, c);
                HDong_TenHDong = (string)cmd.ExecuteScalar();
                c.Close();
            }
            return new JsonResult { Data = HDong_TenHDong, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //POST Home/PhancongGiangday
        [HttpPost]
        public ActionResult PhanCongGiangday(FormCollection form)
        {
            int countGV = 0;
            foreach (var key in form.Keys)
            {
                if (key.ToString().StartsWith("PCGDTengiaovien_"))
                {
                    countGV++;
                }
            }
            using (var con = new SqlConnection(cn))
            {
                con.Open();
                string[] maCV = form["tenCVGD"].Split('_');
                for (int i = 0; i < countGV; i++)
                {
                    var keyTenGV = "PCGDTengiaovien_" + i;
                    var keySoTiet = "PCGDSotietday_" + i;
                    string[] maGV = form[keyTenGV].Split('_');
                    var query = "insert into PhanCong_GiangDay values(@magv, @macv, @sotiet)";
                    using(var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@magv", maGV[1]);
                        cmd.Parameters.AddWithValue("@macv", maCV[0]);
                        cmd.Parameters.AddWithValue("@sotiet", form[keySoTiet]);
                        cmd.ExecuteNonQuery();
                    }
                    
                }
            }

            return RedirectToAction("PhanCongDaoTao", "Home");

        }
        //POST Home/PhancongKhaothi
        [HttpPost]
        public ActionResult PhanCongKhaothi(FormCollection form)
        {
            int countGV = 0;
            foreach (var key in form.Keys)
            {
                if (key.ToString().StartsWith("PCKTTenGV_"))
                {
                    countGV++;
                }
            }
            using (var con = new SqlConnection(cn))
            {
                con.Open();
                string[] maCV = form["tenCVKT"].Split('_');
                string tenHP = "";
                var q1 = "select TenHocPhan from HocPhan hp, Lop l, KhaoThi kt where hp.MaHocPhan = l.MaHocPhan and l.MaLop =  kt.Malop and kt.MaCVKT = " + maCV[0];
                SqlCommand command = new SqlCommand(q1, con);
                tenHP = (string)command.ExecuteScalar();

                var q2 = "select MaHocPhan from HocPhan hp where hp.TenHocPhan = N'" + tenHP + "'";
                SqlCommand command2 = new SqlCommand(q2, con);
                int maHP = 0;
                maHP = Convert.ToInt32(command2.ExecuteScalar());
                for (int i = 0; i < countGV; i++)
                {
                    var keyTenGV = "PCKTTenGV_" + i;
                    var keySoTiet = "PCKTSobai_" + i;
                    string[] maGV = form[keyTenGV].Split('_');
                    var query = "insert into PhanCong_KhaoThi values(@magv, @macv, @sobai, @mahp)";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@magv", maGV[1]);
                        cmd.Parameters.AddWithValue("@macv", maCV[0]);
                        cmd.Parameters.AddWithValue("@sobai", form[keySoTiet]);
                        cmd.Parameters.AddWithValue("@mahp", maHP);
                        cmd.ExecuteNonQuery();
                    }

                }
            }

            return RedirectToAction("PhanCongDaoTao", "Home");
        }
        //POST Home/PhancongHuongdan
        [HttpPost]
        public ActionResult PhanCongHuongdan(FormCollection form)
        {
            int countGV = 0;
            foreach (var key in form.Keys)
            {
                if (key.ToString().StartsWith("PCHDanTenGV_"))
                {
                    countGV++;
                }
            }
            using (var con = new SqlConnection(cn))
            {
                con.Open();
                string[] maCV = form["tenCVHDan"].Split('_');
                for (int i = 0; i < countGV; i++)
                {
                    var keyTenGV = "PCHDanTenGV_" + i;
                    string[] maGV = form[keyTenGV].Split('_');
                    var query = "insert into PhanCong_HuongDan values(@magv, @macv)";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@magv", maGV[1]);
                        cmd.Parameters.AddWithValue("@macv", maCV[0]);
                        cmd.ExecuteNonQuery();
                    }

                }
            }

            return RedirectToAction("PhanCongDaoTao", "Home");

        }
        //POST Home/PhancongHoidong
        [HttpPost]
        public ActionResult PhanCongHoidong(FormCollection form)
        {
            int countGV = 0;
            foreach (var key in form.Keys)
            {
                if (key.ToString().StartsWith("PCHdongTenGV_"))
                {
                    countGV++;
                }
            }
            using (var con = new SqlConnection(cn))
            {
                con.Open();
                string[] maCV = form["tenCVHDong"].Split('_');
                for (int i = 0; i < countGV; i++)
                {
                    var keyTenGV = "PCHdongTenGV_" + i;
                    var keyVT = "PCHdongVaiTro_" + i;
                    var keyLan = "PCHdongSolan_" + i;

                    string[] maGV = form[keyTenGV].Split('_');
                    string tenVT = form[keyVT];
                    var queryMaVT = "select MaVTHD from VaiTroHoiDong vt where vt.TenVaiTro = N'" + tenVT + "'";
                    var command = new SqlCommand(queryMaVT, con);
                    int maVT = 0;
                    maVT = Convert.ToInt32(command.ExecuteScalar());
                    var query = "insert into PhanCong_HoiDong values (@magv, @macv, @mavt, @solan)";

                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@magv", maGV[1]);
                        cmd.Parameters.AddWithValue("@macv", maCV[0]);
                        cmd.Parameters.AddWithValue("@mavt", maVT);
                        cmd.Parameters.AddWithValue("@solan", form[keyLan]);
                        cmd.ExecuteNonQuery();
                    }

                }
            }

            return RedirectToAction("PhanCongDaoTao", "Home");

        }


        //GET: Home/ThemNghiencuu
        public ActionResult ThemNghiencuu()
        {
            return View();
        }

        //GET: Home/PhancongNghiencuu
        public ActionResult PhancongNghiencuu()
        {
            return View();
        }

        //GET: Home/NghiencuuHoanthanh
        public ActionResult NghiencuuHoanthanh()
        {
            return View();
        }

        //POST: Home/ThemDaotao
        [HttpPost]
        public ActionResult ThemGiangday(FormCollection form)
        {
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var insertquery = @"exec Them_CVGiangDay @tenHP, @tenlop, @he, @siso, @tenCVGD, @tengiochuan";
                using (SqlCommand cmd = new SqlCommand(insertquery, c))
                {
                    cmd.Parameters.AddWithValue("@tenHP", form["tenhocphan"]);
                    cmd.Parameters.AddWithValue("@tenlop", form["tenlop"]);
                    cmd.Parameters.AddWithValue("@he", form["He"]);
                    cmd.Parameters.AddWithValue("@siso", form["Siso"]);
                    cmd.Parameters.AddWithValue("@tenCVGD", form["tenCVGD"]);
                    cmd.Parameters.AddWithValue("@tengiochuan", form["tengiochuan"]);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("ThemDaoTao","Home");
        }

        //POST: Home/ThemDaotao
        [HttpPost]
        public ActionResult ThemHuongdan(FormCollection form)
        {
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var insertquery = @"insert into HuongDan(MaGCHDan, TenCongViec)	select MaGCHDan, @tenCVHD	from GioChuan_HuongDan g	where g.TenGioChuan = @tengiochuan";
                using (SqlCommand cmd = new SqlCommand(insertquery, c))
                {
                    cmd.Parameters.AddWithValue("@tenCVHD", form["tenCVHD"]);
                    cmd.Parameters.AddWithValue("@tengiochuan", form["tengiochuan_HD"]);
                    cmd.ExecuteNonQuery();
                }

                SqlCommand sqlCommand = new SqlCommand("SELECT IDENT_CURRENT ('Huongdan')", c);
                int id_HD = Convert.ToInt32(sqlCommand.ExecuteScalar());


                int countHV = 0;
                foreach (var key in form.Keys)
                {
                    if (key.ToString().StartsWith("TenHV_"))
                    {
                        countHV++;
                    }
                }

                using (var con = new SqlConnection(cn))
                {
                    con.Open();
                    for (int i = 0; i < countHV; i++)
                    {
                        var keyTen = "TenHV_" + i;
                        var keyLop = "TenLopHV_" + i;
                        var keyDeTai = "TenDeTai_" + i;
                        var queryThemHV = @"exec Them_CVHD @maCV, @tenHV, @tenlop, @tendetai";
                        using (SqlCommand cmd = new SqlCommand(queryThemHV, con))
                        {
                            
                            cmd.Parameters.AddWithValue("@maCV", id_HD);
                            cmd.Parameters.AddWithValue("@tenHV", form[keyTen]);
                            cmd.Parameters.AddWithValue("@tenlop", form[keyLop]);
                            cmd.Parameters.AddWithValue("@tendetai", form[keyDeTai]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
            return RedirectToAction("ThemDaoTao", "Home");
        }

        //POST: Home/ThemDaotao
        [HttpPost]
        public ActionResult ThemKhaothi(FormCollection form)
        {
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var insertquery = @"exec Them_KhaoThi @tenCVKT, @tengiochuan, @tenlop";
                using (SqlCommand cmd = new SqlCommand(insertquery, c))
                {
                    cmd.Parameters.AddWithValue("@tenCVKT", form["tenCVKT"]);
                    cmd.Parameters.AddWithValue("@tengiochuan", form["tengiochuan"]);
                    cmd.Parameters.AddWithValue("@tenlop", form["tenlop"]);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("ThemDaoTao", "Home");
        }

        //POST: Home/ThemDaotao
        [HttpPost]
        public ActionResult ThemHoidong(FormCollection form)
        {
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var insertquery = @"exec Them_CVHDong @tenCVHDong, @tengiochuan, @tenHDong";
                using (SqlCommand cmd = new SqlCommand(insertquery, c))
                {
                    cmd.Parameters.AddWithValue("@tenCVHDong", form["tenCVHDong"]);
                    cmd.Parameters.AddWithValue("@tengiochuan", form["tengiochuan"]);
                    cmd.Parameters.AddWithValue("@tenHDong", form["tenhoidong"]);
                    cmd.ExecuteNonQuery();
                }

            }
            return RedirectToAction("ThemDaoTao", "Home");
        }
    }
}
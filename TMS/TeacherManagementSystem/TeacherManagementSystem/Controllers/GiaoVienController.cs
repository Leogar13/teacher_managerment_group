using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TeacherManagementSystem.Models;

namespace TeacherManagementSystem.Controllers
{
	public class GiaoVienController : Controller
	{
		public string cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
		TinhThanhPho tp = new TinhThanhPho();
		BoMon boMon = new BoMon();
		DateTime today = DateTime.Now;
		//GET: GiaoVien/Index
		public ActionResult Index()
		{
			List<TempTeacher> teachers = new List<TempTeacher>();
			using(var c = new SqlConnection(cn))
			{
				c.Open();
				var listGiaoVien = @"select MaGV, TenGiaoVien, QueQuan, GioiTinh, NgaySinh, TenKhoa, TenBoMon from DanhSachGiaoVien;";
				SqlCommand sqlCommand = new SqlCommand(listGiaoVien, c);
				SqlDataReader reader = sqlCommand.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						//0 - mgv, 1 - TenGV, 2 - QueQuan, 3 - GioiTinh, 4 - NgaySinh, 5 - TenKhoa, 6 - TenBoMon
						var maGV = reader.GetInt32(0);
						var tenGV = reader.GetString(1);
						var queQuan = reader.GetString(2);
						var gioiTinh = reader.GetString(3);
						var ngaySinh = reader.GetDateTime(4).ToString("dd/MM/yyyy");
						var tenKhoa = reader.GetString(5);
						var tenBoMon = reader.GetString(6);
						var teacher = new TempTeacher() { MaGV = maGV, BoMon = tenBoMon, GioiTinh = gioiTinh, HoTen = tenGV, NgaySinh = ngaySinh, QueQuan = queQuan, DonVi = tenKhoa };
						teachers.Add(teacher);
					}
				}
				reader.Close();
				ViewBag.Teachers = teachers;
				ViewBag.Today = today;
			}
			return View();
		}

		// GET: GiaoVien/Them
		public ActionResult Them()
		{
			//get max id
			using (var connection = new SqlConnection(cn))
			{
				connection.Open();
				//var getMaxTeacherId = "select IDENT_CURRENT('GiaoVien');";
				SqlCommand sqlCommand = new SqlCommand("SELECT IDENT_CURRENT ('GiaoVien')", connection);
				int maxId = Convert.ToInt32(sqlCommand.ExecuteScalar());
				var checkExist = "select count(1) from GiaoVien";
				var cmd = new SqlCommand(checkExist, connection);
				int count = Convert.ToInt32(cmd.ExecuteScalar());
				var newMaxId = maxId;
				if (count != 0)
				{
					newMaxId = maxId + 1;
				}

				ViewBag.newId = newMaxId;
				connection.Close();
			}	
			ViewBag.BoMon = boMon.boMon;
			ViewBag.TinhThanhPho = tp.tinhThanhPho;
			ViewBag.Today = today;
			return View();
		}

        //GET: GiaoVien/Sua
		public ActionResult Sua(int id)
		{
			//need to check if id exist
			using(var c = new SqlConnection(cn))
			{
				c.Open();
				var qCheckExist = "select * from GiaoVien where MaGV = @id";
				var cmd = new SqlCommand(qCheckExist, c);
				cmd.Parameters.AddWithValue("@id", id);
				var reader = cmd.ExecuteReader();
				if (!reader.HasRows)
				{
					return RedirectToAction("Index", "GiaoVien");
				}
				reader.Close();
			}
			var tp = new TinhThanhPho();
			var boMon = new BoMon();
			var trinhDo = new TrinhDo();
			var heDaoTao = new HeDaoTao();
			var hocVi = new HocVi();

			var danhSachCVCMNV = new DanhSachCVCMNV();
			var danhSachCVD = new DanhSachCVD();
			var danhSachCVCQ = new DanhSachCVCQ();
			var danhSachHocHam = new DanhSachHocHam();

			//constant
			ViewBag.TinhThanhPho = tp.tinhThanhPho;
			ViewBag.BoMon = boMon.boMon;
			ViewBag.HeDaoTao = heDaoTao.heDaoTao;
			ViewBag.TrinhDo = trinhDo.trinhDo;
			ViewBag.HocVi = hocVi.hocVi;

			ViewBag.HocHam = danhSachHocHam.list;
			ViewBag.CVCQ = danhSachCVCQ.list;
			ViewBag.CVCMNV = danhSachCVCMNV.list;
			ViewBag.CVD = danhSachCVD.list;
			ViewBag.Today = today;

			GiaoVien giaoVien = new GiaoVien(id);

            ViewBag.GiaoVien = giaoVien;
			return View();
		}

		//POST: GiaoVien/Them
		[HttpPost]
		public ActionResult Them(FormCollection form)
		{
			using(var c = new SqlConnection(cn))
			{
				int idBoMon;
				var layIdBoMon = @"select MaBoMon from BoMon where TenBoMon = @bomon;";
				c.Open();
				using (SqlCommand sqlCommand = new SqlCommand(layIdBoMon, c))
				{
					sqlCommand.Parameters.AddWithValue("@bomon", form["bomon"]);
					idBoMon = (int)sqlCommand.ExecuteScalar();
				}
				var timeFormat = TeacherManagementSystem.Resource.sqlTimeFormat;

				var insertQuery = @"insert into GiaoVien(MaBoMon, TenGiaoVien, QueQuan, GioiTinh, NgaySinh, NgayBatDau ) values (@MaBoMon, @TenGiaoVien, @QueQuan, @GioiTinh, @NgaySinh, @NgayBatDau);";
				using(SqlCommand sqlCommand = new SqlCommand(insertQuery, c))
				{
					sqlCommand.Parameters.AddWithValue("@MaBoMon", idBoMon);
					sqlCommand.Parameters.AddWithValue("@TenGiaoVien", form["ten"]);
					sqlCommand.Parameters.AddWithValue("@QueQuan", form["quequan"]);
					sqlCommand.Parameters.AddWithValue("@GioiTinh", form["gioitinh"]);
					sqlCommand.Parameters.AddWithValue("@NgaySinh", form["ngaysinh"]);
					sqlCommand.Parameters.AddWithValue("@NgayBatDau", form["ngayBatDau"]);
					sqlCommand.ExecuteNonQuery();
				}
				return RedirectToAction("Index", "GiaoVien");
			}
		}

		
		//POST: GiaoVien/Sua
		[HttpPost]
		public ActionResult Sua(FormCollection form)
		{
			// Every form field will have a prefix added to them.
			// naming: check update_form_naming_scheme.md

			var mgv = Convert.ToInt32(form["TT_MaGV"]);
			//-------------- Handle update for teacher's basic info ----------------//
			using(var c = new SqlConnection(cn))
			{
				c.Open();
				var qBoMon = "select MaBoMon from BoMon where TenBoMon = @tenBM";
				var cmd = new SqlCommand(qBoMon, c);
				cmd.Parameters.AddWithValue("@tenBM", form["TT_BoMon"]);
				int maBM = Convert.ToInt32(cmd.ExecuteScalar());
				cmd.Parameters.Clear();
				var qUpdateGV = "update GiaoVien set MaBoMon = @maBM, TenGiaoVien = @tenGV, QueQuan = @queQuan, GioiTinh = @gioiTinh, NgaySinh = @ngaySinh where MaGV = @mgv";
				cmd = new SqlCommand(qUpdateGV, c);
				cmd.Parameters.AddWithValue("@maBm", maBM);
				cmd.Parameters.AddWithValue("@tenGV", form["TT_HoTen"]);
                cmd.Parameters.AddWithValue("@queQuan", form["TT_QueQuan"]);
				cmd.Parameters.AddWithValue("@gioiTinh", form["TT_GioiTinh"]);
				DateTime ngaySinhGV = Convert.ToDateTime(form["TT_NgaySinh"]); 
				cmd.Parameters.AddWithValue("@ngaySinh", ngaySinhGV);
				cmd.Parameters.AddWithValue("@mgv", mgv);
				cmd.ExecuteNonQuery();
			}
			
			foreach(var key in form.Keys)
			{
				//------------ Handle update for DCLH (prefix: "ls_"  --------------//

				if (key.ToString().StartsWith("ls_MLH"))
				{
					var MaLienHe = Convert.ToInt32(form[key.ToString()]);
					//update DiaChiLienHe
					using(var c = new SqlConnection(cn))
					{
						c.Open();
						var keyThoiGian = "ls_ThoiGian" + MaLienHe;
						DateTime thoiGian = Convert.ToDateTime(form[keyThoiGian]); 
						var keyPhuong = "ls_Phuong" + MaLienHe;
						var keyQuan = "ls_Quan" + MaLienHe;
						var keyThanhPho = "ls_ThanhPho" + MaLienHe;
						var keyDTNR = "ls_DTNR" + MaLienHe;
						var keyDTDD = "ls_DTDD" + MaLienHe;
						var keyEmail = "ls_Email" + MaLienHe;

						// Update DCLH
						var qUpdateDCLH = @"update DiaChiLienHe 
												set Phuong = @phuong, Quan = @quan, ThanhPho = @thanhPho, DT_NhaRieng = @dtnr, DT_DiDong = @dtdd, Email = @email 
												where MaLienHe = @mlh;";
						var cmd = new SqlCommand(qUpdateDCLH, c);
						cmd.Parameters.AddWithValue("@phuong", form[keyPhuong]);
						cmd.Parameters.AddWithValue("@quan", form[keyQuan]);
						cmd.Parameters.AddWithValue("thanhPho", form[keyThanhPho]);
						cmd.Parameters.AddWithValue("@dtnr", form[keyDTNR]);
						cmd.Parameters.AddWithValue("@dtdd", form[keyDTDD]);
						cmd.Parameters.AddWithValue("@email", form[keyEmail]);
						cmd.Parameters.AddWithValue("@mlh", MaLienHe);
						cmd.ExecuteNonQuery();
						cmd.Parameters.Clear();

						//Update LS_DCLH
						var qUpdateLS_DCLH = @"update LichSuDCLH 
												set ThoiGian = @thoiGian 
												where MaGV = @mgv and MaLienHe = @mlh;";
						cmd = new SqlCommand(qUpdateLS_DCLH, c);
						cmd.Parameters.AddWithValue("@thoiGian", thoiGian);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@mlh", MaLienHe);
						cmd.ExecuteNonQuery();
					}
				}

				//------------- Handle update for ThanNhan (prefix: "tn_") -----------------//

				if (key.ToString().StartsWith("tn_MTN"))
				{
					var MaThanNhan = Convert.ToInt32(form[key.ToString()]);
					using(var c = new SqlConnection(cn))
					{
						c.Open();
						var keyTenTn = "tn_TenTN" + MaThanNhan;
						var keyGioiTinh = "tn_GioiTinh" + MaThanNhan;
						var keyNgheNghiep = "tn_NgheNghiep" + MaThanNhan;
						var keyNgaySinh = "tn_NgaySinh" + MaThanNhan;
						var ngaySinh = Convert.ToDateTime(form[keyNgaySinh]);

						var qUpdateThanNhan = @"update ThanNhan
													set TenThanNhan = @ten, NgaySinh = @ns, GioiTinh = @gt, NgheNghiep = @nn 
													where MaThanNhan = @mtn;";
						var cmd = new SqlCommand(qUpdateThanNhan, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTenTn]);
						cmd.Parameters.AddWithValue("@ns", ngaySinh);
						cmd.Parameters.AddWithValue("@gt", form[keyGioiTinh]);
						cmd.Parameters.AddWithValue("@nn", form[keyNgheNghiep]);
						cmd.Parameters.AddWithValue("@mtn", MaThanNhan);
						cmd.ExecuteNonQuery();
					}
				}

				//------------ Handle update for HocVan (prefix: "hv_") ------------------//

				if (key.ToString().StartsWith("hv_MHV"))
				{
					var MaHocVan = Convert.ToInt32(form[key.ToString()]);
					var keyNamCap = "hv_NamCap" + MaHocVan;
					var keyHocVi = "hv_HocVi" + MaHocVan;
					var keyTrinhDo = "hv_TrinhDo" + MaHocVan;
					var keyNoiDaoTao = "hv_NoiDaoTao" + MaHocVan;
					var keyNuocDaoTao = "hv_NuocDaoTao" + MaHocVan;
					var keyHeDaoTao = "hv_HeDaoTao" + MaHocVan;
					var keyTDBD = "hv_TDBD" + MaHocVan;
					var keyTDKT = "hv_TDKT" + MaHocVan;
					DateTime tdbd = Convert.ToDateTime(form[keyTDBD]);
					DateTime tdkt = Convert.ToDateTime(form[keyTDKT]);
					using(var c = new SqlConnection(cn))
					{
						c.Open();
						//look for MaHocVi from TenHocVi
						var qHocVi = @"select MaHocVi from HocVi
										where TenHocVi = @tenHocVi;";
						var cmd = new SqlCommand(qHocVi, c);
						cmd.Parameters.AddWithValue("@tenHocVi", form[keyHocVi]);
						int MaHocVi = Convert.ToInt32(cmd.ExecuteScalar());
						cmd.Parameters.Clear();
						//Update HocVan
						var qUpdateHocVan = @"update HocVan 
												set MaHocVi = @mHocVi, TenTrinhDo = @trinhDo, NuocDaoTao = @nuocDaoTao, HeDaoTao = @heDaoTao, NoiDaoTao = @noiDaoTao, NamCap = @namCap
												where MaHocVan = @mhv;";
						cmd = new SqlCommand(qUpdateHocVan, c);
						cmd.Parameters.AddWithValue("@mHocVi", MaHocVi);
						cmd.Parameters.AddWithValue("@trinhDo", form[keyTrinhDo]);
						cmd.Parameters.AddWithValue("@nuocDaoTao", form[keyNuocDaoTao]);
						cmd.Parameters.AddWithValue("@heDaoTao", form[keyHeDaoTao]);
						cmd.Parameters.AddWithValue("@noiDaoTao", form[keyNoiDaoTao]);
						cmd.Parameters.AddWithValue("@namCap", form[keyNamCap]);
						cmd.Parameters.AddWithValue("@mhv", MaHocVan);
						cmd.ExecuteNonQuery();
						cmd.Parameters.Clear();
						//Update HS_HocVan
						var qUpdateHS_HocVan = @"update HS_HocVan
													set ThoiDiemNhan = @tdbd, ThoiDiemKetThuc = @tdkt
													where MaGV = @mgv and MaHocVan = @mhv;";
						cmd = new SqlCommand(qUpdateHS_HocVan, c);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@mhv", MaHocVan);
						cmd.ExecuteNonQuery();
					}

					   
				}

				//------------- Handle update for CVD -------------------//

				if (key.ToString().StartsWith("cvd_MaCVD"))
				{
					var MaHS_CVD = Convert.ToInt32(form[key.ToString()]);
					using (var c = new SqlConnection(cn))
					{
						c.Open();
						var keyTDBD = "cvd_TDBD" + MaHS_CVD;
						var keyTDKT = "cvd_TDKT" + MaHS_CVD;
						DateTime tdbd = Convert.ToDateTime(form[keyTDBD]);
						DateTime tdkt = Convert.ToDateTime(form[keyTDKT]);
						var keyTenCVD = "cvd_TenCVD" + MaHS_CVD;

						var qMaCVDMoi = "select MaCVD from ChucVuDang where TenCVD = @ten";
						var cmd = new SqlCommand(qMaCVDMoi, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTenCVD]);
						int MaCVDMoi = Convert.ToInt32(cmd.ExecuteScalar());
						cmd.Parameters.Clear();
						//update
						var qUpdateHS_CVD = @"update HS_CVD
													set MaCVD = @maMoi, ThoiDiemNhan = @tdbd, ThoiDiemKetThuc = @tdkt
													where MaGV = @mgv and MaHS_CVD = @maHSCVD;";
						cmd = new SqlCommand(qUpdateHS_CVD, c);
						cmd.Parameters.AddWithValue("@maMoi", MaCVDMoi);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@maHSCVD", MaHS_CVD);
						cmd.ExecuteNonQuery();
					}

				}

				// ----------- Handle update for CVCQ ---------------------//

				if (key.ToString().StartsWith("cvcq_MaCVCQ"))
				{
					var MaHS_CVCQ = Convert.ToInt32(form[key.ToString()]);
					using (var c = new SqlConnection(cn))
					{
						c.Open();
						var keyTDBD = "cvcq_TDBD" + MaHS_CVCQ;
						var keyTDKT = "cvcq_TDKT" + MaHS_CVCQ;
						DateTime tdbd = Convert.ToDateTime(form[keyTDBD]);
						DateTime tdkt = Convert.ToDateTime(form[keyTDKT]);
						var keyTenCVCQ = "cvcq_TenCVCQ" + MaHS_CVCQ;
						var qMaCVCQMoi = "select MaCVCQ from ChucVuChinhQuyen where TenCVCQ = @ten";
						var cmd = new SqlCommand(qMaCVCQMoi, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTenCVCQ]);
						int MaCVCQMoi = Convert.ToInt32(cmd.ExecuteScalar());
						cmd.Parameters.Clear();
						//update
						var qUpdateHS_CVCQ = @"update HS_CVCQ
													set MaCVCQ = @maMoi, ThoiDiemNhan = @tdbd, ThoiDiemKetThuc = @tdkt
													where MaGV = @mgv and MaHS_CVCQ = @maHSCVCQ;";
						cmd = new SqlCommand(qUpdateHS_CVCQ, c);
						cmd.Parameters.AddWithValue("@maMoi", MaCVCQMoi);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@maHSCVCQ", MaHS_CVCQ);
						cmd.ExecuteNonQuery();
					}

				}

				// ---------- Handle update for CVCMNV ---------------------//

				if (key.ToString().StartsWith("cmnv_MaCMNV"))
				{
					var MaHS_CVCMNV = Convert.ToInt32(form[key.ToString()]);
					using (var c = new SqlConnection(cn))
					{
						c.Open();
						var keyTDBD = "cmnv_TDBD" + MaHS_CVCMNV;
						var keyTDKT = "cmnv_TDKT" + MaHS_CVCMNV;
						DateTime tdbd = Convert.ToDateTime(form[keyTDBD]);
						DateTime tdkt = Convert.ToDateTime(form[keyTDKT]);
						var keyTenCVCMNV = "cmnv_TenCMNV" + MaHS_CVCMNV;
						var qMaCMNVMoi = "select MaCVCMNV from CV_ChuyenMonNghiepVu where TenCVCMNV = @ten";
						var cmd = new SqlCommand(qMaCMNVMoi, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTenCVCMNV]);
						int MaCMNVMoi = Convert.ToInt32(cmd.ExecuteScalar());
						cmd.Parameters.Clear();
						//update
						var qUpdateHS_CVCMNV = @"update HS_CVCMNV
													set MaCVCMNV = @maMoi, ThoiDiemNhan = @tdbd, ThoiDiemKetThuc = @tdkt
													where MaGV = @mgv and MaHS_CVCMNV = @maHSCVCMNV;";
						cmd = new SqlCommand(qUpdateHS_CVCMNV, c);
						cmd.Parameters.AddWithValue("@maMoi", MaCMNVMoi);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@maHSCVCMNV", MaHS_CVCMNV);
						cmd.ExecuteNonQuery();
					}
				}

				// --------- Handle update for HocHam ----------------------//

				if (key.ToString().StartsWith("hh_MaHocHam"))
				{
					var MaHS_HocHam = Convert.ToInt32(form[key.ToString()]);
					using (var c = new SqlConnection(cn))
					{
						c.Open();
						var keyTDBD = "hh_TDBD" + MaHS_HocHam;
						var keyTDKT = "hh_TDKT" + MaHS_HocHam;
						DateTime tdbd = Convert.ToDateTime(form[keyTDBD]);
						DateTime tdkt = Convert.ToDateTime(form[keyTDKT]);
						var keyTenHocHam = "hh_TenHocHam" + MaHS_HocHam;
						var qMaHocHamMoi = "select MaHocHam from HocHam where TenHocHam = @ten";
						var cmd = new SqlCommand(qMaHocHamMoi, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTenHocHam]);
						int MaHocHamMoi = Convert.ToInt32(cmd.ExecuteScalar());
						cmd.Parameters.Clear();
						//update
						var qUpdateHS_HocHam = @"update HS_HocHam
													set MaHocHam = @maMoi, ThoiDiemNhan = @tdbd, ThoiDiemKetThuc = @tdkt
													where MaGV = @mgv and MaHS_HocHam = @maHSHocHam;";
						cmd = new SqlCommand(qUpdateHS_HocHam, c);
						cmd.Parameters.AddWithValue("@maMoi", MaHocHamMoi);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@maHSHocHam", MaHS_HocHam);
						cmd.ExecuteNonQuery();
					}
				}

				// --------- Handle new for DCLH ---------------------//

				if (key.ToString().StartsWith("ls_new_id"))
				{
					var newId = Convert.ToInt32(form[key.ToString()]);
					var p = "ls_new_"; //prefix
					var keyPhuong = p + "Phuong" + newId;
					var keyQuan = p + "Quan" + newId;
					var keyThanhPho = p + "ThanhPho" + newId;
					var keyDTNR = p + "DTNR" + newId;
					var keyDTDD = p + "DTDD" + newId;
					var keyThoiGian = p + "ThoiGian" + newId;
					var keyEmail = p + "Email" + newId;
					var thoiGian = Convert.ToDateTime(form[keyThoiGian]);

					using(var c = new SqlConnection(cn))
					{
						c.Open();
						var qInsert = "insert into DiaChiLienHe (Phuong, Quan, ThanhPho, DT_NhaRieng, DT_DiDong, Email) values ( @phuong, @quan, @thanhPho, @dtnr, @dtdd, @email);";
						var cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@phuong", form[keyPhuong]);
						cmd.Parameters.AddWithValue("@quan", form[keyQuan]);
						cmd.Parameters.AddWithValue("@thanhPho", form[keyThanhPho]);
						cmd.Parameters.AddWithValue("@dtnr", form[keyDTNR]);
						cmd.Parameters.AddWithValue("@dtdd", form[keyDTDD]);
						cmd.Parameters.AddWithValue("@email", form[keyEmail]);
						cmd.ExecuteNonQuery();
						cmd.Parameters.Clear();
						// insert into LichSuDCLH

						var qGetAddedId = "select IDENT_CURRENT('DiaChiLienHe')";
						cmd = new SqlCommand(qGetAddedId, c);
						int addedId = Convert.ToInt32(cmd.ExecuteScalar());
						qInsert = "insert into LichSuDCLH (MaGV, MaLienHe, ThoiGian) values (@mgv, @mlh, @tg)";
						cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@mlh", addedId);
						cmd.Parameters.AddWithValue("@tg", thoiGian);
						cmd.ExecuteNonQuery();
					}

				}

				// --------- Handle new for ThanNhan -------------//
				if (key.ToString().StartsWith("tn_new_id"))
				{
					var newId = Convert.ToInt32(form[key.ToString()]);
					var p = "tn_new_";
					var keyTen = p + "Ten" + newId;
					var keyGioiTinh = p + "GioiTinh" + newId;
					var keyNgaySinh = p + "NgaySinh" + newId;
					var keyNgheNghiep = p + "NgheNghiep" + newId;
					var ngaySinh = Convert.ToDateTime(form[keyNgaySinh]);

					using(var c = new SqlConnection(cn))
					{
						//insert ThanNhan
						c.Open();
						var qInsert = "insert into ThanNhan (TenThanNhan, NgaySinh, GioiTinh, NgheNghiep) values (@ten, @ngaySinh, @gioiTinh, @ngheNghiep);";
						var cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTen]);
						cmd.Parameters.AddWithValue("@ngaySinh", ngaySinh);
						cmd.Parameters.AddWithValue("@gioiTinh", form[keyGioiTinh]);
						cmd.Parameters.AddWithValue("@ngheNghiep", form[keyNgheNghiep]);
						cmd.ExecuteNonQuery();
						//Get added id and insert into GiaoVien_ThanNhan
						var qGetAddedId = "select IDENT_CURRENT('ThanNhan');";
						cmd = new SqlCommand(qGetAddedId, c);
						int addedId = Convert.ToInt32(cmd.ExecuteScalar());
						qInsert = "insert into GiaoVien_ThanNhan (MaGV, MaThanNhan) values (@mgv, @mtn);";
						cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@mtn", addedId);
						cmd.ExecuteNonQuery();
					}
				}

				// --------- Handle new for HocVan -------------//
				if (key.ToString().StartsWith("hv_new_id"))
				{
					var newId = Convert.ToInt32(form[key.ToString()]);
					var p = "hv_new_";
					var keyNamCap = p + "NamCap" + newId;
					var keyHocVi = p + "HocVi" + newId;
					var keyTrinhDo = p + "TrinhDo" + newId;
					var keyNoiDaoTao = p + "NoiDaotao" + newId;
					var keyNuocDaoTao = p + "NuocDaoTao" + newId;
					var keyHeDaoTao = p + "HeDaoTao" + newId;
					var keyNgayBatDau = p + "NgayBatDau" + newId;
					var keyNgayKetThuc = p + "NgayKetThuc" + newId;
					var tdbd = Convert.ToDateTime(form[keyNgayBatDau]);
					var tdkt = Convert.ToDateTime(form[keyNgayKetThuc]);

					using (var c = new SqlConnection(cn))
					{
						c.Open();
						// need to lookup MaHocVi
						var qMaHocVi = "select MaHocVi from HocVi where TenHocVi = @tenHocVi;";
						var cmd = new SqlCommand(qMaHocVi, c);
						cmd.Parameters.AddWithValue("@tenHocVi", form[keyHocVi]);
						int maHocVi = Convert.ToInt32(cmd.ExecuteScalar());
						// insert into HocVan
						var qInsert = "insert into HocVan (MaHocVi, TenTrinhDo, NuocDaoTao, HeDaoTao, NoiDaoTao, NamCap) values (@mhv, @trinhDo, @nuoc, @he, @noi, @nam);";
						cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@mhv", maHocVi);
						cmd.Parameters.AddWithValue("@trinhDo", form[keyTrinhDo]);
						cmd.Parameters.AddWithValue("@nuoc", form[keyNuocDaoTao]);
						cmd.Parameters.AddWithValue("@he", form[keyHeDaoTao]);
						cmd.Parameters.AddWithValue("@noi", form[keyNoiDaoTao]);
						cmd.Parameters.AddWithValue("@nam", form[keyNamCap]);
						cmd.ExecuteNonQuery();
						//insert into hs_hocvan
						
						var qGetAddedId = "select IDENT_CURRENT('HocVan');";
						cmd = new SqlCommand(qGetAddedId, c);
						int addedId = Convert.ToInt32(cmd.ExecuteScalar());
						qInsert = "insert into HS_HocVan (MaGV, MaHocVan, ThoiDiemNhan, ThoiDiemKetThuc) values (@mgv, @maHocVan, @tdbd, @tdkt)";
						cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@maHocVan", addedId);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.ExecuteNonQuery();
					}
					
					



				}

				// --------- Handle new for CVD ------------//
				if (key.ToString().StartsWith("cvd_new_id"))
				{
					var newId = Convert.ToInt32(form[key.ToString()]);
					var p = "cvd_new_";
					var keyTen = p + "TenCVD" + newId;
					var keyNgayBatDau = p + "NgayBatDau" + newId;
					var keyNgayKetThuc = p + "NgayKetThuc" + newId;
					//lookup id from name
					using(var c = new SqlConnection(cn))
					{
						c.Open();
						var qMaCVD = "select MaCVD from ChucVuDang where TenCVD = @ten";
						var cmd = new SqlCommand(qMaCVD, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTen]);
						int MaCVD = Convert.ToInt32(cmd.ExecuteScalar());
						var tdbd = Convert.ToDateTime(form[keyNgayBatDau]);
						var tdkt = Convert.ToDateTime(form[keyNgayKetThuc]);
						//insert into HS_CVD
						var qInsert = "insert into HS_CVD (MaGV, MaCVD, ThoiDiemNhan, ThoiDiemKetThuc) values (@mgv, @maCVD, @tdbd, @tdkt);";
						cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@maCVD", MaCVD);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.ExecuteNonQuery();
					}
				}

				// --------- Handle new for CVCQ -----------//
				if (key.ToString().StartsWith("cvcq_new_id"))
				{
					var newId = Convert.ToInt32(form[key.ToString()]);
					var p = "cvcq_new_";
					var keyTen = p + "TenCVCQ" + newId;
					var keyNgayBatDau = p + "NgayBatDau" + newId;
					var keyNgayKetThuc = p + "NgayKetThuc" + newId;
					//lookup id from name
					using (var c = new SqlConnection(cn))
					{
						c.Open();
						var qMaCVCQ = "select MaCVCQ from ChucVuChinhQuyen where TenCVCQ = @ten";
						var cmd = new SqlCommand(qMaCVCQ, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTen]);
						int MaCVCQ = Convert.ToInt32(cmd.ExecuteScalar());
						var tdbd = Convert.ToDateTime(form[keyNgayBatDau]);
						var tdkt = Convert.ToDateTime(form[keyNgayKetThuc]);
						//insert into HS_CVCQ
						var qInsert = "insert into HS_CVCQ (MaGV, MaCVCQ, ThoiDiemNhan, ThoiDiemKetThuc) values (@mgv, @maCVCQ, @tdbd, @tdkt);";
						cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@maCVCQ", MaCVCQ);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.ExecuteNonQuery();
					}
				}

				// -------- Handle new for CVCMNV ------------//
				if (key.ToString().StartsWith("cvcmnv_new_id"))
				{
					var newId = Convert.ToInt32(form[key.ToString()]);
					var p = "cvcmnv_new_";
					var keyTen = p + "TenCVCMNV" + newId;
					var keyNgayBatDau = p + "NgayBatDau" + newId;
					var keyNgayKetThuc = p + "NgayKetThuc" + newId;
					//lookup id from name
					using (var c = new SqlConnection(cn))
					{
						c.Open();
						var qMaCVCMNV = "select MaCVCMNV from CV_ChuyenMonNghiepVu where TenCVCMNV = @ten";
						var cmd = new SqlCommand(qMaCVCMNV, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTen]);
						int MaCVCMNV = Convert.ToInt32(cmd.ExecuteScalar());
						var tdbd = Convert.ToDateTime(form[keyNgayBatDau]);
						var tdkt = Convert.ToDateTime(form[keyNgayKetThuc]);
						//insert into HS_CVCMNV
						var qInsert = "insert into HS_CVCMNV (MaGV, MaCVCMNV, ThoiDiemNhan, ThoiDiemKetThuc) values (@mgv, @maCVCMNV, @tdbd, @tdkt);";
						cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@maCVCMNV", MaCVCMNV);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.ExecuteNonQuery();
					}
				}

				// -------- Handle new for HocHam ----------//
				if (key.ToString().StartsWith("hh_new_id"))
				{
					var newId = Convert.ToInt32(form[key.ToString()]);
					var p = "hh_new_";
					var keyTen = p + "TenHocHam" + newId;
					var keyNgayBatDau = p + "NgayBatDau" + newId;
					var keyNgayKetThuc = p + "NgayKetThuc" + newId;
					//lookup id from name
					using (var c = new SqlConnection(cn))
					{
						c.Open();
						var qMaHocHam = "select MaHocHam from HocHam where TenHocHam = @ten";
						var cmd = new SqlCommand(qMaHocHam, c);
						cmd.Parameters.AddWithValue("@ten", form[keyTen]);
						int MaHocHam = Convert.ToInt32(cmd.ExecuteScalar());
						var tdbd = Convert.ToDateTime(form[keyNgayBatDau]);
						var tdkt = Convert.ToDateTime(form[keyNgayKetThuc]);
						//insert into HS_HocHam
						var qInsert = "insert into HS_HocHam (MaGV, MaHocHam, ThoiDiemNhan, ThoiDiemKetThuc) values (@mgv, @maHocHam, @tdbd, @tdkt);";
						cmd = new SqlCommand(qInsert, c);
						cmd.Parameters.AddWithValue("@mgv", mgv);
						cmd.Parameters.AddWithValue("@maHocHam", MaHocHam);
						cmd.Parameters.AddWithValue("@tdbd", tdbd);
						cmd.Parameters.AddWithValue("@tdkt", tdkt);
						cmd.ExecuteNonQuery();
					}
				}

			}
			return Redirect("/GiaoVien/Sua/" + mgv);
		}

        [HttpPost]
		public ActionResult Xoa(DeleteIdentifier deleteItem)
		{
			var n = deleteItem.deleteName;
			var mgv = Convert.ToInt32(deleteItem.mgv);
			var delId = Convert.ToInt32(deleteItem.deleteId);
			if(n == "ls")
			{
				using(var c = new SqlConnection(cn))
				{
					c.Open();
					var qDeleteLichSuDCLH = "delete from LichSuDCLH where MaGV = @mgv and MaLienHe = @mlh;";
					var cmd = new SqlCommand(qDeleteLichSuDCLH, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					cmd.Parameters.AddWithValue("@mlh", delId);
					cmd.ExecuteNonQuery();
					var qDeleteDCLH = "delete from DiaChiLienHe where MaLienHe = @mlh;";
					cmd = new SqlCommand(qDeleteDCLH, c);
					cmd.Parameters.AddWithValue("@mlh", delId);
					cmd.ExecuteNonQuery();
				}
			}
			if(n == "tn")
			{
				using (var c = new SqlConnection(cn))
				{
					c.Open();
					var qDeleteGV_TN = "delete from GiaoVien_ThanNhan where MaGV = @mgv and MaThanNhan = @mtn;";
					var cmd = new SqlCommand(qDeleteGV_TN, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					cmd.Parameters.AddWithValue("@mtn", delId);
					cmd.ExecuteNonQuery();
					var qDeleteThanNhan = "delete from ThanNhan where MaThanNhan = @mtn;";
					cmd = new SqlCommand(qDeleteThanNhan, c);
					cmd.Parameters.AddWithValue("@mtn", delId);
					cmd.ExecuteNonQuery();
				}

			}
			if(n == "hv")
			{
				using (var c = new SqlConnection(cn))
				{
					c.Open();
					var qDeleteHS_HocVan = "delete from HS_HocVan where MaGV = @mgv and MaHocVan = @mhv;";
					var cmd = new SqlCommand(qDeleteHS_HocVan, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					cmd.Parameters.AddWithValue("@mhv", delId);
					cmd.ExecuteNonQuery();
					var qDeleteThanNhan = "delete from HocVan where MaHocVan = @mhv;";
					cmd = new SqlCommand(qDeleteThanNhan, c);
					cmd.Parameters.AddWithValue("@mhv", delId);
					cmd.ExecuteNonQuery();
				}
			}
			if(n == "hsCVD")
			{
				using (var c = new SqlConnection(cn))
				{
					c.Open();
					var qDeleteHS_CVD = "delete from HS_CVD where MaHS_CVD = @mhs;";
					var cmd = new SqlCommand(qDeleteHS_CVD, c);
					cmd.Parameters.AddWithValue("@mhs", delId);
					cmd.ExecuteNonQuery();
				}
			}
			if(n == "hsCVCQ")
			{
				using (var c = new SqlConnection(cn))
				{
					c.Open();
					var qDeleteHS_CVCQ = "delete from HS_CVCQ where MaHS_CVCQ = @mhs;";
					var cmd = new SqlCommand(qDeleteHS_CVCQ, c);
					cmd.Parameters.AddWithValue("@mhs", delId);
					cmd.ExecuteNonQuery();
				}
			}
			if(n == "hsCVCMNV")
			{
				using (var c = new SqlConnection(cn))
				{
					c.Open();
					var qDeleteHS_CVCMNV = "delete from HS_CVCMNV where MaHS_CVCMNV = @mhs;";
					var cmd = new SqlCommand(qDeleteHS_CVCMNV, c);
					cmd.Parameters.AddWithValue("@mhs", delId);
					cmd.ExecuteNonQuery();
				}
			}
			if(n == "hsHocHam")
			{
				using (var c = new SqlConnection(cn))
				{
					c.Open();
					var qDeleteHS_HocHam = "delete from HS_HocHam where MaHS_HocHam = @mhs;";
					var cmd = new SqlCommand(qDeleteHS_HocHam, c);
					cmd.Parameters.AddWithValue("@mhs", delId);
					cmd.ExecuteNonQuery();
				}
			}
			if(n == "gv")
			{
				using(var c = new SqlConnection(cn))
				{
					c.Open();
					var qPartialDelete = "exec deleteRecordGiaoVien @MGV = @mgv;";
					var cmd = new SqlCommand(qPartialDelete, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					cmd.ExecuteNonQuery();
					//lookup MaThanNhan that's going to be deleted later
					var qLookupMaThanNhan = "select MaThanNhan from GiaoVien_ThanNhan where MaGV = @mgv;";
					cmd = new SqlCommand(qLookupMaThanNhan, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					var reader = cmd.ExecuteReader();
					List<int> mtn = new List<int>();
					List<int> mlh = new List<int>();
					List<int> mhv = new List<int>();

					if (reader.HasRows)
					{
						while (reader.Read())
						{
							mtn.Add(reader.GetInt32(0));
						}
					}
					reader.Close();
					//lookup MaLienHe that's going to be deleted later
					var qLookupMaLienHe = "select MaLienHe from LichSuDCLH where MaGV = @mgv;";
					cmd = new SqlCommand(qLookupMaLienHe, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					reader = cmd.ExecuteReader();
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							mlh.Add(reader.GetInt32(0));
						}
					}
					reader.Close();
					//lookup MaHocVan that's going to be deleted later
					var qLookupMaHocVan = "select MaHocVan from HS_HocVan where MaGV = @mgv;";
					cmd = new SqlCommand(qLookupMaHocVan, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					reader = cmd.ExecuteReader();
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							mhv.Add(reader.GetInt32(0));
						}
					}
					reader.Close();
					//delete foreign key entries
					var qDeleteGV_TN = "delete from GiaoVien_ThanNhan where MaGV = @mgv;";
					cmd = new SqlCommand(qDeleteGV_TN, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					cmd.ExecuteNonQuery();
					//delete primary key entries
					foreach(var m in mtn)
					{
						var qDeleteThanNhan = "delete from ThanNhan where MaThanNhan = " + m + ";";
						cmd = new SqlCommand(qDeleteThanNhan, c);
						cmd.ExecuteNonQuery();
					}
					//delete foreign key entries
					var qDeleteLichSuDCLH = "delete from LichSuDCLH where MaGV = @mgv;";
					cmd = new SqlCommand(qDeleteLichSuDCLH, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					cmd.ExecuteNonQuery();
					//delete primary key entries
					foreach (var m in mlh)
					{
						var qDeleteDCLH = "delete from DiaChiLienHe where MaLienHe = " + m + ";";
						cmd = new SqlCommand(qDeleteDCLH, c);
						cmd.ExecuteNonQuery();
					}
					//delete foreign key entries
					var qDeleteHS_HocVan = "delete from HS_HocVan where MaGV = @mgv;";
					cmd = new SqlCommand(qDeleteHS_HocVan, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					cmd.ExecuteNonQuery();
					//delete primary key entries
					foreach (var m in mhv)
					{
						var qDeleteHocVan = "delete from HocVan where MaHocVan = " + m + ";";
						cmd = new SqlCommand(qDeleteHocVan, c);
						cmd.ExecuteNonQuery();
					}
					var qDeleteGV = "delete from GiaoVien where MaGV = @mgv";
					cmd = new SqlCommand(qDeleteGV, c);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					cmd.ExecuteNonQuery();
				}
				return Json(new { success = true, redirectToIndex = true }, JsonRequestBehavior.AllowGet);
			}

			if(n == "nghi")
			{
				using(var c = new SqlConnection(cn))
				{
					c.Open();
					var qUpdateGVNghiViec = "update GiaoVien set NgayKetThuc = @tdkt where MaGV = @mgv;";
					var cmd = new SqlCommand(qUpdateGVNghiViec, c);
					cmd.Parameters.AddWithValue("@tdkt", today);
					cmd.Parameters.AddWithValue("@mgv", mgv);
					cmd.ExecuteNonQuery();
				}
			}
			return Json(new { success = true, redirectToIndex = false }, JsonRequestBehavior.AllowGet);
		}

        public ActionResult TaiCaNhan(int id)
        {
            var gv = new GiaoVien(id);
            ViewBag.GiaoVien = gv;
            return View();
        }
    }


	public class DeleteIdentifier
	{
		public string mgv { get; set; }
		public string deleteName { get; set; }
		public string deleteId { get; set; }
	}

	public class TempTeacher
	{
		public int MaGV { get; set; }
		public string HoTen { get; set; }
		public string QueQuan { get; set; }
		public string NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string BoMon { get; set; }
		public string DonVi { get; set; }
	}
}
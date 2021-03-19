using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace TeacherManagementSystem.Models
{
    public class GiaoVien
    {
        public int MaGV { get; set; }
        public string HoTen { get; set; }
        public string QueQuan { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string BoMon { get; set; }
        public string DonVi { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string ChucVuChinhQuyen { get; set; }
        public string ChucVuDang { get; set; }
        public string ChucVuCMNV { get; set; }
        public string HocHam { get; set; }
        public string HocVi { get; set; }


        //danh sách thân nhân
        public List<ThanNhan> listThanNhan = new List<ThanNhan>();
        //danh sách học vấn
        public List<HoSoHocVan> listHocVan = new List<HoSoHocVan>();
        //danh sách chức vụ đảng
        public List<ChucVuDang> listChucVuDang = new List<ChucVuDang>();
        //danh sách chức vụ chính quyền
        public List<ChucVuChinhQuyen> listCVCQ = new List<ChucVuChinhQuyen>();
        //danh sách chức vụ chuyên môn nghiệp vụ
        public List<ChucVuCMNV> listCVCMNV = new List<ChucVuCMNV>();
        //danh sách học hàm
        public List<HocHam> listHocHam = new List<HocHam>();
        //danh sách địa chỉ liên hệ
        public List<DiaChiLienHe> lichSu = new List<DiaChiLienHe>();

        public DinhMuc_Tai dm_t;

        public GiaoVien()
        {

        }

        public GiaoVien(int id)
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var thongTinGiaoVienTheoId = @"exec GiaoVienTheoId @MGV = @id";

                var cmd = new SqlCommand(thongTinGiaoVienTheoId, c);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var maGV = reader.GetInt32(0);
                        var tenGV = reader.GetString(1);
                        var queQuan = reader.GetString(2);
                        var gioiTinh = reader.GetString(3);
                        var ngaySinh = reader.GetDateTime(4);
                        var tenKhoa = reader.GetString(5);
                        var tenBoMon = reader.GetString(6);
                        this.MaGV = maGV; this.HoTen = tenGV; this.QueQuan = queQuan; this.GioiTinh = gioiTinh; this.NgaySinh = ngaySinh; this.DonVi = tenKhoa; this.BoMon = tenBoMon;
                    }
                }
                reader.Close();
            }

            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var qTrangThai = "select NgayBatDau, NgayKetThuc from GiaoVien where MaGV = @id;";
                var cmd = new SqlCommand(qTrangThai, c);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var tdbd = reader.GetDateTime(0);
                        this.NgayBatDau = tdbd;
                        if (!reader.IsDBNull(1))
                        {
                            var tdkt = reader.GetDateTime(1);
                            this.NgayKetThuc = tdkt;
                        }
                        else
                        {
                            this.NgayKetThuc = null;
                        }
                    }
                }
            }
            this.populateThanNhan();
            this.populateHoSoHocVan();
            this.populateHocHam();
            this.populateCVCQ();
            this.populateChucVuDang();
            this.populateChucVuCMNV();
            this.populateLichSuDCLH();
            dm_t = new DinhMuc_Tai(MaGV);

        }

        public void populateThanNhan()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            if (listThanNhan.Count > 0)
            {
                listThanNhan.Clear();
            }
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "exec DanhSachThanNhan @MGV = @id";
                var cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", this.MaGV);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var thanNhan = new ThanNhan()
                        {
                            MaThanNhan = reader.GetInt32(1),
                            TenThanNhan = reader.GetString(2),
                            NgaySinh = reader.GetDateTime(3),
                            GioiTinh = reader.GetString(4),
                            NgheNghiep = reader.GetString(5)
                        };
                        listThanNhan.Add(thanNhan);
                    }
                }
                reader.Close();
                listThanNhan.Sort((a, b) => b.NgaySinh.CompareTo(a.NgaySinh));
            }
        }

        public void populateHoSoHocVan()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                if (listHocVan.Count > 0)
                {
                    listHocVan.Clear();
                }
                else
                {
                    var q = @"exec HoSoHocVanGiaoVien @MGV = @id";
                    var cmd = new SqlCommand(q, c);
                    cmd.Parameters.AddWithValue("@id", this.MaGV);
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var hocVan = new HoSoHocVan()
                            {
                                MaHocVan = reader.GetInt32(1),
                                TenHocVi = reader.GetString(2),
                                TenTrinhDo = reader.GetString(3),
                                NuocDaoTao = reader.GetString(4),
                                HeDaoTao = reader.GetString(5),
                                NoiDaoTao = reader.GetString(6),
                                NamCap = reader.GetInt32(7),
                                ThoiDiemNhan = reader.GetDateTime(8),
                                ThoiDiemKetThuc = reader.GetDateTime(9)
                            };
                            listHocVan.Add(hocVan);
                        }
                    }
                }
            }
            listHocVan.Sort((a, b) => b.ThoiDiemNhan.CompareTo(a.ThoiDiemNhan));
            if (listHocVan.Count != 0)
            {
                HocVi = listHocVan[0].TenHocVi;
            }
            else
            {
                HocVi = "Không";
            }


        }

        public void populateChucVuDang()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                if (listChucVuDang.Count > 0)
                {
                    listChucVuDang.Clear();
                }
                var q = @"exec ChucVuDangGiaoVien @MGV = @id";
                var cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", this.MaGV);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cvd = new ChucVuDang();
                        cvd.MaHS_CVD = reader.GetInt32(0);
                        cvd.MaCVD = reader.GetInt32(2);
                        cvd.TenCVD = reader.GetString(3);
                        cvd.ThoiDiemNhan = reader.GetDateTime(4);
                        cvd.ThoiDiemKetThuc = reader.GetDateTime(5);
                        listChucVuDang.Add(cvd);
                    }
                }
                reader.Close();
            }
            listChucVuDang.Sort((a, b) => b.ThoiDiemNhan.CompareTo(a.ThoiDiemNhan));
            if (listChucVuDang.Count != 0)
            {
                ChucVuDang = listChucVuDang[0].TenCVD;
            }
            else
            {
                ChucVuDang = "Không";
            }
        }

        public void populateCVCQ()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                if (listCVCQ.Count > 0)
                {
                    listCVCQ.Clear();
                }
                var q = @"exec ChucVuChinhQuyenGiaoVien @MGV = @id";
                var cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", this.MaGV);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cvcq = new ChucVuChinhQuyen();

                        cvcq.MaHS_CVCQ = reader.GetInt32(0);
                        cvcq.MaCVCQ = reader.GetInt32(2);
                        cvcq.TenCVCQ = reader.GetString(3);
                        cvcq.ThoiDiemNhan = reader.GetDateTime(4);
                        cvcq.ThoiDiemKetThuc = reader.GetDateTime(5);
                        listCVCQ.Add(cvcq);
                    }
                }
                reader.Close();
            }
            listCVCQ.Sort((a, b) => b.ThoiDiemNhan.CompareTo(a.ThoiDiemNhan));
            if (listCVCQ.Count != 0)
            {
                ChucVuChinhQuyen = listCVCQ[0].TenCVCQ;
            }
            else
            {
                ChucVuChinhQuyen = "Không";
            }
        }

        public void populateChucVuCMNV()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                if (listCVCMNV.Count > 0)
                {
                    listCVCMNV.Clear();
                }
                var q = @"exec ChucVuCMNVGiaoVien @MGV = @id";
                var cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", this.MaGV);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cvcmnv = new ChucVuCMNV();
                        cvcmnv.MaHS_CVCMNV = reader.GetInt32(0);
                        cvcmnv.MaCVCMNV = reader.GetInt32(2);
                        cvcmnv.TenCVCMNV = reader.GetString(3);
                        cvcmnv.ThoiDiemNhan = reader.GetDateTime(4);
                        cvcmnv.ThoiDiemKetThuc = reader.GetDateTime(5);
                        listCVCMNV.Add(cvcmnv);
                    }
                }
                reader.Close();
            }
            listCVCMNV.Sort((a, b) => b.ThoiDiemNhan.CompareTo(a.ThoiDiemNhan));
            if (listCVCMNV.Count != 0)
            {
                ChucVuCMNV = listCVCMNV[0].TenCVCMNV;
            }
            else
            {
                ChucVuCMNV = "Không";
            }

        }
        public void populateHocHam()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                if (listHocHam.Count > 0)
                {
                    listHocHam.Clear();
                }
                var q = @"exec HocHamGiaoVien @MGV = @id";
                var cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", this.MaGV);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var hocHam = new HocHam();
                        hocHam.MaHS_HocHam = reader.GetInt32(0);
                        hocHam.MaHocHam = reader.GetInt32(2);
                        hocHam.TenHocHam = reader.GetString(3);
                        hocHam.ThoiDiemNhan = reader.GetDateTime(4);
                        hocHam.ThoiDiemKetThuc = reader.GetDateTime(5);
                        listHocHam.Add(hocHam);
                    }
                }
                reader.Close();
            }
            listHocHam.Sort((a, b) => b.ThoiDiemNhan.CompareTo(a.ThoiDiemNhan));
            if (listHocHam.Count != 0)
            {
                HocHam = listHocHam[0].TenHocHam;
            }
            else
            {
                HocHam = "Không";
            }
        }

        public void populateLichSuDCLH()
        {
            var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
            using (var c = new SqlConnection(cn))
            {
                c.Open();
                var q = "exec DanhSachDiaChiGiaoVienTheoId @MGV = @id";
                SqlCommand cmd = new SqlCommand(q, c);
                cmd.Parameters.AddWithValue("@id", this.MaGV);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var dchi = new DiaChiLienHe()
                        {
                            MaGV = this.MaGV,
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
            lichSu.Sort((a, b) => b.ThoiGian.CompareTo(a.ThoiGian));
        }

    }
}
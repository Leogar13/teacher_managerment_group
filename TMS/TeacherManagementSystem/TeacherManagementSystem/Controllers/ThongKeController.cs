using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace TeacherManagementSystem.Views.ThongKe
{
    public class ThongKeController : Controller
    {
        DateTime today = DateTime.Now;
        public string cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;

        // GET: ThongKe/NhanLuc
        public ActionResult NhanLuc()
        {
            ViewBag.Today = today;
            return View();
        }

        //POST: ThongKe/NhanLuc
        [HttpPost]
        public ActionResult NhanLuc(date day)
        {
            var d = day.d;
            var result = "";
            var dataTable = new DataTable();
            using (var c = new SqlConnection(cn))
            {
                var q = "exec BangThongKe @Ngay = '" + d + "';";
                var da = new SqlDataAdapter(q, c);
                da.Fill(dataTable);
            }
            result += "<pre>" + "Tong so" + "    " + "GS" + "   " + "PGS" + "   " + "TSKH" + "    " + "Thacsy" + "   " + "DH" + "   " + "Khac" + "</pre><br>";

            int countGV = 0, countGS = 0, countPGS = 0, countTSKH = 0, countTienSi = 0, countThacSy = 0, countDH = 0, countKhac = 0;
            var tenKhoa = dataTable.Rows[0][0].ToString();
            List<ThongKeKhoa> list = new List<ThongKeKhoa>();
            var khoa = new ThongKeKhoa();


            foreach (DataRow row in dataTable.Rows)
            {
                string TenKhoa = row["TenKhoa"].ToString();
                string tenHocHam, tenHocVi;
                int MGV;

                if (row.IsNull("MaGV"))
                {
                    MGV = 0;
                }
                else
                {
                    MGV = Convert.ToInt32(row["MaGV"]);
                }
                if (row.IsNull("TenHocHam"))
                {
                    tenHocHam = "0";
                }
                else
                {
                    tenHocHam = row["TenHocHam"].ToString();
                }

                if (row.IsNull("TenHocVi"))
                {
                    tenHocVi = "0";
                }
                else
                {
                    tenHocVi = row["TenHocVi"].ToString();
                }

                if (TenKhoa != tenKhoa)
                {
                    khoa = new ThongKeKhoa()
                    {
                        TongSo = countGV,
                        TenKhoa = tenKhoa,
                        SoDH = countDH,
                        SoGiaoSu = countGS,
                        SoPGS = countPGS,
                        SoThacSy = countThacSy,
                        SoTienSi = countTienSi,
                        SoTSKH = countTSKH,
                        SoKhac = countKhac
                    };
                    list.Add(khoa);
                    countGV = 0; countGS = 0; countPGS = 0; countTSKH = 0; countTienSi = 0; countThacSy = 0; countDH = 0; countKhac = 0;
                    tenKhoa = TenKhoa;
                    if (MGV != 0)
                    {
                        countGV++;
                        if (tenHocHam != "0")
                        {
                            if (tenHocHam == "Giáo sư")
                            {
                                countGS++;
                            }
                            if (tenHocHam == "Phó giáo sư")
                            {
                                countPGS++;
                            }
                        }
                        if (tenHocVi != "0")
                        {
                            if (tenHocVi == "Cử nhân" || tenHocVi == "Kỹ sư")
                            {
                                countDH++;
                            }
                            if (tenHocVi == "Thạc sỹ")
                            {
                                countThacSy++;
                            }
                            if (tenHocVi == "Tiến sĩ")
                            {
                                countTienSi++;
                            }
                            if (tenHocVi == "TSKH")
                            {
                                countTSKH++;
                            }
                        }
                        else
                        {
                            countKhac++;
                        }
                    }
                }
                else
                {
                    if (MGV != 0)
                    {
                        countGV++;
                        if (tenHocHam != "0")
                        {
                            if (tenHocHam == "Giáo sư")
                            {
                                countGS++;
                            }
                            if (tenHocHam == "Phó giáo sư")
                            {
                                countPGS++;
                            }
                        }
                        if (tenHocVi != "0")
                        {
                            if (tenHocVi == "Cử nhân" || tenHocVi == "Kỹ sư")
                            {
                                countDH++;
                            }
                            if (tenHocVi == "Thạc sỹ")
                            {
                                countThacSy++;
                            }
                            if (tenHocVi == "Tiến sĩ")
                            {
                                countTienSi++;
                            }
                            if (tenHocVi == "TSKH")
                            {
                                countTSKH++;
                            }
                        }
                        else
                        {
                            countKhac++;
                        }
                    }

                }

            }

            khoa = new ThongKeKhoa()
            {
                TongSo = countGV,
                TenKhoa = tenKhoa,
                SoDH = countDH,
                SoGiaoSu = countGS,
                SoPGS = countPGS,
                SoThacSy = countThacSy,
                SoTienSi = countTienSi,
                SoTSKH = countTSKH,
                SoKhac = countKhac
            };
            list.Add(khoa);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TonghopTai()
        {
            return View();
        }
    }
    public class ThongKeKhoa{
        public string TenKhoa { get; set; }
        public int TongSo { get; set; }
        public int SoGiaoSu { get; set; }
        public int SoPGS { get; set; }
        public int SoTSKH { get; set; }
        public int SoTienSi { get; set; }
        public int SoThacSy { get; set; }
        public int SoDH { get; set; }
        public int SoKhac { get; set; }

    }

    public class date
    {
        public string d { get; set; }
    }
}
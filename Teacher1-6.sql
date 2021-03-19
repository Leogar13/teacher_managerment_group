/* Query for tables */
use Teacher
/* DonVi */
insert into DonVi(TenKhoa) values (''); /* Change params in app */
delete from DonVi where TenKhoa = '' ; /* Change params in app */
update DonVi set TenKhoa = '' where MaKhoa = ''; /* Change params in app */

/* Bo mon */
insert into BoMon(MaKhoa, TenBoMon) values ( 1, ''); /* Change params in app */

select * from GiaoVien

/* Giao Vien */
/* Mau ly lich khoa hoc */

/* Tu 1-6 */

select gv.MaGV, TenGiaoVien, QueQuan, GioiTinh, NgaySinh, TenKhoa, Phuong, Quan, ThanhPho, DT_DiDong, DT_NhaRieng, Email, ThoiGian from
	(
		select MaGV, MAX(ThoiGian) as maxTg from
		(
			select gv_Khoa.MaGV, TenGiaoVien, QueQuan, GioiTinh, NgaySinh, TenKhoa, Phuong, Quan, ThanhPho, DT_NhaRieng, DT_DiDong, Email, ThoiGian from
			(
				select MaGV,TenGiaoVien, QueQuan, GioiTinh, NgaySinh, TenKhoa from GiaoVien
					inner join 
						(
							select MaBoMon ,BoMon.MaKhoa, DonVi.TenKhoa from BoMon
							left join DonVi on BoMon.MaKhoa = DonVi.MaKhoa
						) as BM_Khoa
					on GiaoVien.MaBoMon = BM_Khoa.MaBoMon
			) as gv_Khoa
			inner join
				(
					select LichSuDCLH.MaGV,Phuong, Quan, ThanhPho, DT_NhaRieng, DT_DiDong, Email, ThoiGian from DiaChiLienHe
					inner join LichSuDCLH 
					on DiaChiLienHe.MaLienHe = LichSuDCLH.MaLienHe
				) as gv_dc
			on gv_Khoa.MaGV = gv_dc.MaGV
			) as gv
		group by MaGV
	) as gv_maxtg
inner join 
	(
		select gv_Khoa.MaGV, TenGiaoVien, QueQuan, GioiTinh, NgaySinh, TenKhoa, Phuong, Quan, ThanhPho, DT_NhaRieng, DT_DiDong, Email, ThoiGian from
			(
				select MaGV,TenGiaoVien, QueQuan, GioiTinh, NgaySinh, TenKhoa from GiaoVien
					inner join 
						(
							select MaBoMon ,BoMon.MaKhoa, DonVi.TenKhoa from BoMon
							left join DonVi on BoMon.MaKhoa = DonVi.MaKhoa
						) as BM_Khoa
					on GiaoVien.MaBoMon = BM_Khoa.MaBoMon
			) as gv_Khoa
			inner join
				(
					select LichSuDCLH.MaGV,Phuong, Quan, ThanhPho, DT_NhaRieng, DT_DiDong, Email, ThoiGian from DiaChiLienHe
					inner join LichSuDCLH 
					on DiaChiLienHe.MaLienHe = LichSuDCLH.MaLienHe
				) as gv_dc
			on gv_Khoa.MaGV = gv_dc.MaGV
	) as gv
on gv_maxtg.MaGV = gv.MaGV and gv_maxtg.maxTg = gv.ThoiGian

/* 7 */
select gv_HocVan.MaGV, MaHocVan, ThoiDiemNhan, MaHocVi, TenHocVi from
	(		
		select MaGV, MAX(ThoiDiemNhan) as maxTDN from
		(
			select MaGV, MaHocVan, ThoiDiemNhan, gv_HocVan.MaHocVi, HocVi.TenHocVi from
				(
					select MaGV, HS_HocVan.MaHocVan, ThoiDiemNhan, HocVan.MaHocVi from HS_HocVan
					inner join HocVan
					on HS_HocVan.MaHocVan = HocVan.MaHocVan
				) as gv_HocVan
			inner join HocVi on gv_HocVan.MaHocVi = HocVi.MaHocVi
		) as foo
		group by MaGV
	) as gv_maxTGHocVan
	inner join
	(
		select MaGV, MaHocVan, ThoiDiemNhan, gv_HocVan.MaHocVi, HocVi.TenHocVi from
				(
					select MaGV, HS_HocVan.MaHocVan, ThoiDiemNhan, HocVan.MaHocVi from HS_HocVan
					inner join HocVan
					on HS_HocVan.MaHocVan = HocVan.MaHocVan
				) as gv_HocVan
			inner join HocVi on gv_HocVan.MaHocVi = HocVi.MaHocVi
	) as gv_HocVan
	on gv_maxTGHocVan.MaGV = gv_HocVan.MaGV and gv_maxTGHocVan.maxTDN = gv_HocVan.ThoiDiemNhan


/* 8 */
	select MaGV, HS_CVCMNV.MaCVCMNV, ThoiDiemNhan, ThoiDiemKetThuc, TenCVCMNV from HS_CVCMNV
	inner join CV_ChuyenMonNghiepVu
	on HS_CVCMNV.MaCVCMNV = CV_ChuyenMonNghiepVu.MaCVCMNV

	select * from HS_HocHam

/* 9 */
select MaGV, GV_Lop.MaLop, MaHocPhan, TenLop, MaBoMon, TenBoMon from
	(
		select MaGV, phanCongGiaoVien.MaLop from
			(
				select MaGV, PhanCong_GiangDay.MaCVGD, MaLop, SoTiet from PhanCong_GiangDay
				inner join GiangDay_Lop
				on PhanCong_GiangDay.MaCVGD = GiangDay_Lop.MaCVGD
			) as phanCongGiaoVien
			inner join Lop on phanCongGiaoVien.MaLop = Lop.MaLop
	) as GV_Lop
	inner join
	(	
		select MaLop, MaHocPhan, TenLop, BoMon.MaBoMon, TenBoMon from 
			(
				select MaLop,Lop.MaHocPhan, TenLop,MaBoMon from Lop
				inner join HocPhan on Lop.MaHocPhan = HocPhan.MaHocPhan
			) as Lop_HocPhan
			inner join BoMon on Lop_HocPhan.MaBoMon = BoMon.MaBoMon
	) Lop_HocPhan_BoMon
	on GV_Lop.MaLop = Lop_HocPhan_BoMon.MaLop
	where MaGV = 1 


/* 14-15 */

select MaGV, TenTrinhDo, NoiDaoTao, HeDaoTao, NuocDaoTao, NamCap, TenLuanAn from HS_HocVan
inner join HocVan
on HS_HocVan.MaHocVan = HocVan.MaHocVan

/* 18 - same as 9 */






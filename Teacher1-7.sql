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

select gv_HocVi.MaGV, MAX(gv_HocVi.ThoiDiemNhan) from
	(
		select MaGV, MaHocVan, ThoiDiemNhan, gv_HocVan.MaHocVi, HocVi.TenHocVi from
			(
				select MaGV, HS_HocVan.MaHocVan, ThoiDiemNhan, HocVan.MaHocVi from HS_HocVan
				inner join HocVan
				on HS_HocVan.MaHocVan = HocVan.MaHocVan
			) as gv_HocVan
		inner join HocVi on gv_HocVan.MaHocVi = HocVi.MaHocVi
		group by gv_HocVan.ThoiDiemNhan
	) as gv_maxTgHocVi
	inner join
	(
		select MaGV, MaHocVan, ThoiDiemNhan, gv_HocVan.MaHocVi, HocVi.TenHocVi from
			(
				select MaGV, HS_HocVan.MaHocVan, ThoiDiemNhan, HocVan.MaHocVi from HS_HocVan
				inner join HocVan
				on HS_HocVan.MaHocVan = HocVan.MaHocVan
			) as gv_HocVan
		inner join HocVi on gv_HocVan.MaHocVi = HocVi.MaHocVi
	) as gv_HocVi
	on gv_HocVi.MaGV = gv_maxTgHocVi.MaGV and gv_HocVi.ThoiDiemNhan = gv_maxTgHocVi.ThoiDiemNhan










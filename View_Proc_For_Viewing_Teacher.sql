use Teacher;
go
create view DanhSachGiaoVien
 as
	select MaGV, TenGiaoVien, QueQuan, GioiTinh, NgaySinh, TenKhoa, TenBoMon from 
		(
			select MaGV, GiaoVien.MaBoMon, TenGiaoVien, QueQuan, GioiTinh,NgaySinh, MaKhoa, TenBoMon from GiaoVien
				inner join BoMon
				on GiaoVien.MaBoMon = BoMon.MaBoMon
		) as GV_BM
		inner join DonVi
		on DonVi.MaKhoa = GV_BM.MaKhoa
go

create procedure GiaoVienTheoId @MGV INT
as 
	 select MaGV, TenGiaoVien, QueQuan, GioiTinh, NgaySinh, TenKhoa, TenBoMon from 
		(
			select MaGV, GiaoVien.MaBoMon, TenGiaoVien, QueQuan, GioiTinh,NgaySinh, MaKhoa, TenBoMon from GiaoVien
				inner join BoMon
				on GiaoVien.MaBoMon = BoMon.MaBoMon
		) as GV_BM
		inner join DonVi
		on DonVi.MaKhoa = GV_BM.MaKhoa
		where MaGV = @MGV
go

create procedure DanhSachDiaChiGiaoVienTheoId @MGV int
as
	select MaGV, DiaChiLienHe.MaLienHe, ThoiGian, Phuong, Quan, ThanhPho, DT_DiDong, DT_NhaRieng, Email from LichSuDCLH
		inner join DiaChiLienHe
		on DiaChiLienHe.MaLienHe = LichSuDCLH.MaLienHe
	where MaGV = @MGV
go

create procedure DanhSachThanNhan @MGV int
as
	select MaGV, ThanNhan.MaThanNhan, TenThanNhan, NgaySinh, GioiTinh, NgheNghiep from GiaoVien_ThanNhan
		inner join ThanNhan
		on GiaoVien_ThanNhan.MaThanNhan = ThanNhan.MaThanNhan
	where MaGV = @MGV
go

create procedure HoSoHocVanGiaoVien @MGV int 
as
	select MaGV, MaHocVan, TenHocVi, TenTrinhDo, NuocDaoTao, HeDaoTao, NoiDaoTao, NamCap, ThoiDiemNhan, ThoiDiemKetThuc  from 
		(
		select MaGV,HS_HocVan.MaHocVan, ThoiDiemNhan,ThoiDiemKetThuc, MaHocVi,TenTrinhDo,NuocDaoTao,HeDaoTao, NoiDaoTao, NamCap from HS_HocVan
			inner join HocVan
			on HocVan.MaHocVan = HS_HocVan.MaHocVan
		) as GV_HSHV
		inner join HocVi on HocVi.MaHocVi = GV_HSHV.MaHocVi
	where MaGV = @MGV
go

create procedure ChucVuDangGiaoVien @MGV int
as
select MaHS_CVD, MaGV, ChucVuDang.MaCVD, TenCVD, ThoiDiemNhan, ThoiDiemKetThuc from ChucVuDang
	inner join HS_CVD
	on ChucVuDang.MaCVD = HS_CVD.MaCVD
	where MaGV = @MGV
go

create procedure ChucVuChinhQuyenGiaoVien @MGV int
as 
	select MaHS_CVCQ, MaGV, ChucVuChinhQuyen.MaCVCQ, TenCVCQ, ThoiDiemNhan, ThoiDiemKetThuc from ChucVuChinhQuyen
		inner join HS_CVCQ
		on ChucVuChinhQuyen.MaCVCQ = HS_CVCQ.MaCVCQ
		where MaGV = @MGV
go

create procedure ChucVuCMNVGiaoVien @MGV int
as
	select MaHS_CVCMNV, MaGV, CV_ChuyenMonNghiepVu.MaCVCMNV, TenCVCMNV, ThoiDiemNhan, ThoiDiemKetThuc from CV_ChuyenMonNghiepVu
		inner join HS_CVCMNV
		on CV_ChuyenMonNghiepVu.MaCVCMNV = HS_CVCMNV.MaCVCMNV
	where MaGV = @MGV
go

create procedure HocHamGiaoVien @MGV int
as
	select MaHS_HocHam, MaGV, HS_HocHam.MaHocHam, TenHocHam, ThoiDiemNhan, ThoiDiemKetThuc from HS_HocHam
		inner join HocHam 
		on HocHam.MaHocHam = HS_HocHam.MaHocHam
	where MaGV = @MGV
go

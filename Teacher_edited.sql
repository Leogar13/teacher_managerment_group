
create database Teacher;
go

use Teacher;
go 

set nocount on;
go

create table DonVi(
	MaKhoa int primary key identity(1,1),
	TenKhoa nvarchar(max) not null
)

create table BoMon(
	MaBoMon int primary key identity(1,1),
	MaKhoa int foreign key references DonVi(MaKhoa),
	TenBoMon nvarchar(MAX) not null
)

create table GiaoVien(
	MaGV int primary key identity(1,1),
	MaBoMon int foreign key references BoMon(MaBoMon),
	TenGiaoVien nvarchar(max) not null,
	QueQuan nvarchar(max) not null,
	GioiTinh nvarchar(10)  not null,
	NgaySinh datetime not null,
	NgayBatDau datetime,
	NgayKetThuc datetime
)

create table ThanNhan(
	MaThanNhan int primary key identity(1,1),
	TenThanNhan nvarchar(max) not null,
	NgaySinh datetime not null,
	GioiTinh nvarchar(10)  not null,
	NgheNghiep nvarchar(max)
)

create table GiaoVien_ThanNhan(
	MaGV int foreign key references GiaoVien(MaGV),
	MaThanNhan int foreign key references ThanNhan(MaThanNhan),
	primary key(MaGV, MaThanNhan)
)

create table DiaChiLienHe(
	MaLienHe int primary key identity(1,1),
	Phuong nvarchar(20) not null,
	Quan nvarchar(20) not null,
	ThanhPho nvarchar(20) not null,
	DT_NhaRieng nvarchar(max),
	DT_DiDong nvarchar(max),
	Email nvarchar(max)
)

create table LichSuDCLH(
	MaGV int foreign key references GiaoVien(MaGV),
	MaLienHe int foreign key references DiaChiLienHe(MaLienHe),
	ThoiGian datetime not null,
	primary key(MaGV, MaLienHe)
)

create table MienGiam(
	MaMienGiam int primary key identity(1,1),
	TyLe float not null
)

create table ChucVuDang(
	MaCVD int primary key identity(1,1),
	MaMienGiam int foreign key references MienGiam(MaMienGiam),
	TenCVD nvarchar(max)
)

create table ChucVuChinhQuyen(
	MaCVCQ int primary key identity(1,1),
	MaMienGiam int foreign key references MienGiam(MaMienGiam),
	TenCVCQ nvarchar(max) not null
)

create table HocVi(
	MaHocVi int primary key identity(1,1),
	TenHocVi nvarchar(max) not null
)

create table HocVan(
	MaHocVan int primary key identity(1,1),
	MaHocVi int foreign key references HocVi(MaHocVi),
	TenTrinhDo nvarchar(max),
	NuocDaoTao nvarchar(max),
	HeDaoTao nvarchar(max),
	NoiDaoTao nvarchar(max),
	NamCap int,
	TenLuanAn nvarchar(max)
)

create table DinhMucDaoTao(
	MaDMDT int primary key identity(1,1),
	TenDinhMuc nvarchar(max),
	SoGioDMDT int,
	SoGioDMDT_TCQP int
)

create table DinhMucNghienCuu(
	MaDMNC int primary key identity(1,1),
	TenDinhMuc nvarchar(max),
	SoGioDMNC int,
	GioChuanDMNC int
)

create table HocHam(
	MaHocHam int primary key identity(1,1),
	MaDMDT int foreign key references DinhMucDaoTao(MaDMDT),
	MaDMNC int foreign key references DinhMucNghienCuu(MaDMNC),
	TenHocHam nvarchar(max)
)

create table CV_ChuyenMonNghiepVu(
	MaCVCMNV int primary key identity(1,1),
	MaDMDT int foreign key references DinhMucDaoTao(MaDMDT),
	MaDMNC int foreign key references DinhMucNghienCuu(MaDMNC),
	TenCVCMNV nvarchar(max)
)

create table HS_CVD(
	MaHS_CVD int primary key identity(1,1),
	MaGV int foreign key references GiaoVien(MaGV),
	MaCVD int foreign key references ChucVuDang(MaCVD),
	ThoiDiemNhan datetime,
	ThoiDiemKetThuc datetime
)

create table HS_CVCQ(
	MaHS_CVCQ int primary key identity(1,1),
	MaGV int foreign key references GiaoVien(MaGV),
	MaCVCQ int foreign key references ChucVuChinhQuyen(MaCVCQ),
	ThoiDiemNhan datetime,
	ThoiDiemKetThuc datetime
)

create table HS_CVCMNV(
	MaHS_CVCMNV int primary key identity(1,1), 
	MaGV int foreign key references GiaoVien(MaGV),
	MaCVCMNV int foreign key references CV_ChuyenMonNghiepVu(MaCVCMNV),
	ThoiDiemNhan datetime,
	ThoiDiemKetThuc datetime
)

create table HS_HocVan(
	MaGV int foreign key references GiaoVien(MaGV),
	MaHocVan int foreign key references HocVan(MaHocVan),
	ThoiDiemNhan datetime,
	ThoiDiemKetThuc datetime,
	primary key(MaGV, MaHocVan)
)

create table HS_HocHam(
	MaHS_HocHam int primary key identity(1,1),
	MaGV int foreign key references GiaoVien(MaGV),
	MaHocHam int foreign key references HocHam(MaHocHam),
	ThoiDiemNhan datetime,
	ThoiDiemKetThuc datetime,
)

create table HocPhan(
	MaHocPhan int primary key identity(1,1),
	TenHocPhan nvarchar(MAX) not null,
	MaBoMon int foreign key references BoMon(MaBoMon),
	SoTiet int not null,
	SoTinChi int not null
)

create table Lop(
	MaLop int primary key identity(1,1),
	MaHocPhan int foreign key references HocPhan(MaHocPhan),
	TenLop nvarchar(max) not null,
	He nvarchar(max) not null,
	SiSo int
)
create table GioChuan_HoiDong(
	MaGCHD int primary key identity(1,1),
	TenGioChuan nvarchar(max) not null,
	SoGio float not null,
	DonVi nvarchar(10)  not null,
	So int not null
)

create table GioChuan_GiangDay(
	MaGCGD int primary key identity(1,1),
	TenGioChuan nvarchar(max) not null,
	SoGio float not null,
	DonVi nvarchar(10)  not null,
	So int not null
)
create table GiangDay(
	MaCVGD int primary key identity(1,1),
	MaGCGD int foreign key references GioChuan_GiangDay(MaGCGD),
	MaLop int foreign key references Lop(MaLop),
	TenCongViec nvarchar(max) not null
)

create table PhanCong_GiangDay(
	MaGV int foreign key references GiaoVien(MaGV),
	MaCVGD int foreign key references GiangDay(MaCVGD),
	SoTietDay int not null,
	primary key (MaGV, MaCVGD)
)

create table HoiDong(
	MaCVHD int primary key identity(1,1),
	MaGCHD int foreign key references GioChuan_HoiDong(MaGCHD),
	TenCongViec nvarchar(max) not null,
	TenHoiDong nvarchar(max) not null
)

create table VaiTroHoiDong(
	MaVTHD int primary key identity(1,1),
	TenVaiTro nvarchar(max) not null
)

create table PhanCong_HoiDong(
	MaGV int foreign key references GiaoVien(MaGV),
	MaCVHD int foreign key references HoiDong(MaCVHD),
	MaVTHD int foreign key references VaiTroHoiDong(MaVTHD),
	SoLan int default (0),
	primary key(MaGV, MaCVHD, MaVTHD)
)

create table GioChuan_HuongDan(
	MaGCHDan int primary key identity(1,1),
	TenGioChuan nvarchar(max) not null,
	SoGio float not null,
	DonVi nvarchar(10)  not null,
	So int not null
)

create table GioChuan_KhaoThi(
	MaGCKT int primary key identity(1,1),
	TenGioChuan nvarchar(max) not null,
	SoGio float not null,
	DonVi nvarchar(10)  not null,
	So int not null
)

create table HocVien(
	MaHocVien int primary key identity(1,1),
	TenHocVien nvarchar(max)
)

create table HuongDan(
	MaCVHDan int primary key identity(1,1),
	MaGCHDan int  foreign key references GioChuan_HuongDan(MaGCHDan),
	TenCongViec nvarchar(max) not null
)

create table PhanCong_HuongDan(
	MaGV int  foreign key references GiaoVien(MaGV),
	MaCVHDan int  foreign key references HuongDan(MaCVHDan),
	primary key(MaGV, MaCVHDan)
)

create table HuongDan_HocVien(
	MaCVHDan int  foreign key references HuongDan(MaCVHDan),
	MaHocVien int  foreign key references HocVien(MaHocVien),
	TenDeTai nvarchar(max) not null,
	primary key(MaCVHDan, MaHocVien)
)

create table HocVien_Lop(
	MaHocVien int  foreign key references HocVien(MaHocVien),
	MaLop int  foreign key references Lop(MaLop),
	primary key(MaHocVien, MaLop)
)

create table KhaoThi(
	MaCVKT int primary key identity(1,1),
	MaGCKT int  foreign key references GioChuan_KhaoThi(MaGCKT),
	TenCongViec nvarchar(max) not null,
	MaLop int foreign key references Lop(MaLop)
)

create table PhanCong_KhaoThi(
	MaGV int  foreign key references GiaoVien(MaGV),
	MaCVKT int  foreign key references KhaoThi(MaCVKT),
	SoBai int not null,
	MaHocPhan int foreign key references HocPhan(MaHocPhan),
	primary key(MaGV, MaCVKT, MaHocPhan)
)

create table DonViTinh(
	MaDVT int primary key identity(1,1),
	DonVi nvarchar(max) not null,
	So int not null
)

create table LoaiNghienCuu(
	MaLoaiNC int primary key identity(1,1),
	TenLoaiNC nvarchar(max)
)

create table VaiTro(
	MaVT int primary key identity(1,1),
	MaLoaiNC int  foreign key references LoaiNghienCuu(MaLoaiNC),
	TenVaiTro nvarchar(max) not null
)

create table ThuocTinh(
	MaThuocTinh int primary key identity(1,1),
	TenThuocTinh nvarchar(max) not null
)

create table LoaiNC_ThuocTinh(
	MaLoaiNC int  foreign key references LoaiNghienCuu(MaLoaiNC),
	MaThuocTinh int  foreign key references ThuocTinh(MaThuocTinh),
	NoiDung nvarchar(max) not null,
	primary key(MaLoaiNC, MaThuocTinh)
)

create table LoaiCongTrinh(
	MaLoaiCT int primary key identity(1,1),
	TenLoaiCT nvarchar(max) not null,
	GioChuan int not null
)

create table CongTrinhKhoaHoc(
	MaCTKH int primary key identity(1,1),
	MaLoaiCT int  foreign key references LoaiCongTrinh(MaLoaiCT),
	MaLoaiNC int  foreign key references LoaiNghienCuu(MaLoaiNC),
	MaDVT int  foreign key references DonViTinh(MaDVT),
	TenCTNC nvarchar(max) not null,
	SoTacGia int
)

create table PhanCong_NghienCuu(
	MaGV int  foreign key references GiaoVien(MaGV),
	MaCTKH int  foreign key references CongTrinhKhoaHoc(MaCTKH),
	MaVT int foreign key references VaiTro(MaVT),
	primary key(MaGV, MaCTKH)

)

create table LoaiNC_LoaiCT(
	MaLoaiNC int  foreign key references LoaiNghienCuu(MaLoaiNC),
	MaLoaiCT int foreign key references LoaiCongTrinh(MaLoaiCT),
	primary key(MaLoaiNC, MaLoaiCT)
)
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

create procedure deleteRecordGiaoVien @MGV int
as 
	delete from HS_CVCMNV where MaGV = @MGV;
	delete from HS_CVCQ where MaGV = @MGV;
	delete from HS_CVD where MaGV = @MGV;
	delete from HS_HocHam where MaGV = @MGV;
	delete from PhanCong_GiangDay where MaGV = @MGV;
	delete from PhanCong_HoiDong where MaGV = @MGV;
	delete from PhanCong_HuongDan where MaGV = @MGV;
	delete from PhanCong_KhaoThi where MaGV = @MGV;
	delete from PhanCong_NghienCuu where MaGV = @MGV;

go
create proc BangThongKe @Ngay datetime
as
select DonVi.TenKhoa, MaGV, TenHocHam, TenHocVi from DonVi
left join 
(
	select MaGV, TenHocHam, TenHocVi, TenKhoa from
		(
			select MaGV, MaBoMon, TenHocHam, TenHocVi, NgayBatDau, NgayKetThuc from
				(
					select MaGV, MaBoMon, TenHocHam, MaHocVi, NgayBatDau, NgayKetThuc from
					(
						select a.MaGV, MaBoMon,MaHocHam,MaHocVi, NgayBatDau, NgayKetThuc from
							(
								select GiaoVien.MaGV, MaBoMon, maHocHam as MaHocHam, NgayBatDau, NgayKetThuc from GiaoVien
								left join
									(
										select MaGV, max(MaHocHam) as maHocHam from HS_HocHam
										group by MaGV 
									) as a
								on GiaoVien.MaGV = a.MaGV
							) as a
							left join
							(
								select b.MaGV, mhv as MaHocVi from
									(
											select MaGV, max(MaHocVi) as mhv from
										(
											select MaGV, MaBoMon, NgayBatDau, NgayKetThuc, MaHocVi from
												(
													select GiaoVien.MaGV, MaBoMon, MaHocVan, NgayBatDau, NgayKetThuc from GiaoVien
													left join HS_HocVan
													on GiaoVien.MaGV = HS_HocVan.MaGV
												) as a
												left join HocVan
												on a.MaHocVan = HocVan.MaHocVan
										) as a
										group by MaGV
									) as a
								inner join
									(
										select MaGV, MaBoMon, NgayBatDau, NgayKetThuc, MaHocVi from
											(
												select GiaoVien.MaGV, MaBoMon, MaHocVan, NgayBatDau, NgayKetThuc from GiaoVien
												left join HS_HocVan
												on GiaoVien.MaGV = HS_HocVan.MaGV
											) as a
											left join HocVan
											on a.MaHocVan = HocVan.MaHocVan
									) as b
								on mhv = b.MaHocVi and a.MaGV = b.MaGV
							) as b
							on a.MaGV = b.MaGV
					) as a
					left join HocHam
					on a.MaHocHam = HocHam.MaHocHam
				) as a
				left join HocVi
				on a.MaHocVi = HocVi.MaHocVi
		) as a
		left join
		(
			select MaBoMon, TenKhoa from BoMon
			inner join DonVi
			on BoMon.MaKhoa = DonVi.MaKhoa
		) as b
		on a.MaBoMon = b.MaBoMon
		where (NgayKetThuc is null or NgayKetThuc > @Ngay) and (NgayBatDau < @Ngay)
) as a
on DonVi.TenKhoa = a.TenKhoa
order by TenKhoa

go

create view GV_DinhMuc
as
--tính tải yêu cầu cho dt và nc của từng gv, nếu không có là 0
select MaGV, MaBoMon, TenGiaoVien, dmdt, dmnc, floor(dmdt - TongMienGiam/100 * dmdt) as taiYeuCau_dt, floor(dmnc - TongMienGiam/100 * dmnc) as taiYeuCau_nc from
	(
	-- lấy giáo viên, định mức nghiên cứu (gốc), định mức đào tạo (gốc) và Tổng miễn giảm của gv đó, nếu null để mặc định là 0
		select a.MaGV, MaBoMon, TenGiaoVien, dmdt, dmnc, ISNULL(TongMienGiam, 0) as TongMienGiam from
			(
			--lấy danh sách tất cả giáo viên cùng với dmdt và dmnc của giáo viên đó, nếu không có mặc định là 0
				select GiaoVien.MaGV, MaBoMon, TenGiaoVien, ISNULL(dmdt, 0) as dmdt, ISNULL(dmnc, 0) as dmnc from GiaoVien
				left join
					(
					--đổi mã dmdt ra giờ chuẩn
						select MaGV, SoGioDMDT as dmdt ,GioChuanDMNC as dmnc from DinhMucDaoTao
						inner join
							(
							-- đổi mã dmnc ra giờ chuẩn
								select MaGV, GioChuanDMNC, dmdt from DinhMucNghienCuu
								inner join
									(
									-- so sánh mã dmdt của cmnv và dmdt của học hàm, lấy cái lớn hơn, tương tự với dmnc của học hàm và cmnv
										select MaGV, FLOOR(0.5 * ((dmdt_cmnv + dmdt_hh) + ABS(dmdt_cmnv - dmdt_hh))) as dmdt, FLOOR(0.5 * ((dmnc_cmnv + dmnc_hh) + ABS(dmnc_cmnv - dmnc_hh))) as dmnc from 
											(
											-- lấy mã dmdt, dmnc ứng với cả 2 chức vụ cmnv và học hàm, nếu không có mặc định là 0
												select ISNULL(dm_cmnv.MaGV, dm_hh.MaGV) as MaGV, ISNULL(dmdt_cmnv, 0) as dmdt_cmnv, ISNULL(dmnc_cmnv, 0) as dmnc_cmnv, ISNULL(dmdt_hh, 0) as dmdt_hh, ISNULL(dmnc_hh, 0) as dmnc_hh from 
													(
													--lấy mã định mức đào tạo và định mức nghiên cứu ứng với cv_cmnv
														select MaGV, MaDMDT as dmdt_cmnv, MaDMNC as dmnc_cmnv from CV_ChuyenMonNghiepVu
														inner join
															(
															-- Chọn mã gv và mã cvcmnv gần nhất ứng với mỗi gv
																select HS_CVCMNV.MaGV, MaCVCMNV from HS_CVCMNV
																	inner join
																	( 
																		select MaGV, MAX(ThoiDiemNhan) as maxTDN
																		from HS_CVCMNV
																		group by MaGV
																	) as a
																	on HS_CVCMNV.MaGV = a.MaGV and HS_CVCMNV.ThoiDiemNhan = a.maxTDN
															) as a
														on a.MaCVCMNV = CV_ChuyenMonNghiepVu.MaCVCMNV
													) as dm_cmnv
												full outer  join
													(
													-- lấy mã dmdt và dmnc ứng với học hàm của giáo viên
														select MaGV, MaDMDT as dmdt_hh, MaDMNC as dmnc_hh from HocHam
														inner join
															(
															--lấy mã gv và mã học hàm có thời điểm gần nhất của gv
																select HS_HocHam.MaGV, MaHocHam from HS_HocHam
																inner join
																	(
																		select MaGV, MAX(ThoiDiemNhan) as maxTDN from HS_HocHam
																		group by MaGV
																	) as a
																on a.MaGV = HS_HocHam.MaGV and a.maxTDN = HS_HocHam.ThoiDiemNhan
															) as a
														on HocHam.MaHocHam = a.MaHocHam
													) as dm_hh
												on dm_cmnv.MaGV = dm_hh.MaGV
											) as a
									) as a
								on a.dmnc = DinhMucNghienCuu.MaDMNC
							) as a
						on a.dmdt = DinhMucDaoTao.MaDMDT
					) as a
				on GiaoVien.MaGV = a.MaGV
			) as a
		left join
			(
			-- chọn miễn giảm cao hơn trong 2 cái
				select MaGV, FLOOR(0.5 * ((mg_CVCQ + mg_CVD) + ABS(mg_CVCQ - mg_CVD))) as TongMienGiam from
					(
					-- lấy miễn giảm cvd và cvcq của mỗi gv
						select ISNULL(a.MaGV, b.MaGV) as MaGV, isNull(mg_CVD, 0) as mg_CVD, ISNULL(mg_CVCQ, 0) as mg_CVCQ from
							(
							-- lấy tỷ lệ miễn giảm ứng với mã miễn giảm của chức vụ đảng vừa lấy
								select MaGV, TyLe as mg_CVD from MienGiam
								inner join
									(
									--lấy mã miễn giảm ứng với cvd vừa lấy của mỗi giáo viên
										select MaGV, MaMienGiam from ChucVuDang
										inner join
											(
											--lấy chức vụ đảng gần nhất ứng với mỗi gv
												select HS_CVD.MaGV, MaCVD from HS_CVD
												inner join
													(
														select MaGV, MAX(ThoiDiemNhan) as maxTDN from HS_CVD
														group by MaGV
													) as a
												on HS_CVD.MaGV = a.MaGV and HS_CVD.ThoiDiemNhan = a.maxTDN
											) as a
										on a.MaCVD = ChucVuDang.MaCVD
									) as a
								on a.MaMienGiam = MienGiam.MaMienGiam
							) as a 
						full outer join
							(
							--lấy tỷ lệ miễn giảm của cvcq vừa lấy
								select MaGV, TyLe as mg_CVCQ from MienGiam
								inner join
									(
									-- lấy mã miễn giảm của cvcq vừa lấy
										select MaGV, MaMienGiam from ChucVuChinhQuyen
										inner join
											(
											--lấy mã giáo viên và chức vụ chính quyền gần nhất của gv đó
												select HS_CVCQ.MaGV, MaCVCQ from HS_CVCQ
												inner join
													(
														select MaGV, MAX(ThoiDiemNhan) as maxTDN from HS_CVCQ
														group by MaGV
													) as a
												on a.MaGV = HS_CVCQ.MaGV and a.maxTDN = HS_CVCQ.ThoiDiemNhan
											) as a
										on ChucVuChinhQuyen.MaCVCQ = a.MaCVCQ
									) as a
								on a.MaMienGiam = MienGiam.MaMienGiam
							) as b
							on a.MaGV = b.MaGV
					) as a
			) as b
		on a.MaGV = b.MaGV
	) as a 
go

create view DanhSachTaiGiangDay
as
	select MaGV, MaCVGD, SoTietDay, SoTietDay * TyLeGioChuan_GiangDay as TaiGiangDay, DonVi from
		(
			select MaGV, GiangDay.MaCVGD, TenCongViec, MaGCGD, SoTietDay from PhanCong_GiangDay
			inner join GiangDay
			on PhanCong_GiangDay.MaCVGD = GiangDay.MaCVGD
		) as a
	inner join
		(
			select MaGCGD, (SoGio/So) as TyLeGioChuan_GiangDay, DonVi from GioChuan_GiangDay
		) as b
	on a.MaGCGD = b.MaGCGD

	go

create view DanhSachTaiHoiDong
as
	select MaGV, MaCVHD, SoLan, (SoLan * TyLeGioChuan_HoiDong) as TaiHoiDong, DonVi from 
		(
			select MaGV, HoiDong.MaCVHD, MaGCHD, TenCongViec, SoLan from PhanCong_HoiDong
			inner join HoiDong
			on PhanCong_HoiDong.MaCVHD = HoiDong.MaCVHD
		) as a
	inner join
		(
			select MaGCHD, (SoGio/So) as TyLeGioChuan_HoiDong, DonVi from GioChuan_HoiDong
		) as b
	on a.MaGCHD = b.MaGCHD
	
go

create view DanhSachTaiKhaoThi
as
select MaGV, MaCVKT, SoBai, (SoBai * TyLeGioChuan_KhaoThi) as TaiKhaoThi , DonVi from 
	(
		select MaGV, KhaoThi.MaCVKT, TenCongViec, SoBai, MaGCKT from PhanCong_KhaoThi
		inner join KhaoThi
		on PhanCong_KhaoThi.MaCVKT = KhaoThi.MaCVKT
	) as a
inner join 
	(
		select MaGCKT, (SoGio/So) as TyLeGioChuan_KhaoThi, DonVi from GioChuan_KhaoThi
	) as b
on a.MaGCKT = b.MaGCKT

go

create view DanhSachTaiHuongDan
as
select MaGV, MaCVHDan, SoHocVien, TyLeGioChuan_HuongDan, (SoHocVien * TyLeGioChuan_HuongDan) as TaiHuongDan, DonVi from
	(
		select MaGV, a.MaCVHDan, MaGCHDan, SoHocVien from
			(
				select MaGV, HuongDan.MaCVHDan, MaGCHDan from PhanCong_HuongDan
				inner join HuongDan
				on PhanCong_HuongDan.MaCVHDan = HuongDan.MaCVHDan
			) as a
		inner join
			(
				select MaCVHDan, COUNT(MaHocVien) as SoHocVien from HuongDan_HocVien
				group by MaCVHDan
 			) as b
		on a.MaCVHDan = b.MaCVHDan
	) as a
inner join
	(
		select MaGCHDan ,(SoGio/So) as TyLeGioChuan_HuongDan, DonVi from GioChuan_HuongDan
	) as b
on a.MaGCHDan = b.MaGCHDan
go

create procedure TinhTaiDaoTao @MGV int
as
	select GV_DinhMuc.MaGV, dmdt, taiYeuCau_dt, TaiGiangDay, TaiHoiDong, TaiKhaoThi, TaiHuongDan, ThucTai_DaoTao from GV_DinhMuc
	inner join
		(
			select a.MaGV, TaiGiangDay, TaiHoiDong, TaiKhaoThi, TaiHuongDan, (TaiGiangDay + TaiHoiDong + TaiKhaoThi + TaiHuongDan) as ThucTai_DaoTao from
				(
					select a.MaGV, TaiGiangDay, TaiHoiDong, TaiKhaoThi from
						(
							select a.MaGV, TaiGiangDay, TaiHoiDong from
								(	
									select GV_DinhMuc.MaGV, SUM(ISNULL(TaiHoiDong, 0)) as TaiHoiDong  from GV_DinhMuc
									left join DanhSachTaiHoiDong
									on GV_DinhMuc.MaGV = DanhSachTaiHoiDong.MaGV
									where GV_DinhMuc.MaGV = @MGV
									group by GV_DinhMuc.MaGV
								) as a
							inner join
								(
									select GV_DinhMuc.MaGV, SUM(ISNULL(TaiGiangDay, 0)) as TaiGiangDay from GV_DinhMuc
									left join DanhSachTaiGiangDay
									on GV_DinhMuc.MaGV = DanhSachTaiGiangDay.MaGV
									where GV_DinhMuc.MaGV = @MGV
									group by GV_DinhMuc.MaGV
								) as b
							on a.MaGV = b.MaGV
						) as a
					inner join
						(
							select GV_DinhMuc.MaGV, SUM(ISNULL(TaiKhaoThi, 0)) as TaiKhaoThi from GV_DinhMuc
							left join DanhSachTaiKhaoThi
							on GV_DinhMuc.MaGV = DanhSachTaiKhaoThi.MaGV
							where GV_DinhMuc.MaGV = @MGV
							group by GV_DinhMuc.MaGV
						) as b
					on a.MaGV = b.MaGV
				) as a
			inner join
				(
					select GV_DinhMuc.MaGV, SUM(ISNULL(TaiHuongDan, 0)) as TaiHuongDan from GV_DinhMuc
					left join DanhSachTaiHuongDan
					on GV_DinhMuc.MaGV = DanhSachTaiHuongDan.MaGV
					where GV_DinhMuc.MaGV = @MGV
					group by GV_DinhMuc.MaGV
				) as b
			on a.MaGV = b.MaGV
		) as a
		on GV_DinhMuc.MaGV = a.MaGV
go

create proc TinhDinhMuc @MGV int
as
	select * from GV_DinhMuc
	where MaGV = @MGV
go

create view DanhSachCongTrinh_GV
as
select MaCTKH, MaGV, MaVT, MaLoaiCT, GioChuan, MaLoaiNC, So, SoTacGia from DonViTinh
inner join
	(
		select MaCTKH, MaGV, MaVT, LoaiCongTrinh.MaLoaiCT, MaLoaiNC, MaDVT, SoTacGia, GioChuan from LoaiCongTrinh
		inner join 
			(
				select CongTrinhKhoaHoc.MaCTKH, MaGV, MaVT, MaLoaiCT, MaLoaiNC, MaDVT, SoTacGia from PhanCong_NghienCuu
				inner join CongTrinhKhoaHoc
				on PhanCong_NghienCuu.MaCTKH = CongTrinhKhoaHoc.MaCTKH
			) as a
		on LoaiCongTrinh.MaLoaiCT = a.MaLoaiCT
	) as a
on a.MaDVT = DonViTinh.MaDVT
go

SET IDENTITY_INSERT [dbo].[MienGiam] ON 
go
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (1, 0)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (2, 5)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (3, 10)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (4, 15)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (5, 20)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (6, 25)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (7, 27)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (8, 30)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (9, 35)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (10, 40)
SET IDENTITY_INSERT [dbo].[MienGiam] OFF
SET IDENTITY_INSERT [dbo].[DinhMucDaoTao] ON 

INSERT [dbo].[DinhMucDaoTao] ([MaDMDT], [TenDinhMuc], [SoGioDMDT], [SoGioDMDT_TCQP]) VALUES (1, N'Giảng viên', 280, 420)
INSERT [dbo].[DinhMucDaoTao] ([MaDMDT], [TenDinhMuc], [SoGioDMDT], [SoGioDMDT_TCQP]) VALUES (2, N'Phó giáo sư và Giảng viên chính', 320, 460)
INSERT [dbo].[DinhMucDaoTao] ([MaDMDT], [TenDinhMuc], [SoGioDMDT], [SoGioDMDT_TCQP]) VALUES (3, N'Giáo sư và Giảng viên cao cấp', 360, 500)
SET IDENTITY_INSERT [dbo].[DinhMucDaoTao] OFF
SET IDENTITY_INSERT [dbo].[DinhMucNghienCuu] ON 

INSERT [dbo].[DinhMucNghienCuu] ([MaDMNC], [TenDinhMuc], [SoGioDMNC], [GioChuanDMNC]) VALUES (1, N'Giảng viên', 500, 150)
INSERT [dbo].[DinhMucNghienCuu] ([MaDMNC], [TenDinhMuc], [SoGioDMNC], [GioChuanDMNC]) VALUES (2, N'Phó giáo sư và Giảng viên chính', 600, 210)
INSERT [dbo].[DinhMucNghienCuu] ([MaDMNC], [TenDinhMuc], [SoGioDMNC], [GioChuanDMNC]) VALUES (3, N'Giáo sư và Giảng viên cao cấp', 700, 280)
SET IDENTITY_INSERT [dbo].[DinhMucNghienCuu] OFF
SET IDENTITY_INSERT [dbo].[HocVi] ON 

INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (1, N'Cử nhân')
INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (2, N'Kỹ sư')
INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (3, N'Thạc sỹ')
INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (4, N'Tiến sĩ')
INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (5, N'TSKH')
SET IDENTITY_INSERT [dbo].[HocVi] OFF
SET IDENTITY_INSERT [dbo].[HocHam] ON 

INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (1, 1, 1, N'Trợ giảng')
INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (2, 1, 1, N'Giảng viên')
INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (3, 2, 2, N'Giảng viên chính')
INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (4, 2, 2, N'Phó giáo sư')
INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (5, 3, 3, N'Giáo sư')

SET IDENTITY_INSERT [dbo].[HocHam] OFF

--continue

GO
SET IDENTITY_INSERT [dbo].[DonVi] ON 

INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (1, N'Công nghệ thông tin')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (2, N'Vô tuyến điện tử')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (3, N'Hóa lý kỹ thuật')
SET IDENTITY_INSERT [dbo].[DonVi] OFF
SET IDENTITY_INSERT [dbo].[ChucVuChinhQuyen] ON 

INSERT [dbo].[ChucVuChinhQuyen] ([MaCVCQ], [MaMienGiam], [TenCVCQ]) VALUES (1, 1, N'Giáo viên')
INSERT [dbo].[ChucVuChinhQuyen] ([MaCVCQ], [MaMienGiam], [TenCVCQ]) VALUES (2, 3, N'Chủ nhiệm bộ môn')
INSERT [dbo].[ChucVuChinhQuyen] ([MaCVCQ], [MaMienGiam], [TenCVCQ]) VALUES (3, 5, N'Chủ nhiệm khoa')
SET IDENTITY_INSERT [dbo].[ChucVuChinhQuyen] OFF
SET IDENTITY_INSERT [dbo].[ChucVuDang] ON 

INSERT [dbo].[ChucVuDang] ([MaCVD], [MaMienGiam], [TenCVD]) VALUES (1, 2, N'Đội viên')
INSERT [dbo].[ChucVuDang] ([MaCVD], [MaMienGiam], [TenCVD]) VALUES (2, 4, N'Đoàn viên')
INSERT [dbo].[ChucVuDang] ([MaCVD], [MaMienGiam], [TenCVD]) VALUES (3, 6, N'Đảng viên')

SET IDENTITY_INSERT [dbo].[ChucVuDang] OFF
SET IDENTITY_INSERT [dbo].[CV_ChuyenMonNghiepVu] ON 

INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (1, 1, 1, N'Giảng viên')
INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (2, 2, 2, N'Giảng viên chính')
INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (3, 2, 2, N'Phó giáo sư')
INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (4, 3, 3, N'Giảng viên cao cấp')
INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (5, 3, 3, N'Giáo sư')
SET IDENTITY_INSERT [dbo].[CV_ChuyenMonNghiepVu] OFF

--Continue 2
GO
SET IDENTITY_INSERT [dbo].[BoMon] ON 

INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (1, 1, N'Hệ thống thông tin')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (2, 1, N'Khoa học máy tính')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (3, 1, N'Công nghệ phần mềm')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (4, 1, N'Công nghệ mạng')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (5, 1, N'An toàn thông tin')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (6, 1, N'Toán')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (7, 2, N'Kỹ thuật cơ khí')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (8, 2, N'Kỹ thuật cơ điện tử')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (9, 2, N'Kỹ thuật điện điện tử')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (10, 3, N'Kỹ thuật điều khiển tự động hóa')
INSERT [dbo].[BoMon] ([MaBoMon], [MaKhoa], [TenBoMon]) VALUES (11, 3, N'Kỹ thuật xây dựng')
SET IDENTITY_INSERT [dbo].[BoMon] OFF

go


--Continue 3

GO
SET IDENTITY_INSERT [dbo].[HocPhan] ON 

INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (1, N'Hệ thống quản lý', 1, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (2, N'Tương tác người máy', 1, 20, 2)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (3, N'Cơ sở dữ liệu', 1, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (4, N'Trí tuệ nhân tạo', 2, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (5, N'Cấu trúc máy tính', 2, 20, 2)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (6, N'Kỹ thuật phần mềm', 3, 20, 2)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (7, N'Công nghệ web', 4, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (8, N'Công nghệ web 2', 4, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (9, N'Xác xuất thống kê', 6, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (10, N'Đại số tuyến tính', 6, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (11, N'Cơ khí 1', 7, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (12, N'Cơ khí 2', 7, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (13, N'Điện tử 1', 8, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (14, N'Điện tử 2', 8, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (15, N'Xây dựng 1', 11, 30, 3)
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [MaBoMon], [SoTiet], [SoTinChi]) VALUES (16, N'Xây dựng 2', 11, 40, 4)
SET IDENTITY_INSERT [dbo].[HocPhan] OFF

GO
SET IDENTITY_INSERT [dbo].[GioChuan_GiangDay] ON 

INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (1, N'Lý thuyết lớp >75 sinh viên', 1, N'tiết', 1)
INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (3, N'Lý thuyết lớp 76-100 sinh viên', 1.1, N'tiết', 1)
INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (4, N'Lý thuyết lớp >100 sinh viên', 1.2, N'tiết', 1)
INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (5, N'Hướng dẫn BT Thí nghiệm', 0.5, N'tiết', 1)
INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (6, N'Dạy ngoại ngữ cơ sở ngành', 0.8, N'tiết ', 1)
INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (8, N'Chuyên ngành ', 1, N'tiết ', 1)
INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (9, N'Dạy vẽ kỹ thuật ', 0.75, N'tiết', 1)
INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (10, N'Thể chất lý thuyết <=40, Bồi dưỡng <=15', 1, N'tiết', 1)
INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (11, N'Thể chất lý thuyết  >40, Bồi dưỡng  >15', 1.2, N'tiết ', 1)
INSERT [dbo].[GioChuan_GiangDay] ([MaGCGD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (12, N'Ngoại khóa', 2.5, N'ngày', 1)
SET IDENTITY_INSERT [dbo].[GioChuan_GiangDay] OFF
SET IDENTITY_INSERT [dbo].[GioChuan_HoiDong] ON 

INSERT [dbo].[GioChuan_HoiDong] ([MaGCHD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (1, N'Hội đồng đánh giá nghiệm thu đề tài nghiên cứu khoa học', 1, N'Đề tài ', 2)
INSERT [dbo].[GioChuan_HoiDong] ([MaGCHD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (2, N'Hội đồng bảo vệ LVTN', 5, N'lần ', 1)
INSERT [dbo].[GioChuan_HoiDong] ([MaGCHD], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (3, N'Hội đồng đánh giá NCKH', 2, N'lần ', 1)
SET IDENTITY_INSERT [dbo].[GioChuan_HoiDong] OFF
SET IDENTITY_INSERT [dbo].[GioChuan_HuongDan] ON 

INSERT [dbo].[GioChuan_HuongDan] ([MaGCHDan], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (1, N'Hướng dẫn đồ án môn cơ sở, tiểu luận KHXHNV 1-15 học viên', 1.5, N'Đồ án', 1)
INSERT [dbo].[GioChuan_HuongDan] ([MaGCHDan], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (2, N'Hướng dẫn đồ án môn cơ sở, tiểu luận KHXHNV >15 học viên', 0.75, N'Đồ án ', 1)
INSERT [dbo].[GioChuan_HuongDan] ([MaGCHDan], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (3, N'Hướng dẫn đồ án môn chuyên ngành 1-15 học viên', 2, N'Đồ án ', 1)
INSERT [dbo].[GioChuan_HuongDan] ([MaGCHDan], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (4, N'Hướng dẫn đồ án môn chuyên ngành >15 học viên', 1, N'Đồ án ', 1)
INSERT [dbo].[GioChuan_HuongDan] ([MaGCHDan], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (5, N'Hướng dẫn đồ án luận văn tốt nghiệp', 15, N'Đồ án ', 1)
INSERT [dbo].[GioChuan_HuongDan] ([MaGCHDan], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (6, N'Tiểu luận lớp ngắn hạn', 4, N'TL', 1)
SET IDENTITY_INSERT [dbo].[GioChuan_HuongDan] OFF
SET IDENTITY_INSERT [dbo].[GioChuan_KhaoThi] ON 

INSERT [dbo].[GioChuan_KhaoThi] ([MaGCKT], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (1, N'Chấm thi vấn đáp', 1, N'HV', 3)
INSERT [dbo].[GioChuan_KhaoThi] ([MaGCKT], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (2, N'Chấm thi viết ', 1, N'HV', 4)
INSERT [dbo].[GioChuan_KhaoThi] ([MaGCKT], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (3, N'Chấm thi tốt nghiệp', 1, N'HV ', 2)
INSERT [dbo].[GioChuan_KhaoThi] ([MaGCKT], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (4, N'Chấm BTL, tập bài', 1, N'BTL', 3)
INSERT [dbo].[GioChuan_KhaoThi] ([MaGCKT], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (5, N'Chấm đồ án môn cơ sở, tiểu luận KHXHNV ', 1, N'Đồ án', 2)
INSERT [dbo].[GioChuan_KhaoThi] ([MaGCKT], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (6, N'Chấm đồ án môn chuyên ngành', 1, N'Đồ án ', 2)
INSERT [dbo].[GioChuan_KhaoThi] ([MaGCKT], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (7, N'Chấm đồ án tốt nghiệp đại học', 5, N'Đồ án ', 1)
INSERT [dbo].[GioChuan_KhaoThi] ([MaGCKT], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (8, N'Chấm th tuyển đại học', 1, N'bài ', 5)
INSERT [dbo].[GioChuan_KhaoThi] ([MaGCKT], [TenGioChuan], [SoGio], [DonVi], [So]) VALUES (9, N'Sửa đổi, bổ sung ngân hàng đề thi', 15, N'Học phần', 1)
SET IDENTITY_INSERT [dbo].[GioChuan_KhaoThi] OFF



GO
SET IDENTITY_INSERT [dbo].[LoaiNghienCuu] ON 

INSERT [dbo].[LoaiNghienCuu] ([MaLoaiNC], [TenLoaiNC]) VALUES (1, N'Bài báo, báo cáo khoa học')
INSERT [dbo].[LoaiNghienCuu] ([MaLoaiNC], [TenLoaiNC]) VALUES (2, N'Đề tài nghiên cứu')
INSERT [dbo].[LoaiNghienCuu] ([MaLoaiNC], [TenLoaiNC]) VALUES (3, N'Biên soạn sách giáo khoa, giáo trình, TLTK')
INSERT [dbo].[LoaiNghienCuu] ([MaLoaiNC], [TenLoaiNC]) VALUES (4, N'Xây dựng bài thí nghiệm mới')
INSERT [dbo].[LoaiNghienCuu] ([MaLoaiNC], [TenLoaiNC]) VALUES (5, N'Dịch vụ tư vấn, chuyển giao công nghệ')
INSERT [dbo].[LoaiNghienCuu] ([MaLoaiNC], [TenLoaiNC]) VALUES (6, N'Sáng kiến cải tiến kỹ thuật cấp Học viện')
INSERT [dbo].[LoaiNghienCuu] ([MaLoaiNC], [TenLoaiNC]) VALUES (7, N'Giải thưởng sáng tạo cấp BQP')
INSERT [dbo].[LoaiNghienCuu] ([MaLoaiNC], [TenLoaiNC]) VALUES (8, N'Bằng phát minh')
SET IDENTITY_INSERT [dbo].[LoaiNghienCuu] OFF
SET IDENTITY_INSERT [dbo].[VaiTro] ON 

INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (1, 1, N'Tác giả chính')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (2, 1, N'Đồng tác giả')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (3, 2, N'Thành viên')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (4, 2, N'Thư ký')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (5, 2, N'CNĐT')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (6, 2, N'NCS')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (7, 2, N'KTV')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (8, 3, N'Chủ biên')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (9, 3, N'Tác giả')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (10, 4, N'Phụ trách')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (11, 4, N'Tham gia')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (12, 5, N'Phụ trách')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (13, 5, N'Tham gia')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (14, 6, N'Phụ trách')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (15, 6, N'Tham gia')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (16, 7, N'Phụ trách')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (17, 7, N'Tham gia')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (18, 8, N'Phụ trách')
INSERT [dbo].[VaiTro] ([MaVT], [MaLoaiNC], [TenVaiTro]) VALUES (19, 8, N'Tham gia')
SET IDENTITY_INSERT [dbo].[VaiTro] OFF
SET IDENTITY_INSERT [dbo].[LoaiCongTrinh] ON 

INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (1, N'Tạp chí ISI', 200)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (2, N'Tạp chí Quốc tế non-ISI', 150)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (3, N'Tạp chí Quốc gia', 100)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (4, N'Kỷ yếu hội nghị quốc tế', 150)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (5, N'Kỷ yếu hội nghị quốc gia', 150)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (6, N'Nghiên cứu cấp Nhà nước', 400)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (8, N'Nghiên cứu cấp Bộ/ NĐT/ NCCB', 300)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (9, N'Nghiên cứu cấp HV/ Cục NT/ TCKT', 250)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (10, N'Sách chuyên khảo ', 3)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (11, N'Giáo trình mới', 150)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (12, N'Giáo trình tái bản', 120)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (13, N'Tài liệu biên dịch, sách tham khảo', 100)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (14, N'Sách hướng dẫn đối với học phần chưa có giáo trình', 75)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (15, N'Sau đại học', 150)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (16, N'Đại học', 100)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (17, N'Đầu tư < 100tr', 120)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (18, N'100tr < DT < 1 tỷ', 230)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (19, N'1 tỷ < DT < 5 tỷ', 300)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (20, N'DT > 5 tỷ', 400)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (21, N'Cá nhân', 60)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (22, N'Nhóm', 150)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (23, N'Cá nhân', 80)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (24, N'Nhóm', 200)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (25, N'Cấp Quận', 100)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (26, N'Cấp Tỉnh', 200)
INSERT [dbo].[LoaiCongTrinh] ([MaLoaiCT], [TenLoaiCT], [GioChuan]) VALUES (27, N'Cấp Quốc gia', 400)


SET IDENTITY_INSERT [dbo].[LoaiCongTrinh] OFF
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (1, 1)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (1, 2)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (1, 3)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (1, 4)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (1, 5)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (2, 6)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (2, 8)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (2, 9)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (3, 10)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (3, 11)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (3, 12)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (3, 13)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (3, 14)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (4, 15)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (4, 16)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (5, 17)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (5, 18)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (5, 19)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (5, 20)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (6, 21)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (6, 22)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (7, 23)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (7, 24)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (8, 25)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (8, 26)
INSERT [dbo].[LoaiNC_LoaiCT] ([MaLoaiNC], [MaLoaiCT]) VALUES (8, 27)


GO
SET IDENTITY_INSERT [dbo].[VaiTroHoiDong] ON 

INSERT [dbo].[VaiTroHoiDong] ([MaVTHD], [TenVaiTro]) VALUES (1, N'Chủ tịch')
INSERT [dbo].[VaiTroHoiDong] ([MaVTHD], [TenVaiTro]) VALUES (2, N'Thư kí')
INSERT [dbo].[VaiTroHoiDong] ([MaVTHD], [TenVaiTro]) VALUES (3, N'Ủy viên')
INSERT [dbo].[VaiTroHoiDong] ([MaVTHD], [TenVaiTro]) VALUES (4, N'Phản biện')
INSERT [dbo].[VaiTroHoiDong] ([MaVTHD], [TenVaiTro]) VALUES (5, N'Chấm ĐATN')
INSERT [dbo].[VaiTroHoiDong] ([MaVTHD], [TenVaiTro]) VALUES (6, N'Trưởng TB')
INSERT [dbo].[VaiTroHoiDong] ([MaVTHD], [TenVaiTro]) VALUES (7, N'Giới thiệu')
INSERT [dbo].[VaiTroHoiDong] ([MaVTHD], [TenVaiTro]) VALUES (8, N'Chủ tọa')
SET IDENTITY_INSERT [dbo].[VaiTroHoiDong] OFF


go


-- Them Cong viec Giang day
create procedure Them_CVGiangDay
	@tenHP nvarchar(MAX),
	@tenlop nvarchar(MAX),
	@he nvarchar(MAX),
	@siso int,
	@tenCVGD nvarchar(MAX),
	@tengiochuan nvarchar(MAX)
as
begin 
	insert into Lop (MaHocPhan, TenLop, He, SiSo)
	select MaHocPhan, @tenlop, @he, @siso
	from HocPhan
	where TenHocPhan = @tenHP

	insert into GiangDay(MaGCGD, TenCongViec, MaLop)
	select MaGCGD,@tenCVGD, Lop.MaLop
	from GioChuan_GiangDay g, Lop
	where g.TenGioChuan = @tengiochuan and Lop.TenLop = @tenlop
end 

go

-- ví dụ sử dụng proc Them_CVGD
-- exec Them_CVGiangDay @tenHP = N'Hệ thống quản lý', @tenlop = N'HTTT', @he = N'DS', @siso = 80, @tenCVGD = N'Giảng dạy Đại học', @tengiochuan = N'Lý thuyết lớp 76-100 sinh viên'



--Them cong viec Huong dan

go

create procedure Them_CVHD
	@maCV int,
	@tenHV nvarchar(MAX),
	@tenlop nvarchar(MAX),
	@tendetai nvarchar(MAX)
as
begin	
	insert into HocVien(TenHocVien) values (@tenHV)

	insert into HocVien_Lop(MaHocVien, MaLop)
	select MaHocVien, MaLop
	from HocVien, Lop
	where TenLop = @tenlop and TenHocVien = @tenHV

	insert into HuongDan_HocVien (MaCVHDan, MaHocVien, TenDeTai)
	select MaCVHDan, max(hv.MaHocVien), @tendetai
	from HuongDan, HocVien hv
	where MaCVHDan = @maCV
	group by MaCVHDan

end

--ví dụ thực thi Them_CVHD
--exec Them_CVHD @maCV = 2, @tenHV = N'Nguyễn Anh Tùng', @tenlop = N'HTTT', @tendetai = N'ABC'

--THÊM CÔNG VIỆC HỘI ĐỒNG
go
create procedure Them_CVHDong
	@tenCVHDong nvarchar(MAX),
	@tengiochuan nvarchar(MAX),
	@tenHDong nvarchar(MAX)
as 
begin
	insert into HoiDong(MaGCHD, TenCongViec, TenHoiDong)
	select MAGCHD, @tenCVHDong, @tenHDong
	from GioChuan_HoiDong g
	where g.TenGioChuan = @tengiochuan
end

--ví dụ thực thi thêm CV hội đồng
--exec Them_CVHDong @tenCVHDong = N'Hội đồng đại học/ cao đằng', @tengiochuan = N'Hội đồng bảo vệ LVTN', @tenHDong = N'Hội Đồng HVKTQS bảo vệ LVTN Đại học'


--THEM CONG VIỆC KHẢO THÍ
go
create procedure Them_KhaoThi
	@tenCVKT nvarchar(MAX),
	@tengiochuan nvarchar(MAX),
	@tenlop nvarchar(MAX)
as
begin
	insert into KhaoThi (MaGCKT, TenCongViec, MaLop)
	select MaGCKT, @tenCVKT, MaLop
	from GioChuan_KhaoThi g, Lop
	where g.TenGioChuan = @tengiochuan and Lop.TenLop = @tenlop
end


--ví dụ thực thi thêm Khảo thí
--exec Them_KhaoThi @tenCVKT = N'Thi vấn đáp', @tengiochuan = N'Chấm thi vấn đáp', @tenlop = N'HTTT'


-- Them một nhiệm vụ nghiên cứu

go
create procedure Them_NVNC
	@tenloainghiencuu nvarchar(MAX),
	@tenCTNC nvarchar(MAX),
	@loaiCT nvarchar(MAX),
	@so int,
	@donvitinh nvarchar(MAX),
	@sotacgia int
as
begin
	insert into DonViTinh(DonVi, So)
	values (@donvitinh, @so)

	insert into CongTrinhKhoaHoc(MaLoaiCT, MaLoaiNC, MaDVT, TenCTNC, SoTacGia)
	select lct.MaLoaiCT, lnc.MaLoaiNC, MAX(MaDVT), @tenCTNC, @sotacgia
	from LoaiCongTrinh lct, LoaiNghienCuu lnc, DonViTinh
	where lct.TenLoaiCT = @loaiCT and lnc.TenLoaiNC = @tenloainghiencuu
	group by lct.MaLoaiCT, lnc.MaLoaiNC
end

--ví dụ thực thi Them_NVNC
--exec Them_NVNC @tenloainghiencuu = N'Bài báo, báo cáo khoa học', @tenCTNC = N'ABCXYZ', @loaiCT = N'Tạp chí ISI', @so = 250, @donvitinh = N'trang', @sotacgia = 6


go
create view ShowCTKH
as
select MaCTKH, TenCTNC, LoaiCongTrinh.TenLoaiCT , CongTrinhKhoaHoc.SoTacGia , DonViTinh.So, DonViTinh.DonVi, LoaiNghienCuu.TenLoaiNC
from CongTrinhKhoaHoc
inner join LoaiCongTrinh on CongTrinhKhoaHoc.MaLoaiCT= LoaiCongTrinh.MaLoaiCT
inner join DonViTinh on CongTrinhKhoaHoc.MaDVT = DonViTinh.MaDVT
inner join LoaiNghienCuu on LoaiNghienCuu.MaLoaiNC = CongTrinhKhoaHoc.MaLoaiNC
go

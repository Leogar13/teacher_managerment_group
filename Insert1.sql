SET IDENTITY_INSERT [dbo].[MienGiam] ON 

INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (1, 95)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (2, 85)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (3, 80)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (4, 70)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (5, 50)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (6, 25)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (7, 23)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (8, 20)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (9, 15)
INSERT [dbo].[MienGiam] ([MaMienGiam], [TyLe]) VALUES (10, 10)
SET IDENTITY_INSERT [dbo].[MienGiam] OFF
SET IDENTITY_INSERT [dbo].[DinhMucDaoTao] ON 

INSERT [dbo].[DinhMucDaoTao] ([MaDMDT], [TenDinhMuc], [SoGioDMDT], [SoGioDMDT_TCQP]) VALUES (1, N'Giáo sư và Giảng viên cao cấp', 360, 500)
INSERT [dbo].[DinhMucDaoTao] ([MaDMDT], [TenDinhMuc], [SoGioDMDT], [SoGioDMDT_TCQP]) VALUES (2, N'Phó giáo sư và Giảng viên chính', 320, 460)
INSERT [dbo].[DinhMucDaoTao] ([MaDMDT], [TenDinhMuc], [SoGioDMDT], [SoGioDMDT_TCQP]) VALUES (3, N'Giảng viên', 280, 420)
SET IDENTITY_INSERT [dbo].[DinhMucDaoTao] OFF
SET IDENTITY_INSERT [dbo].[DinhMucNghienCuu] ON 

INSERT [dbo].[DinhMucNghienCuu] ([MaDMNC], [TenDinhMuc], [SoGioDMNC], [GioChuanDMNC]) VALUES (1, N'Giáo sư và Giảng viên cao cấp', 700, 280)
INSERT [dbo].[DinhMucNghienCuu] ([MaDMNC], [TenDinhMuc], [SoGioDMNC], [GioChuanDMNC]) VALUES (2, N'Phó giáo sư và Giảng viên chính', 600, 210)
INSERT [dbo].[DinhMucNghienCuu] ([MaDMNC], [TenDinhMuc], [SoGioDMNC], [GioChuanDMNC]) VALUES (3, N'Giảng viên', 500, 150)
SET IDENTITY_INSERT [dbo].[DinhMucNghienCuu] OFF
SET IDENTITY_INSERT [dbo].[HocVi] ON 

INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (1, N'Cử nhân')
INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (2, N'Kỹ sư')
INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (3, N'Thạc sỹ')
INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (4, N'Tiến sĩ')
INSERT [dbo].[HocVi] ([MaHocVi], [TenHocVi]) VALUES (5, N'TSKH')
SET IDENTITY_INSERT [dbo].[HocVi] OFF
SET IDENTITY_INSERT [dbo].[HocHam] ON 

INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (1, 1, 1, N'Giáo sư')
INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (2, 2, 2, N'Phó giáo sư')
INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (3, 2, 2, N'Giảng viên chính')
INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (4, 3, 3, N'Giảng viên')
INSERT [dbo].[HocHam] ([MaHocHam], [MaDMDT], [MaDMNC], [TenHocHam]) VALUES (5, NULL, NULL, N'Trợ giảng')
SET IDENTITY_INSERT [dbo].[HocHam] OFF

--continue

GO
SET IDENTITY_INSERT [dbo].[DonVi] ON 

INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (1, N'Công nghệ thông tin')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (2, N'Vô tuyến điện tử')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (3, N'Hóa lý kỹ thuật')
SET IDENTITY_INSERT [dbo].[DonVi] OFF
SET IDENTITY_INSERT [dbo].[ChucVuChinhQuyen] ON 

INSERT [dbo].[ChucVuChinhQuyen] ([MaCVCQ], [MaMienGiam], [TenCVCQ]) VALUES (1, 9, N'Giáo viên')
INSERT [dbo].[ChucVuChinhQuyen] ([MaCVCQ], [MaMienGiam], [TenCVCQ]) VALUES (2, 6, N'Chủ nhiệm bộ môn')
INSERT [dbo].[ChucVuChinhQuyen] ([MaCVCQ], [MaMienGiam], [TenCVCQ]) VALUES (3, 5, N'Chủ nhiệm khoa')
SET IDENTITY_INSERT [dbo].[ChucVuChinhQuyen] OFF
SET IDENTITY_INSERT [dbo].[ChucVuDang] ON 

INSERT [dbo].[ChucVuDang] ([MaCVD], [MaMienGiam], [TenCVD]) VALUES (1, 7, N'Đảng viên')
INSERT [dbo].[ChucVuDang] ([MaCVD], [MaMienGiam], [TenCVD]) VALUES (2, 9, N'Đoàn viên')
INSERT [dbo].[ChucVuDang] ([MaCVD], [MaMienGiam], [TenCVD]) VALUES (3, 10, N'Đội viên')
SET IDENTITY_INSERT [dbo].[ChucVuDang] OFF
SET IDENTITY_INSERT [dbo].[CV_ChuyenMonNghiepVu] ON 

INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (1, 1, 1, N'Giáo sư')
INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (2, 1, 1, N'Giảng viên cao cấp')
INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (3, 2, 2, N'Phó giáo sư')
INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (4, 2, 2, N'Giảng viên chính')
INSERT [dbo].[CV_ChuyenMonNghiepVu] ([MaCVCMNV], [MaDMDT], [MaDMNC], [TenCVCMNV]) VALUES (5, 3, 3, N'Giảng viên')
SET IDENTITY_INSERT [dbo].[CV_ChuyenMonNghiepVu] OFF

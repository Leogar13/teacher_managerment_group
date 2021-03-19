USE [Teacher]
GO
SET IDENTITY_INSERT [dbo].[DonVi] ON 

INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (1, N'Công nghệ Thông tin')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (2, N'Vô tuyến Điện tử')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (3, N'Hóa lý Kĩ thuật')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (4, N'Hàng không Vũ trụ')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (5, N'Kỹ thuật Điều khiển')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (6, N'Vũ khí')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (7, N'Cơ khí')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (8, N'Kỹ thuật Hóa học')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (9, N'Toán-Lý kĩ thuật')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (10, N'Khoa học Quân sự')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (11, N'Công tác Đảng và Chính trị học')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (12, N'Khoa Mác-Lênin và Tư tưởng Hồ Chí Minh')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (13, N'Khoa ngoại ngữ')
INSERT [dbo].[DonVi] ([MaKhoa], [TenKhoa]) VALUES (14, N'Chỉ huy, tham mưu')

SET IDENTITY_INSERT [dbo].[DonVi] OFF


Use Teacher
go
Set identity_insert dbo.GiaoVien Off

Insert into dbo.GiaoVien (MaGV, MaBoMon, TenGiaoVien, QueQuan, GioiTinh, NgaySinh) Values (1, 4, N'Lê Minh Hiếu', N'Hà Nội', N'Nam', 07/02/1986) 

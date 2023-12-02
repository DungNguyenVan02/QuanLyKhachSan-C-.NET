create database QuanLyKhachSan
go

-- Phòng
-- Tầng
-- Loại phòng
-- user
-- Khách hàng
-- Hóa đơn


create table tbl_users(
	username nvarchar(50) not null,
	fullname nvarchar(50) not null,
	passwd nvarchar(100) not null,
	type int not null, --1: Admin && 0: Nhân viên
	primary key (username)
)
go



create table tbl_khachhang(
	id_khachhang int identity,
	hoten nvarchar(50) not null,
	ngaysinh nvarchar(50) not null,
	gioitinh nvarchar(30) not null,
	dienthoai nvarchar(20) not null,
	email nvarchar(50) not null,
	diachi nvarchar(50) not null,
	cccd nvarchar(30) not null
	primary key (id_khachhang)
)
go


create table tbl_tang(
	tentang nvarchar(50) not null default N'Chưa đặt tên',
	primary key (tentang)
)
go



create table tbl_loaiphong(
	tenloaiphong nvarchar(50) not null default N'Chưa đặt tên',
	gia float not null default 0,
	primary key (tenloaiphong)
)
go



create table tbl_dichvu(
	id_dichvu int identity,
	tendichvu nvarchar(50) not null default N'Chưa đặt tên',
	gia float not null default 0,
	primary key (id_dichvu)
)
go



create table tbl_phong(
	id_phong int identity,
	tenloaiphong nvarchar(50) not null,
	tentang nvarchar(50) not null,
	tenphong nvarchar(50) not null default N'Chưa đặt tên',
	songuoi int,
	soluong int,
	trangthai nvarchar(50) not null default N'Trống', -- Trống || Đã đặt
	primary key (id_phong),
	foreign key (tenloaiphong) references tbl_loaiphong(tenloaiphong),
	foreign key (tentang) references tbl_tang(tentang),
)
go



create table tbl_hoadon(
	id_hoadon int identity,
	id_khachhang int not null,
	id_phong int not null,
	id_dichvu int not null,
	ngayvao date default getdate(),
	ngaytra date,
	trangthai int not null default 0, -- 0: Chưa thanh toán || 1: đã thanh toán
	primary key (id_hoadon),
	foreign key (id_khachhang) references tbl_khachhang(id_khachhang),
	foreign key (id_phong) references tbl_phong(id_phong),
	foreign key (id_dichvu) references tbl_dichvu(id_dichvu),
)
go


-----------------------------------------------------------------------------------

create proc USP_GetAccountByUserName
@userName nvarchar(50)
as
begin
	select * from tbl_users where username = @userName
end
go

exec USP_GetAccountByUserName @username = N'ADMIN'
go

create proc USP_Login
@userName nvarchar(50), @passWord nvarchar(50)
as
begin
	select * from tbl_users where username = @userName and passwd = @passWord 
end
go

insert into tbl_users values(N'ADMIN',N'Nguyễn Văn Dũng', N'123456', 1)

exec USP_Login @userName = N'ADMIN', @passWord = N'123456'
go

insert into tbl_tang values (4)
insert into tbl_loaiphong values (N'Thường', 300000)
insert into tbl_phong values (N'Cao cấp', N'4', N'P403', 0, 4, N'Trống')


select * from tbl_phong

DECLARE @i int = 0
while @i <= 5
begin
	insert tbl_phong values (N'Thường', N'2',(N'P20' + CAST(@i as nvarchar(50))) , 0, 4)
	set @i = @i + 1
end


select * from tbl_loaiphong

create proc USP_GetRoomList
as select * from tbl_phong
go

select * from tbl_phong where tenloaiphong = N'Thường'

exec USP_GetRoomList




select * from tbl_loaiphong


insert into tbl_khachhang values (N'Nguyễn Văn Dũng', N'04/07/2002', N'0374882673', N'dungdung12@gmail.com',N'Thái Bình', N'222336928123')

select * from tbl_hoadon
insert into tbl_hoadon values (N'1', N'3', N'2', N'Staff',GETDATE(),null, 0)
insert into tbl_dichvu values (N'Bể bơi 4 mùa', 100000)



insert into tbl_hoadon values (2, 5, 1, GETDATE(), Null, 0)

select COUNT(tendichvu) from tbl_dichvu

select f.fullname,c.id_khachhang ,c.hoten, b.tenphong, b.tenloaiphong, d.gia as N'giaphong', DATEDIFF(Day,a.ngayvao, a.ngaytra) as N'songay', d.gia * (DATEDIFF(Day,a.ngayvao, a.ngaytra)) as N'tongtien'   from tbl_hoadon as a, tbl_phong as b, tbl_khachhang as c, tbl_loaiphong as d, tbl_users as f where a.username = f.username and b.tenloaiphong = d.tenloaiphong and a.id_khachhang = c.id_khachhang and a.id_phong = b.id_phong and a.trangthai = 0 and b.id_phong = 3 group by f.fullname,c.id_khachhang ,c.hoten, b.tenphong, b.tenloaiphong, d.gia, DATEDIFF(Day,a.ngayvao, a.ngaytra)



select a.tendichvu, a.gia, b.id_phong from tbl_dichvu as a, tbl_hoadon as b where a.id_dichvu = b.id_dichvu and b.id_phong = 4 group by a.tendichvu, a.gia, b.id_phong

select * from tbl_hoadon
go

select * from tbl_phong where tenloaiphong = N'Thường' and trangthai = N'Trống'



select * from tbl_khachhang




alter proc USP_InsertBill
@id_khachhang int, @id_phong int, @id_dichvu int, @ngayvao date, @ngaytra date
as
begin
	begin
		insert into tbl_hoadon(id_khachhang, id_phong, id_dichvu, ngayvao, ngaytra, trangthai) values (@id_khachhang, @id_phong, @id_dichvu, @ngayvao, @ngaytra, 0)
	end
end
go
exec USP_InsertBill


drop proc USP_InsertBill

go



create proc USP_InsertKhachHang 
@hoten nvarchar(50), @ngaysinh nvarchar(50), @dienthoai nvarchar(10), @email nvarchar(50), @diachi nvarchar(50), @cccd nvarchar(30), @gioitinh nvarchar(30)
as
begin
	insert tbl_khachhang(hoten, ngaysinh, dienthoai, email, diachi, cccd, gioitinh) values (@hoten, @ngaysinh, @dienthoai, @email, @diachi, @cccd, @gioitinh)
end
go

drop proc USP_InsertKhachHang





create proc USP_GetIdRoombyName 
@tenphong nvarchar(50)
as
begin
	select id_phong from tbl_phong where tenphong = @tenphong
end
go

create proc USP_GetIdDichVubyName 
@tendichvu nvarchar(50)
as
begin
	select id_dichvu from tbl_dichvu where tendichvu = @tendichvu
end
go

exec USP_GetIdRoombyName N'P202'


select a.tendichvu, a.gia, b.id_phong from tbl_dichvu as a, tbl_hoadon as b, tbl_khachhang as c where a.id_dichvu = b.id_dichvu and b.id_phong = 3 and b.id_khachhang = c.id_khachhang and b.trangthai = 0 group by a.tendichvu, a.gia, b.id_phong



Create trigger UTG_InsertBill
on tbl_hoadon for insert
as
begin
	  declare @id_hoadon int
	  select @id_hoadon = id_hoadon from inserted

	  declare @id_phong int
	  select @id_phong = id_phong from tbl_hoadon where id_hoadon = @id_hoadon and trangthai = 0

	  update tbl_phong set trangthai = N'Có người' where id_phong = @id_phong
end
go

alter trigger UTG_UpdateBill
on tbl_hoadon for update
as
begin
	  declare @id_hoadon int
	  select @id_hoadon = id_hoadon from inserted

	  declare @id_phong int
	  select @id_phong = id_phong from tbl_hoadon where id_hoadon = @id_hoadon

	  declare @count int = 0;

	  select @count = count(*) from tbl_hoadon where id_phong = @id_phong and trangthai = 0

	  if(@count > 0)
		update tbl_phong set trangthai = N'Có người' where id_phong = @id_phong
	else 
		update tbl_phong set trangthai = N'Trống' where id_phong = @id_phong
end
go


drop trigger UTG_UpdateBill


declare @id_phongnew int = 3
select id_hoadon from tbl_hoadon where id_phong = @id_phongnew

declare @id_phongprev int = 4

update tbl_hoadon set id_hoadon = @id_phongprev where id_hoadon in (select )
go
create proc USP_SwitchRoom
@idFristRoom int, @idSeconrdRoom int
as
begin
	
	select id_phong into IDBillInfoTable from tbl_phong where id_phong = @idFristRoom

	update tbl_hoadon set id_phong = @idSeconrdRoom where id_phong = @idFristRoom

	update tbl_hoadon set id_phong = @idFristRoom where id_hoadon in (select * from IDBillInfoTable)

	drop table IDBillInfoTable
end
go

drop proc USP_SwitchRoom


update tbl_phong set trangthai = N'Trống'


select c.id_khachhang ,c.hoten, b.tenphong, b.tenloaiphong, d.gia as N'giaphong', DATEDIFF(Day,a.ngayvao, a.ngaytra) as N'songay', d.gia * (DATEDIFF(Day,a.ngayvao, a.ngaytra)) as N'tongtien',  e.gia as N'giadichvu'  from tbl_hoadon as a, tbl_phong as b, tbl_khachhang as c, tbl_loaiphong as d, tbl_dichvu as e where b.tenloaiphong = d.tenloaiphong and a.id_khachhang = c.id_khachhang and a.id_phong = b.id_phong and a.trangthai = 0 and b.id_phong = 3 and a.id_dichvu = e.id_dichvu group by c.id_khachhang ,c.hoten, b.tenphong, b.tenloaiphong, d.gia, DATEDIFF(Day,a.ngayvao, a.ngaytra), e.gia


alter proc USP_GetListBillBydate
@ngayvao date, @ngayra date
as
begin

	select b.tenphong as [Tên phòng], a.ngayvao as [Ngày vào], a.ngaytra as [Ngày ra], d.tendichvu as [Tên dịch vụ], a.tongtien as [Tổng tiền]
	from tbl_hoadon as a,tbl_phong as b, tbl_loaiphong as c, tbl_dichvu as d
	where a.trangthai = 1 and a.ngayvao >= @ngayvao and a.ngaytra <= @ngayra and a.id_phong = b.id_phong and a.id_dichvu = d.id_dichvu and b.tenloaiphong = c.tenloaiphong
	group by b.tenphong, a.ngayvao, a.ngaytra, d.tendichvu, a.tongtien
end
go


select * from tbl_users

exec USP_UpdateAccount N'ADMIN', N'DUNG NGUYEN', N'1', N'123'
alter proc USP_UpdateAccount
@username nvarchar(50), @fullname nvarchar(50), @passwd nvarchar(50), @newPasswd nvarchar(50)
as
begin
	declare @isRightPass int = 0

	select @isRightPass = count(*) from tbl_users where username = @username and passwd = @passwd

	if (@isRightPass = 1) 
	begin
		if (@newPasswd = null or @newPasswd = '')
			begin
				update tbl_users set fullname = @fullname where username = @username
			end
		else
			begin
				update tbl_users set fullname = @fullname, passwd = @newPasswd where username = @username
			end
	end
end
go

select * from tbl_hoadon

update tbl_phong set trangthai = N'Trống' where id_phong = 12

create trigger UTG_DeleteBill
on tbl_hoadon for delete
as
begin
	declare @idHoadon int
	select @idHoadon = id_hoadon from deleted
	declare @idPhong int

	select @idPhong = id_phong from tbl_hoadon where id_hoadon = @idHoadon

	declare @count int = 0
	select @count = Count(*) from tbl_hoadon where id_hoadon = @idHoadon and trangthai = 0

	if(@count = 0)
		update tbl_phong set trangthai = N'Trống' where id_phong = @idPhong
end
go


create proc USP_GetListBillBydateAndPage
@ngayvao date, @ngayra date, @page int
as
begin
	declare @pageRows int  = 10
	declare @selectRows int = @pageRows * @page
	declare @exceptRows int = (@page - 1) * @pageRows

	; with BillShow as (select b.tenphong as [Tên phòng], a.ngayvao as [Ngày vào], a.ngaytra as [Ngày ra], d.tendichvu as [Tên dịch vụ], a.tongtien as [Tổng tiền]
	from tbl_hoadon as a,tbl_phong as b, tbl_loaiphong as c, tbl_dichvu as d
	where a.trangthai = 1 and a.ngayvao >= @ngayvao and a.ngaytra <= @ngayra and a.id_phong = b.id_phong and a.id_dichvu = d.id_dichvu and b.tenloaiphong = c.tenloaiphong
	group by b.tenphong, a.ngayvao, a.ngaytra, d.tendichvu, a.tongtien)

	select top (@selectRows) * from BillShow
	except
	select top (@exceptRows) * from BillShow
end
go

EXEC USP_GetListBillBydateAndPage 
@ngayvao = N'2023-04-22', @ngayra = N'2023-04-25', @page= 1


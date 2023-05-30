--Создание контейнера базы данных--
create database Computer_magazine
go

--Активизирование контейнера базы данных--
use Computer_magazine
go

--пользовательские типы данных--
create type Adres from varchar(70);
go

create type applicationS from varchar(100);
go

create type TNumber from char(12);
go


--Создание отношений--

--Родительские--

--Тип продкута--
create table Product_type(Cod_type smallint primary key,
						  Product_type varchar(20))
go

insert into Product_type(Cod_type, Product_type)
values (2000, 'PC'),
	   (2010, 'Laptop'),
	   (2020, 'Monitor'),
	   (2030, 'Phone'),
	   (2040, 'Tablet PC'),
	   (2050, 'Printer')
go


--Производитель--
create table manufacturer(Man_code int primary key,
						  Man_name varchar(20),
						  Man_Adres Adres,
						  Man_Production_price smallint)
go

insert into manufacturer(Man_code, Man_name, Man_Adres, Man_Production_price)
values  (4001, 'Xiaomi', 'str. Petru Movila 8', 2000),
		(4002, 'Samsung', 'str. Uzinelor 10', 3000),
		(4003, 'LG', 'str. Muncesti 7', 3000),
		(4004, 'HP', 'str. Gogol 36', 2000),
		(4005, 'Hyper PC', 'str. Mihail Sadoveanu', 4000)
go


--Оплата--
create table Payment(Pay_code int primary key,
					 Pay_type varchar(15) default 'cash')
go

insert into Payment(Pay_code, Pay_type)
values  (101, 'credit'),
		(102, 'credit card'),
		(103, 'cash')
go


--Паспортные данные--
create table Pasport_data(Client_id int primary key,
						  IDNP char(13) unique not null,
						  C_Adres Adres,
						  C_Surname varchar(12),
						  C_Name varchar(12),
						  C_FatherName varchar(12))
go

insert into Pasport_data(Client_id, IDNP, C_Adres, C_Surname, C_Name, C_FatherName)
values (1001, '1111111111111', 'str. Puschin 6/7', 'Balconschii', 'Andrei', 'Nicolai'),
	   (1002, '2222222222222', 'str. Nicolae Milescu Spatarul 21/1', 'Bezuhov', 'Pier', 'Anatol'),
	   (1003, '3333333333333','str. Sadoveanu 13/5', 'Rostov', 'Nicolai', 'Ilie'),
	   (1004, '4444444444444','str. Petru Zadnipru 28/8', 'Boliakin', 'Andrei', 'Nikifor'),
	   (1005, '5555555555555','str. Muncesti 76/9', 'Shiskin', 'Alexandr', 'Igor')
go



--Родительски-дочерние--

--Поставщик--
create table supplier(Sup_code int primary key,
					  Sup_name varchar(20),
					  Sup_Adres Adres,
					  production_type varchar(40),
					  Telephone TNumber,
					  Man_cod int foreign key references manufacturer(Man_code) not null)
go

insert into supplier(Sup_code, Sup_name, Man_cod, Sup_Adres, production_type, Telephone)
values  (3001, 'Comp_Moldova', 4003, 'str. Stefan cel mare 21/5', 'PCs, Laptops, Monitors, Phones', '+37367354672'),
		(3002, 'Hyper Supply', 4005, 'str. Gogol 34/4', 'PCs, Monitors, Printers, Laptops', '+37369984519'),
		(3003, 'GMSupply', 4003, 'str. Columna 5', 'PCs, Laptops', '+37378534765'),
		(3004, 'TechMD', 4002, 'str. Uzinelor 7', 'Monitors, Phones, Laptops', '+37379945543'),
		(3005, 'XMoldova', 4001, 'str. Petru Movila 7', 'Laptops, Phones, Printers, Monitors', '+37360606607'),
		(3006, 'HPSupply', 4004, 'str. Mihail Sadoveanu 3', 'PCs, Monitors, Printers', '+37369984519')
go

--Чек--
create table Chek(Check_code int primary key,
				  Client_id int foreign key references Pasport_data(Client_id) not null,
				  Pay_code int foreign key references Payment(Pay_code) not null)
go

insert into Chek(Check_code, Client_id, Pay_code)
values  (1013, 1001, 101),
		(1014, 1003, 102),
		(1017, 1005, 103),
		(1020, 1002, 103),
		(1022, 1004, 102)
go


--Товар--
create table Product(cod_prod int primary key,
					 Cod_type smallint foreign key references Product_type(Cod_type) not null,
					 Man_cod int foreign key references manufacturer(Man_code) not null,
					 Prod_name varchar(20),
					 Price smallint,
					 Waranty smallint check(Waranty <= 5) default 1,
					 Picture varchar(100) not null unique)
go

insert into Product(cod_prod, Cod_type, Man_cod, Prod_name, Price, Waranty, Picture)
values  (5000, 2000, 4005, 'Blye Yeti', 20000, 5, '../../resources/DataBase/images/blueYeti.png'),
		(5010, 2010, 4003, 'Nitro', 25000 ,3, '../../resources/DataBase/images/Nitro.png'),
		(5020, 2020, 4003, 'Spin 5', 5000, 1, '../../resources/DataBase/images/spin5.png'),
		(5021, 2020, 4004, 'Hp one', 2000, 1, '../../resources/DataBase/images/HPOne.png'),
		(5030, 2030, 4002, 'Redmi 5 plus', 4000, 1, '../../resources/DataBase/images/redmi5plus.png'),
		(5040, 2040, 4001, 'Prestige Pro 3', 4700, 2, '../../resources/DataBase/images/Prestige.png'),
		(5050, 2050, 4004, 'All Vew Pro', 2000, 3, '../../resources/DataBase/images/SamsungAllView.png'),
		(5051, 2050, 4001, 'Clear Sky', 3000, 4, '../../resources/DataBase/images/CanonClearSky.png'),
		(5052, 2050, 4001, 'Colors', 2500, 2, '../../resources/DataBase/images/EpsonColors.png')

go

--Дочерние--

--Компьютер--
create table Computer(cod_prod int foreign key references Product(cod_prod) unique not null, --связь 1 к 1--
					  CPU_freq smallint default 2000,
					  RAM smallint default 1000,
					  HDD smallint default 250,
					  CD varchar(5) check((CD = 'true') or (CD = 'false')) default 'false',
					  Applic applicationS)
go

insert into Computer(cod_prod, CPU_freq, RAM, HDD, CD, Applic)
values  (5000, 4200, 16000, 1000, 'false', 'Gaming'),
		(5010, 3900, 8000, 2500, 'true', 'Gaming')

go


--Монитор--
create table Monitor(cod_prod int foreign key references Product(cod_prod) unique not null,
					 Tatrix_type varchar(5) default 'TN',
					 Diagonal smallint default 21,
					 Monitor_type varchar(20) default 'without rotation',
					 Matrix_size varchar(10) default 'glossy',
					 Applic applicationS)
go

insert into Monitor(cod_prod, Tatrix_type, Diagonal, Monitor_type, Matrix_size, Applic)
values (5020, 'IPS', 21, 'with rotation', 'mate', 'Gaming'),
	   (5021, 'TN', 18, 'without rotation', 'glossy','Office')

go


--Телефон--
create table Phone(cod_prod int foreign key references Product(cod_prod) unique not null,
				   CPU_freq smallint default 1000,
				   RAM smallint default 1000,
				   Intern_mem smallint default 5000,
				   Applic applicationS)
go

insert into Phone(cod_prod, CPU_freq, RAM, Intern_mem, Applic)
values (5030, 2000, 3000, 32000, 'Daily'),
	   (5040, 3000, 4000, 32000, 'Graphic design')

go


--Принтер--
create table Printer(cod_prod int foreign key references Product(cod_prod) unique not null,
					 Printer_type varchar(6) check((Printer_type = 'color') or (Printer_type = 'laser')) default 'laser',
					 Applic applicationS)
go

insert into Printer(cod_prod, Printer_type, Applic)
values  (5050, 'laser', 'Office'),
		(5051, 'color', 'Photos'),
		(5052, 'color', 'Photos')

go


--Журнал поставки--
create table consignment(Sup_code int foreign key references supplier(Sup_code) not null,
						 cod_prod int foreign key references  Product(cod_prod) not null,
						 prod_number smallint,
						 Man_code int foreign key references  manufacturer(Man_code) not null,
						 consignment_number int,
						 Consignment_date date,
						 Price int)
go

insert into consignment(Sup_code, cod_prod, prod_number, Man_code, consignment_number, Consignment_date, Price)
values  (3002, 5000, 5, 4005, 10456, '2021-12-21', 17000),
		(3003, 5010, 3, 4003, 10432, '2022-01-04', 14000),
		(3001, 5020, 4, 4003, 10487, '2021-10-10', 3000),
		(3006, 5021, 7, 4004, 10499, '2022-01-09', 1000),
		(3004, 5030, 10, 4002, 10444, '2022-02-27', 2500),
		(3005, 5040, 8, 4001, 10411, '2022-01-01', 3000),
		(3006, 5050, 11, 4004, 10410, '2022-02-04', 1500),
		(3005, 5051, 15, 4001, 10440, '2022-02-02', 2000),
		(3005, 5052, 12, 4001, 10404, '2021-12-13', 1700)

go


--Информация в чеке--
create table Chek_infoChek(Check_code int foreign key references Chek(Check_code) not null,
						   cod_prod int foreign key references Product(cod_prod) not null,
						   Purc_date date,
						   Gen_price smallint)
go

insert into Chek_infoChek(Check_code, cod_prod, Purc_date, Gen_price)
values  (1013, 5000, '2021-12-31', 20000),
		(1014, 5052, '2021-12-31', 2500),
		(1017, 5040, '2022-01-08', 4700),
		(1020, 5021, '2022-01-09', 2000),
		(1022, 5030, '2022-02-28', 4000)

go


--Отношение для пользователей приложения--
create table Users(user_login varchar(15) unique not null,
				   user_password varchar(16) not null,
				   user_role varchar(7) not null check(user_role = 'user' or user_role='admin' or user_role = 'manager' or user_role='seller'),
				   user_email varchar(30) not null unique)
go

insert into Users values('User', '12345_Test', 'user', 'botannicolai22@gmail.com'),
						('Seller', '1111_Seller', 'seller', 'nckthegamer@gmail.com'),
						('Admin', '98765_Admin', 'admin', 'botsan12@gmail.com'),
						('Manager', '777_Manager', 'manager', 'tinaalekseeva389@gmail.com')

go

--Представления--

--Создаём представление для журнала поставок--
create view Sup_Cons_view as
select Sup_name, cod_prod, prod_number, Man_code, consignment_number, Consignment_date, Price
from supplier inner join consignment on supplier.Sup_code = consignment.Sup_code
go

create view Prod_Cons_view as
select Sup_name, Prod_name, prod_number, Man_code, consignment_number, Consignment_date, Sup_Cons_view.Price
from Product inner join Sup_Cons_view on Product.cod_prod = Sup_Cons_view.cod_prod
go

create view consignment_view as
select Sup_name, Prod_name, prod_number, Man_name, consignment_number, Consignment_date, Prod_Cons_view.Price
from manufacturer inner join Prod_Cons_view on manufacturer.Man_code = Prod_Cons_view.Man_code
go

select * from consignment_view
go


--Создаём представление для чека--
create view Chek_payment_view as
select Check_code, Client_id, Pay_type
from Payment inner join Chek on Payment.Pay_code = Chek.Pay_code
go

create view Chek_Pasport_data as
select Check_code, IDNP, Pay_type
from Pasport_data inner join Chek_payment_view on Pasport_data.Client_id = Chek_payment_view.Client_id
go

create view Chek_Chek_info_payment_view as
select Chek_infoChek.Check_code, IDNP, Pay_type, cod_prod, Purc_date, Gen_price
from Chek_infoChek inner join Chek_Pasport_data on Chek_infoChek.Check_code = Chek_Pasport_data.Check_code
go

create view Chek_view as
select Check_code, IDNP, Pay_type, Prod_name, Purc_date, Gen_price
from Product inner join Chek_Chek_info_payment_view on Product.cod_prod = Chek_Chek_info_payment_view.cod_prod
go

select * from Chek_view
go


--Создаём представление для товаров--
create view Product_Type_view as
select cod_prod, Product_type, Man_cod, Prod_name, Price, Waranty
from Product inner join Product_type on Product.Cod_type = Product_type.Cod_type
go	

create view Product_view as
select Product_type, Man_name, Prod_name, Price, Waranty
from Product_Type_view inner join manufacturer on Product_Type_view.Man_cod = manufacturer.Man_code
go

select * from Product_view
go




--Запросы--

select * from Pasport_data
go

select * from Payment
go

select * from manufacturer
go

select * from Product_type
go

select * from supplier
go

select * from Chek
go

select * from Product
go

select * from consignment
go

select * from Chek_infoChek
go

select * from Computer
go

select * from Phone
go

select * from Monitor
go

select * from Printer
go

select * from Users
go



--Printer(Printer_type)--
update Printer
set Printer_type = 'laser'
where cod_prod = 5052
go

update Printer
set Printer_type = 'color'
where cod_prod = 5052
go


--manufacterer(Man_name)
update manufacturer
set Man_name = 'Lenovo'
where Man_name = 'HP'
go

update manufacturer
set Man_name = 'HP'
where Man_name = 'Lenovo'
go


--supplier(Sup_name)
update supplier
set Sup_name = 'SamsungS_MD'
where Sup_name = 'TechMD'
go

update supplier
set Sup_name = 'TechMD'
where Sup_name = 'SamsungS_MD'
go



--manufacturer(Man_name)--
declare @CManName varchar(20);
set @CManName = 'Test Manufacturer';

delete from manufacturer where Man_name = @CManName
go


--Pasport_data(IDNP)--
declare @CIDNP char(13);
set @CIDNP = '1234567890987';

delete from Pasport_data where IDNP = @CIDNP
go



--supplier(Sup_name)
declare @CSupName varchar(20);
set @CSupName = 'Test supplier';

delete from supplier where Sup_name = @CSupName
go



--все компьютеры и ноутбуки среди товаров--
select Product_type as 'Product type', Man_cod as 'Manufacturer code', Prod_name as 'Product name', Price, Waranty
from Product inner join Product_type on Product.Cod_type = Product_type.Cod_type
where Product_type = 'PC' or Product_type = 'Laptop'
go

--все поставки в периоде с 21.12.2021 по 04.02.2022--
select * 
from consignment inner join Product on consignment.cod_prod = Product.cod_prod
where Consignment_date >= '2021-12-21' and Consignment_date <= '2022-02-04'
go

--все компьютеры частота процессоров которых > 3900--
select * from Computer
where CPU_freq > 3900
go

--Вывести всех поставщиков, поставляющих телефоны--
select *
from supplier
where production_type like '%Phones%'
go



--названия и дату всех купленных телефонов--
select Prod_name as 'Phone', Purc_date as 'Purcase date'
from Chek_infoChek inner join Product on Chek_infoChek.cod_prod = Product.cod_prod
where Cod_type = (select Cod_type from Product_type
				  where Product_type = 'Phone'
				  )
go


--вывести покупателей, коротые расплатились за товар наличными--
select IDNP, C_Adres as 'Client adres', C_Surname as 'Client surname', C_Name as 'Client name', C_FatherName as 'Client father name'
from Pasport_data
where Client_id in (select Client_id 
					from Chek
					where Pay_code = (select Pay_code
									  from Payment
									  where Pay_type = 'cash'
									  )
					)
go



--Вывести тип постевленной продукции за 2021-12-21 число--
select Product_type
from Product_type
where Cod_type in (select Cod_type
				   from Product
				   where cod_prod in (select cod_prod
									  from consignment
									  where Consignment_date = '2021-12-21'
									  )
				   )
go

--select * from Users where user_login = 'user' collate Latin1_General_CS_AI

/*
useful
https://vc.ru/dev/169094-registrozavisimyy-poisk-v-sql
https://habr.com/ru/companies/otus/articles/461231/
*/





--Хранимые процедуры--

--Процедура ИЗМЕНЕНИЯ названия продукта--
create procedure dbo.Change_product_name(@CProductName varchar(20), @CNewProductName varchar(20)) as
	update Product
	set Prod_name = @CNewProductName
	where Prod_name = @CProductName
go


declare @COldProduct varchar(20), @CNewProduct varchar(20);
set @COldProduct = 'Redmi 5 pro';
set @CNewProduct = 'Redmi note 11';


exec dbo.Change_product_name @COldProduct, @CNewProduct
go



update Product
set Prod_name = 'Redmi 5 pro'
where Prod_name = 'Redmi note 11'
go


select *
from Product
go




--создадим процедуру ДОБАВЛЕНИЯ информации о новом продукте--
create procedure dbo.Add_new_product(@ProdType varchar(20), @ManName varchar(20), @ProdName varchar(20), @ProdCoefficient decimal(3,1), @Waranty smallint, @SupName varchar(20), @ProdAmount smallint, @ConsDate date, @ConsCoef decimal(3, 1), @ProdPrice money) as
	

	if(@ProdType = (select Product_type
					from Product_type
					where Product_type = @ProdType))
	begin
		
		declare @ProdTypeCod smallint;
		set @ProdTypeCod = (select Cod_type
							from Product_type
							where Product_type = @ProdType)
		
		if(@ManName = (select Man_name
					   from manufacturer
					   where Man_name = @ManName))
		begin

			declare @ManCode int;
			set @ManCode = (select Man_code
							from manufacturer
							where Man_name = @ManName)


			if(@SupName in (select Sup_name
						   from supplier
						   where Sup_name = @SupName))
			begin

				declare @SupCode int;

				set @SupCode = (select Sup_code
								from supplier
								where Sup_name = @SupName)
				
				if(@ProdTypeCod in (select Cod_type
								   from supplier_products
								   where Sup_code = @SupCode))
				begin
					declare @ProdCod int, @ConsNumb int;
					set @ProdCod = (select max(cod_prod)
									from Product) + 1;

					set @ConsNumb = (select max(consignment_number)
										from consignment) + 1;


					insert into Product
					values (@ProdCod, @ProdTypeCod, @ProdName, @ProdCoefficient, @Waranty);

					insert into consignment
					values(@SupCode, @ProdCod, @ProdAmount, @ManCode, @ConsNumb, @ConsDate, @ConsCoef);

					insert into manufacturer_pruduction_price
					values (@ManCode, @ProdCod, @ProdPrice);


				end
				else
				begin
					print 'This supplier does not supply such type of production!'
					end
			end
			else
			begin
				print 'Sorry, such supplier has not been found!'
			end
		end
		else
		begin
			print 'Sorry, such manufacturer has not been found!'
		end
	end
	else
	begin
		print 'Sorry, such product type has not been found!'
	end

	
go



declare @ProdType varchar(20), @ManName varchar(20), @ProdName varchar(20), @ProdCoefficient decimal(3,1), @Waranty smallint, @SupName varchar(20), @ProdAmount smallint, @ConsDate date, @ConsCoef decimal(3, 1), @ProductionPrice money;
--set @ProdType = 'Test';
set @ProdType = 'Laptop';
--set @ManName = 'Test';
set @ManName = 'Xiaomi';
set @ProdName = 'Test';
set @ProdCoefficient = 5;
set @Waranty = 5;
--set @SupName = 'Test111';
set @SupName = 'XMoldova';
set @ProdAmount = 5;
set @ConsDate = '2022-02-02';
set @ConsCoef = 6;
set @ProductionPrice = 9999;

exec dbo.Add_new_product @ProdType, @ManName, @ProdName, @ProdCoefficient, @Waranty, @SupName, @ProdAmount, @ConsDate, @ConsCoef, @ProductionPrice
go



--Процедура УДАЛЕНИЯ принтеров по из типу (цветной или лазкрный)--
create procedure dbo.Delete_printers_by_type(@CPrinterType varchar(6)) as

delete from Product where cod_prod in (select cod_prod
									   from Printer
									   where Printer_type = @CPrinterType
									  )
go


declare @CPrinter varchar(6);
set @CPrinter = 'laser';

exec dbo.Delete_printers_by_type @CPrinter
go






select * from Users
go

select * from Product
go

--update Users set user_password = '12345_Test' where user_login = @user_login

select * from supplier

delete from supplier where Sup_code = 6668

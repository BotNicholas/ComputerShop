--Создание контейнера базы данных--
create database Computer_magazine
go


--Активизирование контейнера базы данных--
use Computer_magazine
go

create type Adres from varchar(70);
go

create type applicationS from varchar(100);
go

create type TNumber from char(12);
go






create table Payment(Pay_code int primary key,
					 Pay_type varchar(15) default 'cash')
go


insert into Payment(Pay_code, Pay_type)
values  (101, 'credit'),
		(102, 'credit card'),
		(103, 'cash')
go




create table Pasport_data(Id int primary key,
						  C_Adres Adres,
						  C_Surname varchar(12),
						  C_Name varchar(12),
						  C_FatherName varchar(12),
						  IDNP varchar(13),
						  Pay_code int foreign key references Payment(Pay_code) not null)
go

insert into Pasport_data(Id, C_Adres, C_Surname, C_Name, C_FatherName, IDNP, Pay_code)
values (1001, 'ул.Пушкина 6/7', 'Балконский', 'Андрей', 'Николаевич', '1234567890123', 102),
	   (1002, 'ул. Николае Милеску Спэтарул 21/1', 'Безухов', 'Пьер', 'Анатольевичь', '7563549876234', 101),
	   (1003, 'ул. Садовяну 13/5', 'Ростов', 'Николай', 'Ильич', '9175462090876', 103),
	   (1004, 'ул. Петру Заднипру 28/8', 'Болякин', 'Андрей', 'Никифорович', '123454554321', 102),
	   (1005, 'ул. Мунчешть 76/9', 'Шишкин', 'Алескандр', 'Игоревич', '7567493352417', 103)
go


create table Chek(Check_code int primary key,
				  Pay_code int foreign key references Payment(Pay_code) not null)
go


insert into Chek(Check_code, Pay_code)
values  (1013, 101),
		(1014, 102),
		(1017, 103),
		(1020, 103),
		(1022, 102)
go


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





create table Phone(cod_prod int foreign key references Product(cod_prod) unique not null,
				   CPU_freq smallint default 1000,
				   RAM smallint default 1000,
				   Intern_mem smallint default 5000,
				   Applic applicationS)
go


insert into Phone(cod_prod, CPU_freq, RAM, Intern_mem, Applic)
values (5030, 2000, 3000, 32000, 'Dayly'),
	   (5040, 3000, 4000, 32000, 'Graphic design')

go




create table Printer(cod_prod int foreign key references Product(cod_prod) unique not null,
					 Printer_type varchar(6) check((Printer_type = 'color') or (Printer_type = 'laser')) default 'laser',
					 Applic applicationS)
go


insert into Printer(cod_prod, Printer_type, Applic)
values  (5050, 'laser', 'Office'),
		(5051, 'color', 'Photos'),
		(5052, 'color', 'Photos')

go



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


select *
from Product_type
go



create table Product(cod_prod int primary key,
					 Cod_type smallint foreign key references Product_type(Cod_type) not null,
					 /*Man_cod int foreign key references manufacturer(Man_code) not null, -Данное поле пришло в ненодобность, так как было создано новое отношение manufacturer_pruduction_price, через которое можно найти информацию о производителе*/
					 Prod_name varchar(20),
					 Сoefficient decimal(3,1) default 1.5,
					 Waranty smallint check(Waranty <= 5) default 1)
go


insert into Product(cod_prod, Cod_type, Prod_name, Сoefficient, Waranty)
values  (5000, 2000, 'Blye Yeti', 2, 5),
		(5010, 2010, 'Nitro', 1.5, 3),
		(5020, 2020, 'Spin 5', 1.5, 1),
		(5021, 2020, 'Hp one', 2, 1),
		(5030, 2030, 'Redmi 5 pro', 1.5, 1),
		(5040, 2040, 'Prestige Pro 3', 1.5, 2),
		(5050, 2050, 'All Vew Pro', 2, 3),
		(5051, 2050, 'Clear Sky', 1.5, 4),
		(5052, 2050, 'Colors', 3, 2)

go


create table Chek_infoChek(Check_code int foreign key references Chek(Check_code) not null,
						   cod_prod int foreign key references Product(cod_prod) not null,
						   Purc_date date default getdate(),
						   Gen_price smallint)
go

insert into Chek_infoChek(Check_code, cod_prod, Purc_date, Gen_price)
values  (1013, 5000, '2021-12-31', 20000),
		(1014, 5052, '2021-12-31', 2500),
		(1017, 5040, '2022-01-08', 4700),
		(1020, 5021, '2022-01-09', 2000),
		(1022, 5030, '2022-02-28', 4000)

go


create table manufacturer(Man_code int primary key,
						  Man_name varchar(20),
						  Man_Adres Adres)
go


insert into manufacturer(Man_code, Man_name, Man_Adres)
values  (4001, 'Xiaomi', 'ул. Петру Мовилэ 8'),
		(4002, 'Samsung', 'ул. Узинелор 10'),
		(4003, 'LG', 'ул. Мунчешть 7'),
		(4004, 'HP', 'ул. Гоголя 36'),
		(4005, 'Hyper PC', 'ул. Михаил Садовяну 10'),
		(4006, 'Game Max', 'ул. Отовыска 7')

go



select *
from manufacturer
go




create table manufacturer_pruduction_price(Man_code int foreign key references manufacturer(Man_code) not null,
										   cod_prod int foreign key references Product(cod_prod) not null unique,
										   Price money)
go

insert into manufacturer_pruduction_price
values (4005, 5000, 2000),
	   (4006, 5010, 3000),
	   (4003, 5020, 2500),
	   (4004, 5021, 2000),
	   (4002, 5030, 3000),
	   (4001, 5040, 1500),
	   (4004, 5050, 2000),
	   (4001, 5051, 3000),
	   (4001, 5052, 2000)
go



create table consignment(Sup_code int foreign key references supplier(Sup_code) not null,
						 cod_prod int foreign key references  Product(cod_prod) not null,
						 prod_number smallint,
						 Man_code int foreign key references  manufacturer(Man_code) not null,
						 consignment_number int,
						 Consignment_date date default getdate(),
						 Coefficient decimal(3,1) default 0.5
						 )
go

insert into consignment(Sup_code, cod_prod, prod_number, Man_code, consignment_number, Consignment_date, Coefficient)
values  (3002, 5000, 5, 4005, 10456, '2021-12-21', 1),
		(3003, 5010, 3, 4006, 10432, '2022-01-04', 0.5),
		(3001, 5020, 4, 4003, 10487, '2021-10-10', 0.5),
		(3006, 5021, 7, 4004, 10499, '2022-01-09', 0.7),
		(3004, 5030, 10, 4002, 10444, '2022-02-27', 1),
		(3005, 5040, 8, 4001, 10411, '2022-01-01', 0.8),
		(3006, 5050, 11, 4004, 10410, '2022-02-04', 1),
		(3005, 5051, 15, 4001, 10440, '2022-02-02', 1.5),
		(3005, 5052, 12, 4001, 10404, '2021-12-13', 1.7)

go


create table supplier(Sup_code int primary key,
					  Sup_name varchar(20),
					  Company varchar(20),
					  Sup_Adres Adres)
go


alter table supplier
add Telephone TNumber
go

alter table supplier
drop column Company
go

alter table supplier
add Man_cod int foreign key references manufacturer(Man_code) not null
go




insert into supplier(Sup_code, Sup_name, Man_cod, Sup_Adres, Telephone)
values  (3001, 'Comp_Moldova', 4003, 'ул. Штефан чел маре 21/5', '+37367354672'),
		(3002, 'Hyper Supply', 4005, 'ул. Гоголя 34/4', '+37369984519'),
		(3003, 'GMSupply', 4006, 'ул. Колумна 5','+37378534765'),
		(3004, 'TechMD', 4002, 'ул. Узинелор 7','+37379945543'),
		(3005, 'XMoldova', 4001, 'ул. Петру Мовилэ 7','+37360606607'),
		(3006, 'Hyper Supply', 4004, 'ул. Михаил Садовяну 3','+37369984519')
go

update supplier
set Sup_name = 'HPSupply'
where Sup_code = 3006
go



select *
from supplier
go




create table supplier_products(Sup_code int foreign key references supplier(Sup_code) not null,
							   Cod_type smallint foreign key references Product_type(Cod_type) not null)


insert into supplier_products
values (3001, 2000),
	   (3001, 2010),
	   (3001, 2020),
	   (3001, 2030),
	   (3002, 2000),
	   (3002, 2020),
	   (3002, 2050),
	   (3002, 2010),
	   (3003, 2000),
	   (3003, 2010),
	   (3004, 2020),
	   (3004, 2030),
	   (3004, 2010),
	   (3005, 2010),
	   (3005, 2020),
	   (3005, 2030),
	   (3005, 2050),
	   (3006, 2000),
	   (3006, 2020),
	   (3006, 2050)
go
















select *
from supplier
go

/*
drop table consignment 
go
*/


/*
drop table Chek_infoChek 
go
*/

--Укажите, какая информация в созданной базе данных может изменяться и примените команды группы DML для её изменения.--

--Printer(Printer_type)--
update Printer
set Printer_type = 'laser'
where cod_prod = 5052
go

update Printer
set Printer_type = 'color'
where cod_prod = 5052
go


--Computer(RAM, HDD, CD)--
update Computer
set RAM = 32000
where (cod_prod = 5000)
go

update Computer
set RAM = 16000
where (cod_prod = 5000)
go


--Product(price, name)--
update manufacturer_pruduction_price
set Price = 1500
where cod_prod = 5000   --Blue Yeti--
go

select *
from Product



update Product
set Prod_name = 'Blue Yeti'
where Prod_name = 'Blye Yeti'
go

update manufacturer_pruduction_price
set Price = 20000
where cod_prod = 5000 --Blue Yeti--
go


--manufacterer(Man_name, Man_Production_price)
update manufacturer
set Man_name = 'Lenovo'
where Man_name = 'HP'
go

update manufacturer
set Man_name = 'HP'
where Man_name = 'Lenovo'
go


update manufacturer_pruduction_price
set Price = 7000
where Price = 2000
go

update manufacturer_pruduction_price
set Price = 2000
where Price = 7000
go


select *
from manufacturer
go


--supplier(production_type, Sup_name)

update supplier
set Sup_name = 'SamsungS_MD'
where Sup_name = 'TechMD'
go

update supplier
set Sup_name = 'TechMD'
where Sup_name = 'SamsungS_MD'
go


/*
update supplier
set production_type = 'Monitors, Phones, Laptops, PCs'
where Sup_name = 'TechMD'
go

update supplier
set production_type = 'Monitors, Phones, Laptops'
where Sup_name = 'TechMD'
go
*/



--Создайте необходимые индексы--
/*Можно было бы создать индексы также и для отношений product_type и Payment, но в данных отношениях только два столбца, один из которых первичный ключ */

--создание индексов для отношения manufacturer (редко вносим, часто берём => index)--
create index man_name_indx on manufacturer(Man_name)
go

create index man_adres_indx on manufacturer(Man_Adres)
go

--создание индексов для отношения supplyer (редко вносим, часто берём => index)--

create index sup_name_indx on supplier(Sup_name)
go

create index sup_adres_indx on supplier(Sup_Adres)
go

create index sup_telephone_indx on supplier(Telephone)
go

/*
create index sup_prodType_indx on supplier(production_type)
go
*/





--************************************--
create procedure dbo.Show_all as

select *
from Chek

select *
from Chek_infoChek

select *
from Computer

select *
from consignment

select *
from manufacturer

select *
from manufacturer_pruduction_price

select *
from Monitor

select *
from Pasport_data

select *
from Payment

select *
from Phone

select *
from Printer

select *
from Product

select *
from Product_type

select *
from supplier

select *
from supplier_products
go



exec dbo.Show_all
go






--Individual work 31.03.22--





--5 сложных запросов с различными типами соединений--


/*Это сложный запрос с функцией агрегации*/

--посмотреть на какую сумму было поставлено каждого товара--
/*Для этого создадим представление*/
create view Product_Type_view as
select cod_prod, Product_type, Prod_name, Product.Сoefficient, Waranty
from Product inner join Product_type on Product.Cod_type = Product_type.Cod_type
go

create view Product_view as
select Product_Type_view.cod_prod, Product_type, Prod_name, Сoefficient, Waranty, Man_code, Price
from Product_Type_view inner join manufacturer_pruduction_price on Product_Type_view.cod_prod = manufacturer_pruduction_price.cod_prod
go


select *
from Product_view
go

select *
from consignment


select Product_type as Product, sum((Price + consignment.Coefficient*Price)*prod_number) as Total_price
from Product_view inner join consignment on Product_view.cod_prod = consignment.cod_prod
group by Product_type
go


--Вывести каким образом расчитался покупатель Болякин Андрей Никифорович за товар--
select Pay_type as Paying_by
from Payment inner join Pasport_data on Payment.Pay_code = Pasport_data.Pay_code
where C_Name = 'Андрей' and C_Surname = 'Болякин' and C_FatherName = 'Никифорович'
go

--то же, но с подзапросами--

select Pay_type as Paying_by
from Payment
where Pay_code = (select Pay_code
				  from Pasport_data
				  where C_Name = 'Андрей' and C_Surname = 'Болякин' and C_FatherName = 'Никифорович'
				 )
go



--вывести, какие товары были поставлены в феврале (02)--
select Prod_name as Product
from Product inner join consignment on Product.cod_prod = consignment.cod_prod
where month(Consignment_date) = 2
go

--то же, но с подзапросами--
select Prod_name as Product
from Product
where cod_prod in (select cod_prod
				   from consignment
				   where month(Consignment_date) = 2
				  )
go



--вывести все продажи за 2021-12-31--
--естественное соединение c представлением Product_Type_view--
select distinct Product_type
from Product_Type_view inner join Chek_infoChek on Product_Type_view.cod_prod = Chek_infoChek.cod_prod
where Purc_date = '2021-12-31'
go

--то же, но с подзапросами--
select Product_type
from Product_Type
where Cod_type in (select Cod_type
				   from Product
				   where cod_prod in (select cod_prod
									  from Chek_infoChek
									  where Purc_date = '2021-12-31'
									  )
				  )
go


/*сложный запрос с группировкой и функцией агрегации*/

--вывести количекство продаж каждого типа продукции за 2021-12-31--
select Product_type, count(Product_type) as amount_of_products
from Product_Type_view inner join Chek_infoChek on Product_Type_view.cod_prod = Chek_infoChek.cod_prod
where Purc_date = '2021-12-31'
group by Product_type
go

--+ Часть в подзапросах...--






--5 подзапросов--


/*Это сложный запрос с подзапросом*/

--Вывести технические характеристики всех имеющихся компьютеров--
select Prod_name as Product, CPU_freq as CPU, RAM, HDD, CD, Applic as Aplication
from Computer inner join Product on Computer.cod_prod = Product.cod_prod
where Cod_type = (select Cod_type
				  from Product_type
				  where Product_type = 'PC'
				 )
go



/*Это сложный запрос с подзапросом*/

--Вывести информацию из всех чеков, оплаченных наличкой--
select Prod_name as Product, Purc_date as Purcasing_date, Gen_price as General_price
from Chek_infoChek inner join Product on Chek_infoChek.cod_prod = Product.cod_prod
where Check_code in (select Check_code      --in, а не =, так как по итогу создастся отношение (Pay_type = 'cash' > 2-х кортежей) => в Chek_infoChek передастся уже не 1 значение, а > 1 (2)--
					from Chek
					where Pay_code = (select Pay_code
									  from Payment
									  where Pay_type = 'cash'
									 )
					)
go



--посмотреть, какие товары поставляет поставщик XMoldova--
select Product_type
from Product_type
where Cod_type in (select Cod_type
				   from Product
				   where cod_prod in(select cod_prod
									 from consignment
									 where Sup_code = (select Sup_code
													   from supplier
													   where Sup_name = 'XMoldova'
													  )				
								   )
				 )
go



--вывести тип продукции, которую производит компания LG--
select Product_type
from Product_type
where Cod_type in (select Cod_type
				   from Product
				   where cod_prod in (select cod_prod
									  from manufacturer_pruduction_price
									  where Man_code = (select Man_code
													   from manufacturer
													   where Man_name = 'LG')
								   )
				 )
go


/*Подзапрос со сложным запросом с функцией агрегации*/
--вывести самый дорогой товар--
select Prod_name as Product, Price
from Product inner join manufacturer_pruduction_price on Product.cod_prod = manufacturer_pruduction_price.cod_prod
where Price = (select max(Price)
			   from manufacturer_pruduction_price)
go



--Вывести технические характеристики Redmi 5 pro--
select CPU_freq as CPU, RAM, Intern_mem as Memory, Applic as Aplication
from Phone
where cod_prod = (select cod_prod
				  from Product
				  where Prod_name = 'Redmi 5 pro'
				 )
go

--То же, но с естественным соединением--
select CPU_freq as CPU, RAM, Intern_mem as Memory, Applic as Aplication
from Product inner join Phone on Phone.cod_prod = Product.cod_prod
where Prod_name = 'Redmi 5 pro'
go















--5 запросов на группировку и агрегацию информации--

--посчитать количество товаров, поставленных в каждом месяце--
select month(Consignment_date) as mounth, sum(prod_number) as supply_amount
from consignment
group by month(Consignment_date)
go



--посчитать общую цену всех товаров, поставленных в феврале (02)--
select sum(consignment.Coefficient*Price*prod_number) as Total_suply_price
from consignment inner join manufacturer_pruduction_price on consignment.cod_prod = manufacturer_pruduction_price.cod_prod
where month(Consignment_date) = 2
go

select *
from consignment
go

select *
from manufacturer_pruduction_price
go



--подстчитать число каждой продукции имеющейся в магазине--
select Product_type as Production, count(*) as Products_amount
from Product_type inner join Product on Product_type.Cod_type = Product.Cod_type
group by Product_type
go



--вывести количество продаж за 2021-12-31--
select count(cod_prod) as amount_of_products
from Chek_infoChek
where Purc_date = '2021-12-31'
go



--подсчитать количество покупателей для каждого способа оплаты--
/*Создадим представление*/
create view client_payment_view as
select id, C_Adres, C_Surname, C_Name, C_FatherName, Pay_type
from Payment inner join Pasport_data on Payment.Pay_code = Pasport_data.Pay_code
go

select *
from client_payment_view
go --создаем представление так как Pay_type не входит в Pasport_data и в  результате не произойдет агрегация--

select Pay_type, count(*) as amount_of_clients
from client_payment_view
group by Pay_type
go






create view Product_Product_type_view as
select cod_prod, Product_type, Prod_name, Product.Сoefficient, Waranty
from Product_type inner join Product on Product_type.Cod_type = Product.Cod_type
go

create view Product_Manufacturer_price_view as
select Product_Product_type_view.cod_prod, Product_type, Prod_name, Сoefficient, Waranty, Man_code, Price
from Product_Product_type_view inner join manufacturer_pruduction_price on Product_Product_type_view.cod_prod = manufacturer_pruduction_price.cod_prod
go

create view Product_Manufacturer_view as
select cod_prod, Product_type, Man_name, Man_Adres, Prod_name, Сoefficient, Waranty, Price
from manufacturer inner join Product_Manufacturer_price_view on manufacturer.Man_code = Product_Manufacturer_price_view.Man_code
go

create view Pc_view as
select Product_type, CPU_freq, RAM, HDD, CD, Applic, Man_name, Man_Adres, Prod_name, Сoefficient, Waranty, Price
from Product_Manufacturer_view inner join Computer on Product_Manufacturer_view.cod_prod = Computer.cod_prod
go




select *
from Pc_view
go


select *
from Product
go

select *
from Product_type
go

select *
from manufacturer
go

select *
from manufacturer_pruduction_price
go

select *
from Computer
go 





/*
	|============| 
	  ||  ||  ||   __    _    _____    _____    _____
	  ||  ||  ||   \ \  | |  |  _  |  |____ \  / ____|
	  ||  ||  ||    \ \_| |  | |_| |    __/  | | |
	  ||  ||  ||     \__  /  \  __/    / __  | | |
	  ||  ||  ||       / /    \ \__   | |__| | | |
	|============|    /_/      \___|  |______| |_|
*/












--Домашняя работа 1.11.2022--
/*
  Задание:
  Создайте хранимые процедуры и пользовательские функции различных типов(scalar, inline, multi statement) для собственного проекта.
*/



--хранимые процедуры--


--Процедуры удаления--

--Удалить пользователя по ИД--
create procedure dbo.Delete_customer_by_id(@NCustId int) as
	delete from Pasport_data where id = @NCustId;
go


declare  @NCustomerId int;
set @NCustomerId = 1002;

exec dbo.Delete_customer_by_id @NCustomerId
go




insert into Pasport_data(Id, C_Adres, C_Surname, C_Name, C_FatherName, Pay_code)
values (1002, 'ул. Николае Милеску Спэтарул 21/1', 'Безухов', 'Пьер', 'Анатольевичь', '7563549876234', 101)
go



select *
from Pasport_data
go




--Удалить все лазерные принтеры--
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


select *
from Printer
go

select *
from Product
go


--Процедуры изменения--

--Изменить имя Redmi 5 pro--
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




--*Изменить цену товара Nitro (изменить сам коэфициент)--
--!!!!Цены у товара конкретной нет, поэтому для того, чтобы её изменить, надо изменить сам коэфициент, или надо, чтобы изменилась чена на сам продукт у производителя--
create procedure dbo.Change_product_price(@CProductName varchar(20), @CNewProductCoefficient decimal(3,1)) as
	update Product
	set Сoefficient = @CNewProductCoefficient
	where Prod_name = @CProductName
	go
go


declare @COldProduct varchar(20), @CNewProductCoef decimal(3,1);
set @COldProduct = 'Nitro';
set @CNewProductCoef = 3.0;


exec dbo.Change_product_price @COldProduct, @CNewProductCoef
go




update Product
set Сoefficient = 1.5
where cod_prod = 5010
go

select *
from Product
go



--или (у самого производителя)--
create procedure dbo.Change_product_price_for_manufacturer(@CProductName varchar(20), @CNewProductPrice money) as
	update manufacturer_pruduction_price
	set Price = @CNewProductPrice
	where cod_prod = (select cod_prod
					  from Product
					  where Prod_name = @CProductName)
	go
go

declare @COldProduct varchar(20), @CNewProductPrice money;
set @COldProduct = 'Nitro';
set @CNewProductPrice = 30000;


exec dbo.Change_product_price_for_manufacturer @COldProduct, @CNewProductPrice
go


exec dbo.Show_all
go


--Изменить имя производителя HP--
create procedure dbo.Change_manufacturer_name(@COldManufacturerName varchar(20), @CNewManufacturerName varchar(20)) as
	update manufacturer
	set Man_name = @CNewManufacturerName
	where Man_name = @COldManufacturerName
go


declare @COldMan varchar(20), @CNewMan varchar(20);
set @COldMan = 'HP';
set @CNewMan = 'Philips';


exec dbo.Change_manufacturer_name @COldMan, @CNewMan
go

update manufacturer
set Man_name = 'HP'
where Man_code = 4004
go



select *
from manufacturer
go


--Процедуры добавления--

--Добавить нового поставщика--
create procedure dbo.Add_new_supplier(@CNewSupName varchar(20), @CNewSupAddress Adres, @CNewSupTel TNumber, @CManName varchar(20), @NsupProdType varchar(20)) as
	
	if(@CManName = (select Man_name
					from manufacturer
					where Man_name = @CManName
					))
	begin
		
		if(@NsupProdType = (select Product_type
							from Product_type
							where Product_type = @NsupProdType
							))
		begin
			declare @NNewSupCode int, @NMan_cod int, @NProdCodType int;

			set @NNewSupCode = (select max(Sup_code)
								from supplier) + 1;
			set @NMan_cod = (select Man_code
							from manufacturer
							where Man_name = @CManName)
			set @NProdCodType = (select Cod_type
								 from Product_type
								 where Product_type = @NsupProdType
								 )

		
			insert into supplier
			values (@NNewSupCode, @CNewSupName, @CNewSupAddress, @CNewSupTel, @NMan_cod);

			insert into supplier_products
			values (@NNewSupCode, @NProdCodType);
					   			 
		end
		else
		begin
			print 'Sorry, such product type was not found!'
		end
	end
	else
	begin
		print 'Sorry, such manufacturer was not found!';
	end

go

declare @SupName varchar(20), @SupAddress Adres, @SupTelephone TNumber, @ManName varchar(20), @SupProdType varchar(20);

set @SupName = 'Test';
set @SupAddress = 'Test address';
set @SupTelephone = '+37300000000';
--set @ManName = 'Test';
set @ManName = 'Xiaomi';
--set @SupProdType = 'Test';
set @SupProdType = 'Laptop';

exec dbo.Add_new_supplier @SupName, @SupAddress, @SupTelephone, @ManName, @SupProdType
go




select *
from supplier
go


select *
from supplier_products
go

select *
from Product_type
go

select *
from manufacturer
go

--%%Добавить новый продукт%%--
--помимо добавления самого продукта, мы будем ещё и добавлять его в журнал поставки и цену в manufacturer_production_price--
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





select *
from Product
go

select *
from Product_type
go

select *
from manufacturer
go


select *
from manufacturer_pruduction_price
go

select *
from supplier
go

select *
from supplier_products
go

select *
from consignment
go







--Процедура вывода информации--

--Вывести информацию о покупателе Балконский Андрей Николаевич--
create procedure dbo.Show_customer_info(@CustomerSurname varchar(12), @CustomerName varchar(12), @CustomerFatherName varchar(12)) as
	select id, C_Adres as [Adres], C_Surname as [Surname], C_Name as [Name], C_FatherName as [Father name], IDNP, Pay_type as [Payment type]
	from Pasport_data inner join Payment on Pasport_data.Pay_code = Payment.Pay_code
	where C_Surname = @CustomerSurname and C_Name = @CustomerName and C_FatherName = @CustomerFatherName
go


declare  @CCSurname varchar(12),@CCName varchar(12), @CCFatherName varchar(12);
set @CCSurname = 'Балконский';
set @CCName = 'Андрей';
set @CCFatherName = 'Николаевич';

exec dbo.Show_customer_info @CCSurname, @CCName, @CCFatherName
go



select *
from Pasport_data









--пользовательские функции--

--scalar--

--Вывести ИД пользователя по его имени, фамилии, отчеству, и IDNP (так как ФИО может повторяться)--
create function dbo.Get_Customer_id(@CustomerSurname varchar(12), @CustomerName varchar(12), @CustomerFatherName varchar(12), @CustomerIDNP varchar(13))
returns int as
begin
	
	declare @CustomerID int;

	set @CustomerID = (select id
					   from Pasport_data
					   where C_Surname = @CustomerSurname and C_Name = @CustomerName and C_FatherName = @CustomerFatherName and IDNP = @CustomerIDNP);

	return @CustomerID;
end
go


declare @CCSurname varchar(12), @CCName varchar(12), @CCFatherName varchar(12), @CCIDNP varchar(13);
set @CCSurname = 'Болякин';
set @CCName = 'Андрей';
set @CCFatherName = 'Никифорович';
set @CCIDNP = '123454554321';

select dbo.Get_Customer_id(@CCSurname, @CCName, @CCFatherName, @CCIDNP) as [Customer's id]
go

/*or
print dbo.Get_Customer_id(@CCSurname, @CCName, @CCFatherName, @CCIDNP)
go
*/

select *
from Pasport_data
go


--Вывести цену товара All Vew Pro--
--Цена, по идее, расчитывается так: Цена товара производителя + Цена товара производителя x коэфициент поставки (поставщика) + Цена товара производителя х коэфициент товара--
create function dbo.Get_product_price(@ProductName varchar (20))
returns money as
begin
	
	declare @ProductID int, @ProductPrice money, @ConsignmentCoef decimal(3,1), @ProductCoef decimal(3,1), @ProductTotalPrice money;
	set @ProductID = (select cod_prod
					  from Product
					  where Prod_name = @ProductName);

	set @ProductPrice = (select Price
						 from manufacturer_pruduction_price
						 where cod_prod = @ProductID);

	set @ConsignmentCoef = (select Coefficient
							from consignment
							where cod_prod = @ProductID);

	set @ProductCoef = (select Product.Сoefficient
						from Product
						where cod_prod = @ProductID);
	
	set @ProductTotalPrice = @ProductPrice + @ProductPrice * @ConsignmentCoef + @ProductPrice * @ProductCoef;

	return @ProductTotalPrice;
end
go



declare @CProdName varchar(20);
set @CProdName = 'All Vew Pro';

select dbo.Get_product_price(@CProdName) as [Product price]
go




select *
from Product
go

select *
from consignment
go

select *
from manufacturer_pruduction_price
go




--inline--

--Вывести всю продукцию производителя Xiaomi--
create function dbo.Show_manufacturers_products(@ManufacturerName varchar(20))
returns table as
	return select cod_prod, Product_type, Prod_name, Product.Сoefficient, Waranty
		   from Product inner join Product_type on Product.Cod_type = Product_type.Cod_type
		   where cod_prod in(select cod_prod
							 from manufacturer_pruduction_price
							 where Man_code = (select Man_code
											   from manufacturer
											   where Man_name = @ManufacturerName
											   )
							 )
go



declare @CManName varchar(20);
set @CManName = 'Xiaomi';

select *
from dbo.Show_manufacturers_products(@CManName)
go


select *
from Product
go

select *
from Product_type
go

select *
from manufacturer_pruduction_price
go

select *
from manufacturer
go


--Вывести информацию о поставщике Hyper Supply--
create view supplier_products_view as
select Sup_name, Sup_Adres, Telephone, Man_cod, Cod_type
from supplier inner join supplier_products on supplier.Sup_code = supplier_products.Sup_code
go

select *
from supplier_products_view
go


create view supplier_view as
select Sup_name, Sup_Adres, Telephone, Man_cod, Product_type
from supplier_products_view inner join Product_type on supplier_products_view.Cod_type = Product_type.Cod_type
go

select *
from supplier_view
go






create function dbo.Get_info_about_suplier(@SupName varchar(20))
returns table as
	return select Sup_name as [Supplier], Sup_Adres as [Supplier adres], Telephone, Man_name as [Manufacturer], Product_type [Supplied production type]
		   from supplier_view inner join manufacturer on supplier_view.Man_cod = manufacturer.Man_code
		   where Sup_name = @SupName
go


declare @CSupName varchar(20);
set @CSupName = 'Hyper Supply';

select *
from dbo.Get_info_about_suplier(@CSupName)
go


select *
from supplier
go

select *
from supplier_products
go

select *
from manufacturer
go




--multi statement--

--Вывести информацию о всех Компьютерах (PC или Laptop)--
create function dbo.Show_info_apout_computers(@CompType varchar(20))
returns @Computers table (Prod_name varchar(20), CPU_freq smallint, RAM smallint, HDD smallint, CD varchar(5), Applic applicationS, Watanty smallint, Man_name varchar(20))
begin
	
	insert into @Computers
	select Prod_name, CPU_freq, RAM, HDD, CD, Applic, Waranty, Man_name
	from Product_Manufacturer_view inner join Computer on Product_Manufacturer_view.cod_prod = Computer.cod_prod --так находим все компьютеры--
	where Product_type = @CompType --так отбираем только ноутбуки--

	
	return;
end
go




declare @CComputerType varchar(20);
set @CComputerType = 'Laptop';

select *
from dbo.Show_info_apout_computers(@CComputerType)
go





select *
from Computer
go

select *
from Product
go

select *
from Product_Manufacturer_view
go



--Вывести, какой товар, каким поставщиком и какого производителя был поставлен 2022-01-09--


create view Consignment_view as
select Sup_code, Product_type, Man_name, Man_Adres, Prod_name, Product_Manufacturer_view.Сoefficient, Waranty, Price, prod_number, consignment_number, Consignment_date, consignment.Coefficient
from Product_Manufacturer_view inner join consignment on Product_Manufacturer_view.cod_prod = consignment.cod_prod
go




create function dbo.Get_info_about_consignment(@Year int, @Month int, @Day int)
returns @ConsignmentOnDate table (consignment_number int, Prod_type varchar(20), Prod_name varchar(20), Price money, prod_number smallint, Sup_name varchar(20), Telephone TNumber, Man_name varchar(20))
begin 
	

	insert into @ConsignmentOnDate
	select consignment_number, Product_type, Prod_name, Price, prod_number, Sup_name, Telephone, Man_name
	from Consignment_view inner join supplier on Consignment_view.Sup_code = supplier.Sup_code
	where Consignment_date = cast((cast(@Year as varchar(4)) + '-' + cast(@Month as varchar(2)) + '-' + cast(@Day as varchar(2))) as date);


	/*
	declare @TempTableConsignmentProduct table (Sup_code int, consignment_number int, Prod_type varchar(20), Prod_name varchar(20), Price money, prod_number smallint, Man_name varchar(20)) --Создадим временную таблицу через переменную--
	
	insert into @TempTableConsignmentProduct
	select Sup_code, consignment_number, Product_type, Prod_name, Price, prod_number, Man_name
	from Product_Manufacturer_view inner join consignment on Product_Manufacturer_view.cod_prod = consignment.cod_prod
	where Consignment_date = cast((cast(@Year as varchar(4)) + '-' + cast(@Month as varchar(2)) + '-' + cast(@Day as varchar(2))) as date);

	insert into @ConsignmentOnDate
	select consignment_number, Prod_type, Prod_name, Price, prod_number, Sup_name, Telephone, Man_name
	from @TempTableConsignmentProduct inner join supplier on @TempTableConsignmentProduct.Sup_code = supplier.Sup_code;
	*/

	return;
end
go


declare @NYear int, @NMonth int, @NDay int;
set @NYear = 2022;
set @NMonth = 01;
set @NDay = 09;


select *
from dbo.Get_info_about_consignment(@NYear, @NMonth, @NDay)
go




select *
from consignment
go

select *
from Product_Manufacturer_view
go

select *
from Consignment_view
go

select *
from supplier
go





/*
converting process:

declare @t1 char(4) = cast(2022 as char(4))

declare @t2 varchar(10) = cast(2022 as varchar(4)) + '-' + cast(01 as varchar(2)) + '-' + cast(09 as varchar(2))

declare @t3 date = cast(@t2 as date)


print @t2
print @t3
*/



exec dbo.Show_all
go











--how does the group by works!!!--

select * from consignment
go

select sum(Price) from consignment
go

select sum(Price) from consignment group by Man_code
go

-----------------------------
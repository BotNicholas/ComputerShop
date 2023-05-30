use Computer_magazine
go

--Transactions--

--automated transactions--

/*
update Pasport_data
set Pay_code = 102
where Id = 1005
go


select *
from Pasport_data
go


create table supplier(Sup_code int primary key,
					  Sup_name varchar(20),
					  Company varchar(20),
					  Sup_Adres Adres)
go


alter table supplier
add Telephone TNumber
go


insert into supplier(Sup_code, Sup_name, Man_cod, Sup_Adres, Telephone)
values  (3001, 'Comp_Moldova', 4003, 'ул. Штефан чел маре 21/5', '+37367354672'),
		(3002, 'Hyper Supply', 4005, 'ул. Гоголя 34/4', '+37369984519'),
		(3003, 'GMSupply', 4006, 'ул. Колумна 5','+37378534765'),
		(3004, 'TechMD', 4002, 'ул. Узинелор 7','+37379945543'),
		(3005, 'XMoldova', 4001, 'ул. Петру Мовилэ 7','+37360606607'),
		(3006, 'Hyper Supply', 4004, 'ул. Михаил Садовяну 3','+37369984519')
go
*/







--nonautomated transactions--

--implicit transactions--

set implicit_transactions on
go


--Deleting data about ClientS and after that rollback--

--transaction begin--
select *
from Pasport_data;

delete from Pasport_data;

select *
from Pasport_data;

rollback transaction
go
--transaction end--

select *
from Pasport_data
go


set implicit_transactions off
go




--Changing product's (Blue Yeti's) manufacturer (from Hyper PC to LG) (from manufacturer_pruduction_price) => changing product's manufacturer in consignment journal (consignment)!--

set implicit_transactions on
go


declare @ProductName varchar(20), @ProductCode int;

set @ProductName = 'Blue Yeti';
set @ProductCode = (select cod_prod
					from Product
					where Prod_name = @ProductName);


declare @NewManufacturerName varchar(20), @NewManufacturerCode int;

set @NewManufacturerName = 'LG';
set @NewManufacturerCode = (select Man_code
							from manufacturer
							where Man_name = @NewManufacturerName);

--transaction begin--

update manufacturer_pruduction_price
set Man_code = @NewManufacturerCode
where cod_prod = @ProductCode;

update consignment
set Man_code = @NewManufacturerCode
where cod_prod = @ProductCode
go

commit transaction;
--transaction end--

set implicit_transactions off
go



select *
from manufacturer_pruduction_price
go

select *
from Product
go

select *
from manufacturer
go

select *
from consignment
go







--Explicit + distributed transactions--

--withdraw money from a banking card for user Болякин Андрей Никифорович (Boliakin Andrei Nikiforovici)--
--client searching will be released by IDNP searching--

select *
from Pasport_data
go

select *
from Payment
go



select *
from Banking.dbo.Credit_card
go

begin distributed transaction
declare @ClientCode int, @IDNP char(13), @TotalToPay smallint;

set @ClientCode = (select Id
				   from Pasport_data
				   where C_Name = 'Андрей' and C_Surname = 'Болякин' and C_FatherName = 'Никифорович')

set @IDNP = (select IDNP
			 from Pasport_data
			 where Id = @ClientCode); --client's IDNP in our magazine--

set @TotalToPay = (select Gen_price
				   from Chek_infoChek
				   where cod_client = @ClientCode)

set @ClientCode = (select cod_client			--searching the client by his IDNP
				   from Banking.dbo.Client
				   where IDNP = @IDNP);

print 'Total to pay: ' + cast(@TotalToPay as varchar(20));

if(@TotalToPay <= (select money_amount from Banking.dbo.Credit_card where cod_client = @ClientCode))
begin
	update Banking.dbo.Credit_card
	set money_amount = money_amount - @TotalToPay
	where cod_client = @ClientCode;

	print 'Success!';
	print 'Transaction is successfull!'
	commit transaction;
end
else
begin
	print 'Ooops!';
	print 'You do not have enough money!';
	rollback transaction;
end;
go

select *
from Banking.dbo.Credit_card
go

select *
from Banking.dbo.Client
go



--Creating a credit contract for client Безухов	Пьер Анатольевичь--


update Chek
set Pay_code = 101
where Check_code = 1014
go


begin distributed transaction
declare @ContractNumber int, @BankCode int, @OpeningDate date, @Term int, @CreditSize money, @ClientCode int, @ClientFIO varchar(70);

set @ClientFIO = 'Безухов Пьер Анатольевичь';

set @ContractNumber = (select max(contract_number)
					   from Banking.dbo.Credit_contract) + 1;

set @BankCode = 101; --MAIP, for example--

set @OpeningDate = getDate();

set @Term = 24; --2 years, for example--


--in the shop--
set @ClientCode = (select Id
				   from Pasport_data
				   where C_Surname = 'Безухов' and C_Name = 'Пьер' and C_FatherName = 'Анатольевичь')

set @CreditSize = (select Gen_price
				   from Chek_infoChek
				   where cod_client = @ClientCode);


--in bank--
set @ClientCode = (select cod_client
				   from Banking.dbo.Client
				   where fio = @ClientFIO);

if(@ClientCode is null)
begin
print 'Inserting new client!';
set @ClientCode = (select max(cod_client) from Banking.dbo.Client)+1;

insert into Banking.dbo.Client
values (@ClientCode, @ClientFIO, 'ул. Николае Милеску Спэтарул 21/1', 40, '1234567890123', '+37355555555', 0, 6000, 'BezPierAn');
end


insert into Banking.dbo.Credit_contract(contract_number, cod_bank, cod_client, opening_date, term, credit_size)
values(@ContractNumber, @BankCode, @ClientCode, @OpeningDate, @Term, @CreditSize);

commit transaction
go



select *
from Banking.dbo.Credit_contract
go


select *
from Pasport_data
go

select *
from Chek
go

select *
from Chek_infoChek
go

select *
from Payment
go

--Later we can put these last two fransactions in a procedure and use them multypli times in future--









--Triggers--

--create trigger for Check_infoCheck with counting Gen_price (created in main file with tables)--

/*
create or alter trigger check_info_ins
on Chek_infoChek
instead of insert
as
	declare @GenPrice money, @CheckCode int, @ClientCode int, @PurcDate date;

	set @CheckCode = (select Check_code
					  from inserted);

	set @ClientCode = (select cod_client
					   from inserted);

	set @PurcDate = (select Purc_date
					 from inserted);

	/*
	set @ProductCode = (select distinct cod_prod
					    from Chek_products
					    where Check_code = (select Check_code
										    from inserted));
	
	print @ProductCode

	*/

	/*
	select dbo.get_product_price(cod_prod)
	from Chek_products
	where Check_code = (select Check_code
						from inserted);
	*/

	set @GenPrice = (select sum(dbo.get_product_price(cod_prod))
					 from Chek_products
					 where Check_code = (select Check_code
										 from inserted));

	
	insert into Chek_infoChek
	values (@CheckCode, @ClientCode, @PurcDate, @GenPrice);
go
*/




--LOG trigger for table Pasport_data--

--on insert--
create or alter trigger pasport_insert
on Pasport_data
after insert
as
	declare @CName varchar(12), @CSurname varchar(12), @CFatherName varchar(12), @IDNP char(13);

	set @CName = (select C_Name 
				  from inserted);

	set @CSurname = (select C_Surname 
					 from inserted);

	set @CFatherName = (select C_FatherName 
						from inserted);

	set @IDNP = (select IDNP 
				 from inserted);


	print 'User - ' + current_user + ' on - ' + cast(getDate() as varchar(20)) + ' inserted new client: ';
	print '';
	print 'Client surname: ' + @CSurname;

	print 'Client name: ' + @CName;

	print 'Client father name: ' + @CFatherName;

	print 'Client IDNP: ' + @IDNP;
go

--on update--
create trigger pasport_update
on Pasport_data
after update
as
	declare @CName varchar(12), @CSurname varchar(12), @CFatherName varchar(12), @IDNP char(13);

	set @CName = (select C_Name 
				  from deleted);

	set @CSurname = (select C_Surname 
					 from deleted);

	set @CFatherName = (select C_FatherName 
						from deleted);

	set @IDNP = (select IDNP 
				 from deleted);


	declare @NewCName varchar(12), @NewCSurname varchar(12), @NewCFatherName varchar(12), @NewIDNP char(13);

	set @NewCName = (select C_Name 
				  from inserted);

	set @NewCSurname = (select C_Surname 
					 from inserted);

	set @NewCFatherName = (select C_FatherName 
						from inserted);

	set @NewIDNP = (select IDNP 
				 from inserted);




	print 'User - ' + current_user + ' on - ' + cast(getDate() as varchar(20)) + ' updated client: ';
	print '';
	print 'Client surname: ' + @CSurname + ' --> ' + @NewCSurname;

	print 'Client name: ' + @CName + ' --> ' + @NewCName;

	print 'Client father name: ' + @CFatherName + ' --> ' + @NewCFatherName;

	print 'Client IDNP: ' + @IDNP + ' --> ' + @NewIDNP;
go

--on delete--
create trigger pasport_delete
on Pasport_data
after delete
as
	declare @CName varchar(12), @CSurname varchar(12), @CFatherName varchar(12), @IDNP char(13);

	set @CName = (select C_Name 
				  from deleted);

	set @CSurname = (select C_Surname 
					 from deleted);

	set @CFatherName = (select C_FatherName 
						from deleted);

	set @IDNP = (select IDNP 
				 from deleted);


	print 'User - ' + current_user + ' on - ' + cast(getDate() as varchar(20)) + ' deleted client: ';
	print '';
	print 'Client surname: ' + @CSurname;

	print 'Client name: ' + @CName;

	print 'Client father name: ' + @CFatherName;

	print 'Client IDNP: ' + @IDNP;
go



insert into Pasport_data
values(1006, 'Test adres', 'Test surname', 'Test name', 'Test FN', '0000000000000')
go

select *
from Pasport_data
go

update Pasport_data
set C_Name = 'TEST NAME'
where Id = 1006
go

select *
from Pasport_data
go

delete from Pasport_data
go

select *
from Pasport_data
go




--Checking if user is the database owner, and if he is delete an supplier Test--

insert into supplier
values (3007, 'Test', 'ул. Михаил Садовяну 3', '+37369984519', 4004)
go

insert into consignment(Sup_code, cod_prod, prod_number, Man_code, consignment_number, Consignment_date, Coefficient)
values (3007, 5000, 5, 4004, 10500, '2021-12-21', 0.25)
go

insert into supplier_products
values (3007, 2000)
go


create or alter trigger sup_del
on supplier
instead of delete
as
	if(current_user <> 'dbo')
	begin
		print 'You are not the database owner!'
	end
	else
	begin
		declare @SupplierCode int;
		set @SupplierCode = (select Sup_code
							 from supplier
							 where Sup_name = 'Test');

		--first of all lets delete all the dependences!--
		delete from supplier_products
		where Sup_code = @SupplierCode;

		delete from consignment
		where Sup_code = @SupplierCode;


		--and from the supplier table--
		delete from supplier
		where Sup_code = @SupplierCode;
	end
go

select *
from supplier
go



delete from supplier
where Sup_name = 'Test'
go



select *
from supplier
go

select *
from consignment
go

select *
from supplier_products
go

select *
from Product_type
go




--When buying a product, reduce its quantity in the delivery log. Trigger fo Chek--
create or alter trigger chek_inssert
on Chek
instead of insert
as
	declare @Chek_code int, @PayCode int,
			@ProdName varchar(20), @ProdCod int, @ProdCount int,
			@ClientCode int, @PurcDate date;

	set @Chek_code = (select Check_code
					  from inserted);

	set @PayCode = (select pay_code
					from inserted);

	set @ProdName = 'Blue Yeti';

	set @ProdCod = (select cod_prod
					from Product
					where Prod_name = @ProdName);

	set @ProdCount = (select prod_number
					  from consignment
					  where cod_prod =@ProdCod);

	set @ClientCode = 1005;

	set @PurcDate = '2021-12-31';

	if(@ProdCount > 0)
	begin

	insert into Chek
	values (@Chek_code, @PayCode);

	insert into Chek_products
	values (@Chek_code, @ProdCod);

	insert into Chek_infoChek(Check_code, cod_client, Purc_date)
	values (@Chek_code, @ClientCode, @PurcDate);

	update consignment
	set prod_number = prod_number - 1
	where cod_prod = @ProdCod;

	end
	else
	begin

	print 'This product has already been sold!!!';

	end;
go



select *
from Chek
go

select *
from Chek_products
go

select *
from Chek_infoChek
go

select *
from consignment
go




insert into Chek
values ((select max(Check_code) from Chek)+1, 102)
go


select *
from Chek
go

select *
from Chek_products
go

select *
from Chek_infoChek
go

select *
from consignment
go



select *
from Product
go




select *
from Pasport_data
go

select *
from Payment
go







--Trigger for the supply of goods. If the product is already available, make a new entry in which, add the quantity of the already existing quantity of the product with the new quantity.--
--we will change entire record. To do this we have to refresh only consignment date..--

create or alter trigger cons_insert
on consignment
instead of insert
as
	declare @ProdAmount int, @Consignment_date date, @SupCode int, @ProdCod int;

	
	set @SupCode = (select Sup_code
					from inserted);
					
	set @ProdCod = (select cod_prod
					from inserted);
	
	--searching what has left--
	set @ProdAmount = (select prod_number
					   from consignment
					   where cod_prod = @ProdCod and Sup_code = @SupCode); --becuse many suppliers can supply the same product from one manufacturer--

	set @ProdAmount = @ProdAmount + (select prod_number	
									 from inserted);
	
	set @Consignment_date = getDate();

	update consignment
	set prod_number = @ProdAmount, Consignment_date = @Consignment_date
	where cod_prod = @ProdCod and Sup_code = @SupCode;
go



select *
from consignment
go

insert into consignment
values (3002, 5000, 1, 4005, 10456, '2021-12-21', 0.25) --product supply identifies by product id and supplier id--
go




--If the user is the owner of the database, cascade delete the product. Inside the trigger - transaction--

create or alter trigger prod_del
on Product
instead of delete
as
	--First we have to delete all the dependencies--
	declare @ProdType varchar(20), @ProdCode int;

	set @ProdType = (select Product_type
					 from Product_type
					 where Cod_type = (select Cod_type
									   from deleted));

	set @ProdCode = (select cod_prod
					 from deleted);


	begin transaction
	if(@ProdType = 'PC' or @ProdType = 'Laptop')
	begin
		delete from Computer
		where cod_prod = @ProdCode;
	end
	else
	begin
		if(@ProdType = 'Monitor')
		begin
			delete from Monitor
			where cod_prod = @ProdCode;
		end
		else
		begin
			if(@ProdType= 'Phone' or @ProdType = 'Tablet PC')
			begin
				delete from Phone
				where cod_prod = @ProdCode;
			end
			else
			begin
				if(@ProdType = 'Printer')
				begin
					delete from Printer
					where cod_prod = @ProdCode;
				end
			end
		end
	end

	delete from manufacturer_pruduction_price
	where cod_prod = @ProdCode;

	delete from Chek_products
	where cod_prod = @ProdCode;

	delete from consignment
	where cod_prod = @ProdCode;



	--deleting from Product--
	delete from Product
	where cod_prod = @ProdCode;

	commit transaction;
go


select *
from Product_type
go

select *
from Product
go

select *
from Computer
go

select *
from Monitor
go

select *
from Phone
go

select *
from Printer
go

select *
from manufacturer_pruduction_price
go

select *
from Chek_products
go

select *
from consignment
go




delete from Product
where Prod_name = 'Blue Yeti'
go

delete from Product
where Prod_name = 'Nitro'
go

delete from Product
where Prod_name = 'Spin 5'
go

delete from Product
where Prod_name = 'Redmi 5 pro'
go

delete from Product
where Prod_name = 'Prestige Pro 3'
go

delete from Product
where Prod_name = 'Colors'
go



select *
from Product_type
go

select *
from Product
go

select *
from Computer
go

select *
from Monitor
go

select *
from Phone
go

select *
from Printer
go

select *
from manufacturer_pruduction_price
go

select *
from Chek_products
go

select *
from consignment
go


--Backups--

/*
We will make backups for next databases:
master
Computer_magazine
and	Banking

on drive C:\sql\'DBName'.bak
(first of all you have to create the 'sql' folder on your C: drive!)

of course it is not recommended to store backups on the system's drive, but this drive is 100% exists on any PC, so thus, I make this only for demonstration...
*/

backup database master
to disk = 'C:\sql\master.bak'
go

backup database Computer_magazine
to disk = 'C:\sql\Computer_magazine.bak'
go

backup database Banking
to disk = 'C:\sql\Banking.bak'
go



restore database Computer_magazine from disk = 'D:\Student\old\2023.5.13_Backup.bak'
go
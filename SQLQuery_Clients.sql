create table Clients
(
	Id int not null IDENTITY(1, 1),
	Client_surname nvarchar(50) not null,
	Client_name nvarchar(50) not null,	
	Client_patronymic nvarchar(50) not null,
	Phone_number nvarchar(50) null,
	Email nvarchar(50) not null
)

insert into Clients(Client_surname,Client_name,Client_patronymic,Phone_number,Email) 
	values(N'Пономарёв',N'Иван',N'Валерьянович','+79515644485','valera99@gmail.com')
insert into Clients(Client_surname,Client_name,Client_patronymic,Phone_number,Email) 
	values(N'Звездаков',N'Пётр',N'Филатович','+79516485168','petya111@gmail.com')
--CREATE DATABASE DB1;
--USE DB1;

CREATE TABLE users1(
	id INT PRIMARY KEY IDENTITY(1,1),
	firstname VARCHAR(50),
	lastname VARCHAR(50),
	user_name VARCHAR(25),
	pc_name VARCHAR(50),
	last_login DATETIME,
	login_status TINYINT,
	user_status TINYINT,
)

--CREATE TABLE



alter table todoList
add constraint FK_todoList_user foreign key  (user_id) references users (id)
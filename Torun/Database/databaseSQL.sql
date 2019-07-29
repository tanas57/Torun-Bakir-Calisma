--CREATE DATABASE PLAN_TRACERDB;

-- users table
CREATE TABLE Users(
	id INT PRIMARY KEY IDENTITY(1,1),
	firstname VARCHAR(50) NOT NULL,
	lastname VARCHAR(50) NOT NULL,
	user_name VARCHAR(25) NOT NULL,
	password VARCHAR(100) NOT NULL,
	pc_name VARCHAR(50) NOT NULL,
	last_login DATETIME NOT NULL DEFAULT NULL,
	login_status TINYINT NOT NULL DEFAULT 0, -- 1: on, 2 off 
	user_status TINYINT NOT NULL DEFAULT 1,  -- 1: active, 2: disactive 3: banned
	register_date DATETIME NOT NULL
);
-- to do list table
CREATE TABLE TodoList(
	id INT PRIMARY KEY IDENTITY(1,1),
	request_number VARCHAR(100) NOT NULL,
	priority TINYINT NOT NULL DEFAULT 2, -- 1: high, 2: normal, 3: low
	description TEXT,
	user_id INT NOT NULL, -- which user is
	add_time DATETIME NOT NULL,
	status TINYINT NOT NULL DEFAULT 1 -- 1: added, 2: updated, 3: deleted
);

alter table TodoList
add constraint FK_TodoList_Users 
foreign key  (user_id) references Users (id);

-- plan table

CREATE TABLE Plans(
	id INT PRIMARY KEY IDENTITY(1,1),
	work_id INT NOT NULL,
	add_time DATETIME NOT NULL,
	work_plan_time DATETIME NOT NULL
);

alter table Plans
add constraint FK_plan_TodoList 
foreign key  (work_id) references TodoList (id);

-- work done table

CREATE TABLE WorkDone(
	id INT PRIMARY KEY IDENTITY(1,1),
	plan_id INT NOT NULL,
	workDoneTime DATETIME NOT NULL,
	add_time DATETIME NOT NULL,
	description TEXT,
	status TINYINT NOT NULL
);

alter table WorkDone
add constraint FK_WorkDone_TodoList 
foreign key  (plan_id) references Plans(id);

insert into Users (firstname, lastname, user_name, password, pc_name, last_login, login_status, user_status, register_date) values ('M.Tayyip', 'Muslu', 'tanas57', '68053af2923e00204c3ca7c6a3150cf7', 'MusluNET', '2019-07-08', 0, 1,'2019-07-08');

insert into TodoList(request_number, priority, description, user_id, add_time, status)
values('2900', 2, 'Talep açıklaması gelecek', 1, '2019-07-07', 1);

insert into TodoList(request_number, priority, description, user_id, add_time, status)
values('2905', 2, 'Talep açıklaması gelecek', 1, '2019-07-29', 1);
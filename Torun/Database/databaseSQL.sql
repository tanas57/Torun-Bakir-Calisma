--CREATE DATABASE PLAN_TRACERDB;

-- users table
CREATE TABLE users(
	id INT PRIMARY KEY IDENTITY(1,1),
	firstname VARCHAR(50) NOT NULL,
	lastname VARCHAR(50) NOT NULL,
	user_name VARCHAR(25) NOT NULL,
	password VARCHAR(100) NOT NULL,
	pc_name VARCHAR(50) NOT NULL,
	last_login DATETIME NOT NULL DEFAULT NULL,
	login_status TINYINT NOT NULL DEFAULT 0, -- 1: on, 2 off 
	user_status TINYINT NOT NULL DEFAULT 1,  -- 1: active, 2: disactive 3: banned
	register_data DATETIME NOT NULL
);
-- to do list table
CREATE TABLE todoList(
	id INT PRIMARY KEY IDENTITY(1,1),
	request_number VARCHAR(100) DEFAULT NULL,
	priority TINYINT NOT NULL DEFAULT 2, -- 1: high, 2: normal, 3: low
	description TEXT,
	user_id INT NOT NULL, -- which user is
	add_time DATETIME NOT NULL,
	status TINYINT NOT NULL DEFAULT 0 -- 1: added, 2: updated, 3: deleted
);

alter table todoList
add constraint FK_todoList_user 
foreign key  (user_id) references users (id);

-- plan table

CREATE TABLE plans(
	id INT PRIMARY KEY IDENTITY(1,1),
	work_id INT NOT NULL,
	add_time DATETIME NOT NULL,
	work_plan_time DATETIME NOT NULL
);

alter table plans
add constraint FK_plan_todoList 
foreign key  (work_id) references todoList (id);

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
add constraint FK_WorkDone_todoList 
foreign key  (plan_id) references plans(id);

insert into users (firstname, lastname, user_name, pc_name, last_login, login_status, user_status) values ('M.Tayyip', 'Muslu', 'tanas57', 'MusluNET', '2019-07-08', 0, 1,'202cb962ac59075b964b07152d234b70', '2019-07-08');

insert into todoList(request_number, priority, user_id, description, add_time, status)
values('2900', 2, 1, 'Talep açıklaması gelecek', '2019-07-07', 1);
insert into todoList(request_number, priority, user_id, description, add_time, status)
values(NULL, 3, 1, 'Talep açıklaması gelecek', '2019-07-07', 1);
insert into todoList(request_number, priority, user_id, description, add_time, status)
values('2900', 1, 1, 'Talep açıklaması gelecek', '2019-07-07', 1);
--CREATE DATABASE DB1;
--USE DB1;

-- users table
CREATE TABLE users(
	id INT PRIMARY KEY IDENTITY(1,1),
	firstname VARCHAR(50),
	lastname VARCHAR(50),
	user_name VARCHAR(25),
	pc_name VARCHAR(50),
	last_login DATETIME,
	login_status TINYINT, -- 1: on, 2 off 
	user_status TINYINT,  -- 1: active, 2: disactive 3: banned
);
-- to do list table
CREATE TABLE todoList(
	id INT PRIMARY KEY IDENTITY(1,1),
	request_number VARCHAR(15),
	priority TINYINT, -- 1: high, 2: normal, 3: low
	description TEXT,
	user_id INT, -- which user is
	add_time DATETIME,
	status TINYINT -- 1: added, 2: updated, 3: deleted
);

alter table todoList
add constraint FK_todoList_user 
foreign key  (user_id) references users (id);

-- plan table

CREATE TABLE plan(
	id INT PRIMARY KEY IDENTITY(1,1),
	work_id INT,
	add_time DATETIME,
	work_plan_time DATETIME
);

alter table plan
add constraint FK_plan_todoList 
foreign key  (work_id) references todoList (id);

-- work done table

CREATE TABLE WorkDone(
	id INT PRIMARY KEY IDENTITY(1,1),
	work_id INT,
	workDoneTime DATETIME,
	add_time DATETIME,
	description TEXT,
	status TINYINT
);

alter table WorkDone
add constraint FK_WorkDone_todoList 
foreign key  (work_id) references todoList(id);


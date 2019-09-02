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
	user_permission TINYINT NOT NULL DEFAULT 1, -- 1 : standard user, 2 : admin
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
	work_plan_time DATE NOT NULL,
	status TINYINT NOT NULL DEFAULT 0
);

alter table Plans
add constraint FK_plan_TodoList 
foreign key  (work_id) references TodoList (id);

-- work done table

CREATE TABLE WorkDone(
	id INT PRIMARY KEY IDENTITY(1,1),
	plan_id INT NOT NULL,
	workDoneTime DATE,
	add_time DATETIME NOT NULL,
	description TEXT,
	status TINYINT NOT NULL
);

alter table WorkDone
add constraint FK_WorkDone_TodoList 
foreign key  (plan_id) references Plans(id);

CREATE TABLE Log(
	id INT IDENTITY(1,1) PRIMARY KEY,
	log_user INT NOT NULL,
	error_page VARCHAR(50),
	error_text TEXT,
	log_date DATETIME NOT NULL
);

alter table Log
add constraint FK_Log_Users
foreign key  (log_user) references Users(id);

CREATE TABLE Settings(
	id INT IDENTITY(1,1) PRIMARY KEY,
	user_id INT NOT NULL,
	set_countType TINYINT NOT NULL DEFAULT 4, -- 4 : FROM THE BEGINNING
	set_autoOpen BIT NOT NULL DEFAULT 0,
	set_autoBackup BIT NOT NULL DEFAULT 0,
	set_backupTimeInterval TINYINT NOT NULL DEFAULT 1, -- 1 : WEEKLY
	set_defaultReportInterval TINYINT NOT NULL DEFAULT 2, -- 2 : MONTHLY
	set_defaultReportType TINYINT NOT NULL DEFAULT 2, -- 2 : TODOLIST AND WORKDONE
	backup_last_id INT NOT NULL DEFAULT 1
);

alter table Settings
add constraint FK_Settings_Users
foreign key  (user_id) references Users(id);

CREATE TABLE Backups(
	id INT IDENTITY(1,1) PRIMARY KEY,
	user_id INT NOT NULL,
	filename VARCHAR(50) NOT NULL,
	filepath VARCHAR(300) NOT NULL
);

alter table Backups
add constraint FK_Backups_Users
foreign key  (user_id) references Users(id);

CREATE TABLE RoutineWorks(
	id INT IDENTITY(1,1) PRIMARY KEY,
	user_id INT NOT NULL,
	work_description varchar(250) NOT NULL
);

alter table RoutineWorks
add constraint FK_RoutineWorks_Users
foreign key  (user_id) references Users(id);

CREATE TABLE RoutineWorkRecords(
	id INT IDENTITY(1,1) PRIMARY KEY,
	work_Ticks TEXT NOT NULL,
	add_date DATE NOT NULL
);

CREATE TABLE RoutineWorkRelationShip(
	id INT IDENTITY(1,1) PRIMARY KEY,
	user_id INT NOT NULL,
	other_user_id INT NOT NULL
);

alter table RoutineWorkRelationShip
add constraint FK_RoutineWorkRelationShip_Users
foreign key  (user_id) references Users(id);

alter table RoutineWorkRelationShip
add constraint FK_RoutineWorkRelationShip_Users2
foreign key  (other_user_id) references Users(id);
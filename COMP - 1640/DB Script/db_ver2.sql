USE [master]
CREATE DATABASE IdeasCampaignManager_ver2
GO
USE IdeasCampaignManager_ver2
GO
CREATE TABLE Role
(
	r_ID INT PRIMARY KEY IDENTITY (1,1),
	r_Name nvarchar(50) NOT NULL UNIQUE,
	r_Description nvarchar(300)
)

CREATE TABLE Category
(
	c_ID INT PRIMARY KEY IDENTITY (1,1),
	c_Name nvarchar(50) NOT NULL UNIQUE,
	c_Description nvarchar(300)
)

CREATE TABLE Department
(
	dp_ID INT PRIMARY KEY IDENTITY (1,1),
	dp_Name nvarchar(50) NOT NULL UNIQUE,
	dp_Description nvarchar(300)
)

CREATE TABLE PersonalDetail
(
	p_ID INT PRIMARY KEY IDENTITY (1,1),
	r_ID INT FOREIGN KEY REFERENCES Role(r_ID), --FK
	dp_ID INT FOREIGN KEY REFERENCES Department(dp_ID), --FK
	p_Name nvarchar(50) NOT NULL,
	p_Email nvarchar(50) NOT NULL UNIQUE,
	p_Pass	nvarchar(50) NOT NULL,
	p_Detail nvarchar(300)
)

CREATE TABLE Topic
(
	t_ID INT PRIMARY KEY IDENTITY (1,1),
	t_Name nvarchar(150) NOT NULL,
	t_Details nvarchar(300),
	t_PostedDate datetime,
	t_ClosureDate datetime,
	t_FinalClosureDate datetime
)

CREATE TABLE Idea
(
	i_ID INT PRIMARY KEY IDENTITY (1,1),
	t_ID INT FOREIGN KEY REFERENCES Topic(t_ID),
	c_ID INT FOREIGN KEY REFERENCES Category(c_ID), --FK
	p_ID INT FOREIGN KEY REFERENCES PersonalDetail(p_ID), --FK
	i_Title nvarchar(150) NOT NULL,
	i_Details nvarchar(500),
	DocumentLink nvarchar(300), -- link to the additional document uploaded by author of the idea
	i_IsAnonymous bit,
	TotalViews int,
	i_PostedDate datetime,
)

CREATE TABLE [Comment]
(
	cmt_ID INT PRIMARY KEY IDENTITY (1,1),
	I_ID INT FOREIGN KEY REFERENCES Idea(i_ID), --FK
	p_ID INT FOREIGN KEY REFERENCES PersonalDetail(p_ID), --FK
	cmt_Detail nvarchar(500) NOT NULL,
	cmt_IsAnonymous bit,
	cmt_PostedDate datetime,
)

CREATE TABLE Voting
(
	i_ID INT FOREIGN KEY REFERENCES Idea(i_ID),
	p_ID INT FOREIGN KEY REFERENCES PersonalDetail(p_ID),
	Vote bit
)
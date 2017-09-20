CREATE PROCEDURE uspCreateMembershipTypeTable
AS
CREATE TABLE MembershipType(
MemTypeID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
MemType VARCHAR(15) NOT NULL,
JoinDate VARCHAR(50) NOT NULL,
RenewalDate VARCHAR(50) NOT NULL,
GymLocation VARCHAR(50) NOT NULL)

GO
execute uspCreateMembershipTypeTable
GO

CREATE PROCEDURE uspCreateMemberTable
AS
CREATE TABLE Member(
MemberID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
MemTypeID int,
Email VARCHAR(100) NOT NULL,
MemPass VARCHAR(50) NOT NULL,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
Gender VARCHAR(1) NOT NULL,
Age int NOT NULL,
Phone VARCHAR(20) NOT NULL,
MemAddress VARCHAR(50) NOT NULL,
IsAdmin BIT NOT NULL,
FOREIGN KEY (MemTypeID) REFERENCES MembershipType(MemTypeID))

GO
execute uspCreateMemberTable
GO

CREATE PROCEDURE uspCreateTrainerTable
AS
CREATE TABLE Trainer(
TrainerID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
Name VARCHAR(50) NOT NULL,
Age int NOT NULL,
Gender VARCHAR(1) NOT NULL,
Speciality VARCHAR(50) NOT NULL)

GO
execute uspCreateTrainerTable
GO

CREATE PROCEDURE uspCreateClassTable
AS
CREATE TABLE Class(
ClassID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
TrainerID int,
[Time] VARCHAR(50) NOT NULL,
ClassType VARCHAR(50) NOT NULL,
Location VARCHAR(50) NOT NULL,
MaxMembers int NOT NULL,
FOREIGN KEY (TrainerID) REFERENCES Trainer(TrainerID))

GO
execute uspCreateClassTable
GO

CREATE PROCEDURE uspCreateBookingTable
AS
CREATE TABLE Booking(
BookingID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
ClassID int,
MemberID int,
[Date] VARCHAR(50) NOT NULL,
[Time] VARCHAR(50),
FOREIGN KEY (ClassID) REFERENCES Class(ClassID),
FOREIGN KEY (MemberID) REFERENCES Member(MemberID))

GO
execute uspCreateBookingTable
GO

CREATE PROCEDURE uspCreatePTSessionTable
AS
CREATE TABLE PTSession(
PTSessionID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
TrainerID int,
MemberID int,
SessionLength VARCHAR(50) NOT NULL,
SessionDate VARCHAR(50) NOT NULL,
SessionTime VARCHAR(50) NOT NULL,
SessType VARCHAR(50) NOT NULL,
Cost DECIMAL(5, 2) NOT NULL,
SessLocation VARCHAR(50) NOT NULL,
FOREIGN KEY (TrainerID) REFERENCES Trainer(TrainerID),
FOREIGN KEY (MemberID) REFERENCES Member(MemberID))

GO
execute uspCreatePTSessionTable
GO

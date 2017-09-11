CREATE PROCEDURE uspInsertMembershipTypeTable
@memType VARCHAR(15),
@joinDate VARCHAR(10),
@renewalDate VARCHAR(10),
@gymLocation VARCHAR(50)
AS
INSERT INTO MembershipType VALUES (@memType, @joinDate, @renewalDate, @gymLocation)
GO

INSERT INTO MembershipType (MemType, JoinDate, RenewalDate, GymLocation)
VALUES ('Day Pass','na', 'na', 'Georges St'),
('Annual','2017-06-10', '2018-06-10', 'Georges St'),
('Annual','2016-11-15', '2017-11-15', 'Georges St'),
('Annual','2017-08-10', '2018-08-10', 'Jervis St'),
('Monthly','2017-08-29', '2017-09-26', 'Georges St'),
('Annual','2017-06-10', '2018-06-10', 'Drumcondra'),
('Day Pass','na', 'na', 'Georges St'),
('Day Pass','na', 'na', 'Georges St'),
('Day Pass','na', 'na', 'Drumcondra'),
('Monthly','2017-08-29', '2017-09-29', 'Jervis St');
GO

CREATE PROCEDURE uspInsertMemberTable
@memTypeID int,
@email VARCHAR(100),
@memPass VARCHAR(50),
@firstName VARCHAR(50),
@lastName VARCHAR(50),
@gender VARCHAR(50),
@age int,
@phone VARCHAR(20),
@address VARCHAR(50),
@isAdmin BIT
AS
INSERT INTO Member VALUES (@memTypeID, @email, @memPass, @firstName, @lastName, @gender, @age, @phone, @address, @isAdmin)
GO

INSERT INTO Member (MemTypeID, Email, MemPass, FirstName, LastName, Gender, Age, Phone, MemAddress, IsAdmin)
VALUES (1, 'cdawg@yahoo.ie', '12345', 'Carl', 'Taylor', 'M', 28, '0876365835', '22 Fake St, Dublin, Ireland', 1),
(7, 'mdawg@yahoo.ie', '12345','Martin', 'McCarthy', 'M', 21, '0876356466', '25 Fake St, Dublin, Ireland', 1),
(8, 'fdawg@yahoo.ie', '12345','Felix', 'Dehouskat', 'M', 25, '0876362456', '28 Fake St, Dublin, Ireland', 1),
(2, 'jdawg@yahoo.ie', '12345','Jenny', 'Block', 'F', 18, '0876365546', '15 Fake St, Dublin, Ireland', 0),
(3, 'sdawg@yahoo.ie', '12345','Susan', 'Sun', 'F', 52, '0876365564', '38 Fake St, Dublin, Ireland', 0),
(4, 'mdawg@yahoo.ie', '12345','Mary', 'Jane', 'F', 64, '0876654835', '45 Fake St, Dublin, Ireland', 0),
(5, 'bdawg@yahoo.ie', '12345','Bill', 'Tedson', 'M', 38, '086548835', '52 Fake St, Dublin, Ireland', 0),
(6, 'tedawg@yahoo.ie', '12345','Ted', 'Danson', 'M', 71, '086524235', '122 Fake St, Dublin, Ireland', 0),
(9, 'teddydawg@yahoo.ie', '12345','Ted', 'Hansome', 'M', 41, '086524235', '126 Fake St, Dublin, Ireland', 0),
(10, 'rorodawg@yahoo.ie', '12345','Ronald', 'Rump', 'M', 19, '086525556', '130 Fake St, Dublin, Ireland', 0);
GO


CREATE PROCEDURE uspInsertTrainerTable
@name VARCHAR(50),
@age int,
@gender VARCHAR(1),
@speciality VARCHAR(50)
AS
INSERT INTO Trainer VALUES (@name, @age, @gender, @speciality)
GO

INSERT INTO Trainer (Name, Age, Gender, Speciality)
VALUES ('Simon Flynn', 34, 'M', 'Strength'),
('Colm Griffin', 32, 'M', 'Long Distance Running'),
('David Brien', 34, 'M', 'Toning'),
('Kim Hanson', 28, 'F', 'Strength'),
('MArk Flynn', 34, 'M', 'Toning'),
('Adam North', 28, 'M', 'Strength'),
('Jim Franklin', 34, 'M', 'Body Building'),
('Tony Baldwin', 28, 'M', 'Strength'),
('Tim Dalton', 45, 'M', 'Body Building');
GO

CREATE PROCEDURE uspInsertClassTable
@trainerID int,
@time VARCHAR(6),
@classType VARCHAR(50),
@location VARCHAR(50),
@maxMembers int
AS
INSERT INTO Class VALUES (@trainerID, @time, @classType, @location, @maxMembers)
GO

INSERT INTO Class (TrainerID, [Time], ClassType, Location, MaxMembers)
VALUES (1, '14:00', 'DeadLifting', 'Georges St', 10),
(2, '10:00', 'Agility', 'Jervis St', 5),
(2, '07:00', 'Stamina', 'Jervis St', 5),
(1, '16:00', 'Squats', 'Georges St', 15),
(3, '20:00', 'Pilates', 'Drumcondra', 20),
(4, '14:00', 'Circuits', 'Georges St', 12),
(5, '18:30', 'Bicep/Tricep', 'Drumcondra', 20);
GO

CREATE PROCEDURE uspInsertBookingTable
@classID int,
@memberID int,
@date VARCHAR(50),
@time VARCHAR(6)
AS
INSERT INTO Booking VALUES (@classID, @memberID, @date, @time)
GO

INSERT INTO Booking (ClassID, MemberID, [Date], [Time])
VALUES (1, 2, '2017-08-22', '14:00'),
(1, 3, '2017-08-22', '14:00'),
(2, 4, '2017-08-28', '10:00'),
(4, 2, '2017-08-25', '16:00'),
(3, 6, '2017-08-26', '07:00'),
(2, 10, '2017-08-28', '10:00');
GO

CREATE PROCEDURE uspInsertPTSessionTable
@trainerID int,
@memberID int,
@sessionLength VARCHAR(50),
@sessionDate VARCHAR(50),
@sessionTime VARCHAR(6),
@sessType VARCHAR(50),
@cost DECIMAL(5, 2),
@sessLocation VARCHAR(50)
AS
INSERT INTO PTSession VALUES (@trainerID, @memberID, @sessionLength, @sessionDate, @sessionTime, @sessType, @cost, @sessLocation)
GO


INSERT INTO PTSession (TrainerID, MemberID, SessionLength, SessionDate, SessionTime, SessType, Cost, SessLocation)
VALUES (1, 2, '1 hour', '2017-09-22', '18:00', 'Strength', 20.00, 'Georges St'),
(2, 4, '2 hour', '2017-09-22', '18:00', 'Cardio', 40.00, 'Jervis St'),
(4, 8, '1 hour', '2017-09-22', '18:00', 'Strength', 20.00, 'Georges St'),
(4, 5, '0.5 hour', '2017-09-01', '18:00', 'Strength', 10.00, 'Georges St'),
(3, 6, '1 hour', '2017-09-30', '18:00', 'Yoga', 20.00, 'Drumcondra'),
(5, 10, '2 hour', '2017-09-15', '18:00', 'Back Exercises', 40.00, 'Jervis St'),
(1, 2, '1 hour', '2017-09-30', '18:00', 'Strength', 20.00, 'Georges St');
GO

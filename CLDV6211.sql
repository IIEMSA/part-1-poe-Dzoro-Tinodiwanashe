-- Database creation
USE EventEaseDatabase;
-- Drop dependent tables first
DROP TABLE IF EXISTS Booking;
DROP TABLE IF EXISTS Event;
DROP TABLE IF EXISTS Venue;

-- Create the Venue Table
DROP TABLE IF EXISTS Venue;
CREATE TABLE Venue (
	venueID INT IDENTITY(1,1) PRIMARY KEY,
	venueName VARCHAR(250),
	location VARCHAR (250),
	capacity INT,
	imageUrl VARCHAR (100),
);

-- Event table
DROP TABLE IF EXISTS Event;
CREATE TABLE Event (
	eventID INT IDENTITY(1,1) PRIMARY KEY,
	eventName VARCHAR(250),
	eventDate DATE,
	description VARCHAR (250),
	imageUrl VARCHAR (100),
	venueID INT,
	FOREIGN KEY (venueID) REFERENCES Venue (venueID),
);

-- Booking table, associative linking Venue and Event
DROP TABLE IF EXISTS Booking;
CREATE TABLE Booking (
	bookingID INT IDENTITY(1,1) PRIMARY KEY,
	eventID INT,
	venueID INT,
	FOREIGN KEY (eventID) REFERENCES Event (eventID),
	FOREIGN KEY (venueID) REFERENCES Venue (venueID),
	bookingDate DATE,
);

select * from Venue;
select * from Event;
select * from Booking;
SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Venue';


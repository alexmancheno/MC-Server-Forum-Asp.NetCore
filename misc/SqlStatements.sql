
-- As of 7/15/17
CREATE TABLE UserAccounts (
	UserID int identity(1,1) Primary Key,
	FirstName varchar(25) NOT NULL,
	LastName varchar(25) NOT NULL,
	Email varchar (40) NOT NULL,
	Username varchar (40) NOT NULL,
	UserPassword varchar(50) NOT NULL,
	ConfirmPassword varchar(50) NOT NULL
);
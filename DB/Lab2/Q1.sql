create database AirLine_db
use AirLine_db

Create table Airline(
    AirL_ID int identity(10,10) primary key,
    Name varchar(10) not null, 
    Contact_Person varchar(20),
    Address varchar(20) default 'Cairo'
)

CREATE TABLE Employee (
    Emp_Id      INT IDENTITY(10,1) PRIMARY KEY,
    Name        VARCHAR(100) NOT NULL,
    Position    VARCHAR(50),
    Gender      CHAR(1),
    Byear       INT,
    Bmonth      INT CHECK (Bmonth BETWEEN 1 AND 12),
    Bday        INT CHECK (Bday BETWEEN 1 AND 31),
    Address     VARCHAR(200),
    Airline_Id  INT NOT NULL,


    CONSTRAINT FK_Employee_Airline FOREIGN KEY (Airline_Id)
    REFERENCES Airline(AirL_ID)
)

CREATE TABLE Airline_Phone (
    Airline_Id  INT NOT NULL,
    Phone       VARCHAR(20) NOT NULL,
    CONSTRAINT PK_Airline_Phone PRIMARY KEY (Airline_Id, Phone),
    CONSTRAINT FK_AirlinePhone_Airline FOREIGN KEY (Airline_Id)
        REFERENCES Airline(AirL_ID) ON DELETE CASCADE
)

CREATE TABLE Employee_Qualification (
    Employee_Id     INT NOT NULL,
    Qualification   VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Employee_Qualification PRIMARY KEY (Employee_Id, Qualification),
    CONSTRAINT FK_EmpQual_Employee FOREIGN KEY (Employee_Id)
    REFERENCES Employee(Emp_Id) ON DELETE CASCADE
)

CREATE TABLE Transactions (
    Trans_ID    INT IDENTITY(10,1) PRIMARY KEY,
    Description VARCHAR(200),
    Trans_Date      DATE,
    Amount      DECIMAL(12,2),
    Airline_Id  INT NOT NULL,
    
    
    CONSTRAINT FK_Transaction_Airline FOREIGN KEY (Airline_Id)
    REFERENCES Airline(AirL_ID)
)

CREATE TABLE Crew (
    Crew_Id     INT IDENTITY(10,1) PRIMARY KEY,
    Maj_Pilot   VARCHAR(20),
    Assis_Pilot VARCHAR(20),
    Host1       VARCHAR(20),
    Host2       VARCHAR(20)
)

CREATE TABLE AirCraft (
    Id          INT IDENTITY(10,1) PRIMARY KEY,
    Model       VARCHAR(50),
    Capacity    INT,
    Airline_Id  INT NOT NULL,
    Crew_Id     INT NULL unique,


    CONSTRAINT FK_AirCraft_Airline FOREIGN KEY (Airline_Id)
    REFERENCES Airline(AirL_ID),
    CONSTRAINT FK_AirCraft_Crew FOREIGN KEY (Crew_Id)
    REFERENCES Crew(Crew_Id)
)

CREATE TABLE Route (
    R_ID              INT IDENTITY(10,1) PRIMARY KEY,
    Classification  VARCHAR(50),
    Distance        DECIMAL(10,2),
    Destination     VARCHAR(100),
    Origin          VARCHAR(100)
)

CREATE TABLE Assigned (
    AirCraft_Id         INT NOT NULL,
    Route_Id            INT NOT NULL,
    Num_of_Passengers   INT,
    Price               DECIMAL(10,2),
    Arrival             DATETIME,
    Departure           DATETIME,
    Duration            INT,

    CONSTRAINT PK_Assigned PRIMARY KEY (AirCraft_Id, Route_Id, Departure),
    CONSTRAINT FK_Assigned_AirCraft FOREIGN KEY (AirCraft_Id)
    REFERENCES AirCraft(Id),
    CONSTRAINT FK_Assigned_Route FOREIGN KEY (Route_Id)
    REFERENCES Route(R_ID)
)


insert into Airline
values ('EgyptAir', 'Ahmed Ali', 'Cairo, Egypt')
insert into Airline
values ('KSAAir', 'Mohamed Abdallah', 'Geddah, KSA')

INSERT INTO Employee
VALUES
('Sara Mostafa', 'Manager', 'F', 1990, 5, 14, 'Alex',10)
INSERT INTO Employee
VALUES
('Mohamed ben Naif', 'Manager', 'M', 1990, 2, 4, 'Jeddah',30)

INSERT INTO Airline_Phone 
VALUES
(10, '02-12345678'),
(30, '050-87614321')

INSERT INTO Employee_Qualification 
VALUES
(11, 'MBA'),
(12, 'Aviation Management Certificate')

INSERT INTO Transactions 
VALUES
('Fuel Purchase', '2026-07-01', 25000.00, 10),
('Fuel Purchase', '2026-06-10', 26000.00, 30)

INSERT INTO Crew 
VALUES
('Capt. Khaled', 'F.O. Omar', 'Mona', 'Laila'),
('Capt. Ahmed', 'F.O. Mohannad', 'Sara', 'Asmaa')

INSERT INTO AirCraft 
VALUES
('Boeing 737', 180, 10, 10),
('Boeing 737', 180, 30, 11)

INSERT INTO Route 
VALUES
('Domestic', 450.5, 'Aswan', 'Cairo'),
('Domestic', 450.5, 'Dammam', 'Jeddah')

INSERT INTO Assigned
VALUES
(12, 10, 150, 899.99, '2026-07-15 09:30', '2026-07-15 08:00', 90),
(13, 11, 150, 952.00, '2026-07-15 09:30', '2026-07-15 08:00', 120)
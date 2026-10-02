/*
All Ids are Identity
All Foreign keys are not identity
All foreign keys have cascade rule on delete and update
Age and Netsalary are calculated attributes but it will be on instructor table creation
Netsalary = salary+overtime
Age=current year – birthdate year
Address has only cairo or alex value
All salaries in the range from 1000 to 5000
Salary has a default value = 3000
Overtime is unique
Capacity of each lab under 20 seats
Lab is weak entity
Hiredate has a default value= current system data
Duration of each course is unique
*/

CREATE DATABASE Q3_Lab2

CREATE TABLE Instructor
(
    Inst_ID     INT IDENTITY(100,1)   NOT NULL,
    FName       VARCHAR(50)         NOT NULL,
    LName        VARCHAR(50)         NOT NULL,
    BD          DATE                NOT NULL,                       
    HireDate    DATE                NOT NULL,   
    Salary      DECIMAL(10,2)       NOT NULL DEFAULT (3000),
    OverTime    DECIMAL(10,2)       NOT NULL,
    Address     VARCHAR(20)         NOT NULL,
 
    Age         AS (2026 - YEAR(BD))      persisted,
    NetSalary   AS (Salary + OverTime)                  persisted,
 
    CONSTRAINT PK_Instructor        PRIMARY KEY (Inst_ID),
    CONSTRAINT UQ_Instructor_OT     UNIQUE (OverTime),
    CONSTRAINT CK_Instructor_Salary CHECK (Salary BETWEEN 1000 AND 5000),
    CONSTRAINT CK_Instructor_Addr   CHECK (Address IN ('Cairo','Alex'))
)

CREATE TABLE Course
(
    CID       INT IDENTITY(10,1)   NOT NULL,
    CName     VARCHAR(100)        NOT NULL,
    Duration  INT                 NOT NULL,
 
    CONSTRAINT PK_Course       PRIMARY KEY (CID),
    CONSTRAINT UQ_Course_Dur   UNIQUE (Duration)
)


CREATE TABLE Lab
(
    CID        INT              NOT NULL,
    LID        INT IDENTITY(10,1) NOT NULL,
    Location   VARCHAR(50)      NOT NULL,
    Capacity   INT              NOT NULL,
 
    CONSTRAINT PK_Lab PRIMARY KEY (CID, LID),
 
    CONSTRAINT CK_Lab_Capacity CHECK (Capacity < 20),
 
    CONSTRAINT FK_Lab_Course FOREIGN KEY (CID)
    REFERENCES Course(CID) ON DELETE CASCADE ON UPDATE CASCADE
)

CREATE TABLE Teach
(
    InstructorID  INT NOT NULL,
    CID           INT NOT NULL,
 
    CONSTRAINT PK_Teach PRIMARY KEY (InstructorID, CID),
 
    CONSTRAINT FK_Teach_Instructor FOREIGN KEY (InstructorID)
        REFERENCES Instructor(Inst_ID) ON DELETE CASCADE ON UPDATE CASCADE,
 
    CONSTRAINT FK_Teach_Course FOREIGN KEY (CID)
        REFERENCES Course(CID) ON DELETE CASCADE ON UPDATE CASCADE
)

INSERT INTO Instructor
VALUES
    ('Ahmed',  'Hassan',  '1985-03-14', '2015-09-01', 3500, 200, 'Cairo'),
    ('Mona',   'Fathy',   '1990-07-22', '2018-01-15', 4200, 150, 'Alex')

INSERT INTO Course
VALUES
    ('Database Systems',      12),
    ('Web Development',       10)

INSERT INTO Teach 
VALUES
    (100, 10),
    (101, 11)

INSERT INTO Lab
VALUES
    (10, 'Building A - Room 101', 18),
    (11, 'Building A - Room 102', 15)
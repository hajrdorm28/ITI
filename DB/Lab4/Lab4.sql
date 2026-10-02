-- Q1 Lab 4

-- Use ITI DB:
USE ITI
-- 1) Create a scalar function that takes a date and returns the Month name of that date.
    GO
    CREATE FUNCTION dbo.GetMonthName (@d DATE)
    RETURNS VARCHAR(20)
    AS
    BEGIN
       RETURN DATENAME(MONTH, @d)
    END
    GO
-- 2) Create a multi-statements table-valued function that takes 2 integers and returns the values between them.
    GO
    CREATE FUNCTION dbo.ValueesBetween (@Start INT, @End INT)
    RETURNS @Result TABLE (Value INT)
    AS
    BEGIN
        DECLARE @i INT = @Start
        WHILE @i <= @End
      BEGIN
        INSERT INTO @Result VALUES (@i)
        SET @i = @i + 1
      END
    RETURN
   END
   GO
-- 3) Create a table-valued function that takes Student No and returns Department Name with Student full name.
GO
    CREATE FUNCTION dbo.StudentDept (@St_Id INT)
    RETURNS TABLE
    AS
    RETURN
    (SELECT D.Dept_Name, S.St_Fname + ' ' + S.St_Lname AS Student_Full_Name
     FROM Student S INNER JOIN Department D 
     ON S.Dept_Id = D.Dept_Id
     WHERE S.St_Id = @St_Id)
GO
-- 4) Create a scalar function that takes Student ID and returns a message to the user.
	/*If first name and Last name are null, then display 'First name & last name are null.
	  If First name is null, then display 'first name is null'
      If the last name is null, then display 'last name is null.'
      Else display 'First name & last name are not null'*/
GO
CREATE FUNCTION dbo.CheckKStudentName (@St_Id INT)
RETURNS VARCHAR(50)
AS
BEGIN
    DECLARE @Fname VARCHAR(50), @Lname VARCHAR(50), @Masg VARCHAR(100);

    SELECT @Fname = St_Fname, @Lname = St_Lname
    FROM Student
    WHERE St_Id = @St_Id;

    IF @Fname IS NULL AND @Lname IS NULL
        SET @Masg = 'First name & last name are null'
    ELSE IF @Fname IS NULL
        SET @Masg = 'first name is null'
    ELSE IF @Lname IS NULL
        SET @Masg = 'last name is null'
    ELSE
        SET @Masg = 'First name & last name are not null'

    RETURN @Masg
END
GO

-- 5) Create a function that takes an integer which represents the format of the Manager hiring date and displays department name, Manager Name and hiring date with this format.
GO

CREATE FUNCTION dbo.ManagerHireDate_Format (@Format INT)
RETURNS TABLE
AS
RETURN
(SELECT D.Dept_Name, I.Ins_Name AS Manager_Name, CONVERT(VARCHAR(30), D.Manager_hiredate, @Format) AS HireDate
    FROM Department D INNER JOIN Instructor I 
    ON D.Dept_Manager = I.Ins_Id)
GO

--***** 6) Create a multi-statement table-valued function that takes a string.
	 /*If first name and Last name are null, then display 'First name & last name are null
       If First name is null, then display 'first name is null'
       If Last name is null, then display 'last name is null.'
       Else display 'First name & last name are not null'
       Note: Use “ISNULL” function*/
GO
CREATE FUNCTION dbo.CheckNameStatus
(
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100)
)
RETURNS @ResultTable TABLE 
(
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    StatusMessage VARCHAR(250)
)
AS
BEGIN
    DECLARE @StatusMessage VARCHAR(250)

    SET @StatusMessage = 
        CASE 
            WHEN ISNULL(@FirstName, '_NULL_') = '_NULL_' AND ISNULL(@LastName, '_NULL_') = '_NULL_' 
                THEN 'First name & last name are null'
            WHEN ISNULL(@FirstName, '_NULL_') = '_NULL_' 
                THEN 'first name is null'
            WHEN ISNULL(@LastName, '_NULL_') = '_NULL_' 
                THEN 'last name is null'
            ELSE 'First name & last name are not null'
        END

    -- Insert the evaluated row into the return table
    INSERT INTO @ResultTable (FirstName, LastName, StatusMessage)
    VALUES (@FirstName, @LastName, @StatusMessage)

    RETURN
END
GO

-- 7) Create function that takes project number and display all employees in this project (Use My Company DB)

USE MyCompany;
GO

CREATE FUNCTION dbo.fn_ProjectEmployees (@Pno INT)
RETURNS TABLE
AS
RETURN
(SELECT E.SSN, E.Fname, E.Lname
 FROM Employee E INNER JOIN Works_for W 
 ON E.SSN = W.ESSN
 WHERE W.Pno = @Pno)
GO
--=============================================================================================
-- Q2 Lab 4
--1) Create a stored procedure to show the number of students per department.[use ITI DB] 
USE ITI;
GO
CREATE PROCEDURE dbo.StudentsPerDept
AS
BEGIN
    SELECT D.Dept_Name, COUNT(S.St_Id) AS Num_Students
    FROM Department D LEFT OUTER JOIN Student S 
    ON D.Dept_Id = S.Dept_Id
    GROUP BY D.Dept_Name;
END
GO
--2) Create a stored procedure that will check for the Number of employees in the project 100 if they are more than 3 print message to the user “'The number of employees in the project 100 is 3 or more'” if they are less display a message to the user “'The following employees work for the project 100'” in addition to the first name and last name of each one. [MyCompany DB] 
USE MyCompany;
GO
CREATE PROCEDURE dbo.usp_CheckProject100Employees
AS
BEGIN
    DECLARE @Count INT;
    SELECT @Count = COUNT(*) FROM Works_for WHERE Pno = 100;

    IF @Count >= 3
        PRINT 'The number of employees in the project 100 is 3 or more';
    ELSE
    BEGIN
        PRINT 'The following employees work for the project 100';
        SELECT E.Fname, E.Lname
        FROM Employee E INNER JOIN Works_for W 
        ON E.SSN = W.ESSN
        WHERE W.Pno = 100;
    END
END
GO
--3) Create a stored procedure that will be used in case an old employee has left the project and a new one becomes his replacement. The procedure should take 3 parameters (old Emp. number, new Emp. number and the project number) and it will be used to update works_on table. [MyCompany DB]
USE MyCompany
GO
CREATE PROCEDURE dbo.usp_ReplaceEmployeeOnProject
    @OldEmpNo CHAR(9),
    @NewEmpNo CHAR(9),
    @ProjNo   INT
AS
BEGIN
    UPDATE Works_for
    SET ESSN = @NewEmpNo
    WHERE ESSN = @OldEmpNo AND Pno = @ProjNo;
END
GO
--=========================================================================================================
-- Q3 Lab 4
--1) Create a stored procedure that calculates the sum of a given range of numbers
CREATE PROCEDURE dbo.SumRange
    @Start INT,
    @End   INT
AS
BEGIN
    DECLARE @Sum INT = (@Start + @End) * (@End - @Start + 1) / 2;
    SELECT @Sum AS [Sum Of Range]
END
GO
--2) Create a stored procedure that calculates the area of a circle given its radius
CREATE PROCEDURE dbo.CircleArea
    @Radius FLOAT
AS
BEGIN
    SELECT PI() * @Radius * @Radius AS Area;
END
GO
--3) Create a stored procedure that calculates the age category based on a person's age ( Note: IF Age < 18 then Category is Child and if  Age >= 18 AND Age < 60 then Category is Adult otherwise  Category is Senior)
CREATE PROCEDURE dbo.AgeCategory
    @Age INT
AS
BEGIN
    DECLARE @Category VARCHAR(20);

    IF @Age < 18
        SET @Category = 'Child';
    ELSE IF @Age >= 18 AND @Age < 60
        SET @Category = 'Adult';
    ELSE
        SET @Category = 'Senior';

    SELECT @Category AS Category;
END
GO
--4) Create a stored procedure that determines the maximum, minimum, and average of a given set of numbers ( Note : set of numbers as Numbers = '5, 10, 15, 20, 25')
CREATE PROCEDURE dbo.NumberStats
    @Numbers VARCHAR(MAX)
AS
BEGIN
    SELECT
        MAX(CAST(value AS INT)) AS MaxValue,
        MIN(CAST(value AS INT)) AS MinValue,
        AVG(CAST(value AS INT)) AS AvgValue
    FROM STRING_SPLIT(@Numbers, ',');
END
GO
--Use ITI DB:
--5) Create a view that displays the student's full name, course name if the student has a grade more than 50. 
USE ITI
GO
CREATE VIEW dbo.VStudentsPassed
AS
SELECT S.St_Fname + ' ' + S.St_Lname AS Student_Full_Name, C.Crs_Name
FROM Student S INNER JOIN Stud_Course SC 
ON S.St_Id = SC.St_Id
INNER JOIN Course C 
ON SC.Crs_Id = C.Crs_Id
WHERE SC.Grade > 50;
GO
--6) Create an Encrypted view that displays manager names and the topics they teach. 
CREATE VIEW dbo.VManagerTopics
WITH ENCRYPTION
AS
SELECT I.Ins_Name AS Manager_Name, T.Top_Name
FROM Department D INNER JOIN Instructor I 
ON D.Dept_Manager = I.Ins_Id
INNER JOIN Ins_Course IC 
ON I.Ins_Id = IC.Ins_Id
INNER JOIN Course C 
ON IC.Crs_Id = C.Crs_Id
INNER JOIN Topic T 
ON C.Top_Id = T.Top_Id;
GO
--7) Create a view that will display Instructor Name, Department Name for the ‘SD’ or ‘Java’ Department “use Schema binding” and describe what is the meaning of Schema Binding
CREATE VIEW dbo.VSDJavaInstructors
WITH SCHEMABINDING
AS
SELECT I.Ins_Name, D.Dept_Name
FROM dbo.Instructor I INNER JOIN dbo.Department D 
ON I.Dept_Id = D.Dept_Id
WHERE D.Dept_Name IN ('SD', 'Java');
GO
--8) Create a view “V1” that displays student data for students who live in Alex or Cairo. 
     -- Note: Prevent the users to run the following query 
        --Update V1 set st_address=’tanta’
        --Where st_address=’alex’;
CREATE VIEW dbo.V1
AS
SELECT *
FROM Student
WHERE St_Address IN ('Alex', 'Cairo')
WITH CHECK OPTION
GO
--9) Create a view that will display the project name and the number of employees working on it. (Use Company DB)
USE MyCompany;
GO
CREATE VIEW dbo.VProjectEmployeeNum
AS
SELECT P.Pname, COUNT(W.ESSN) AS Num_Employees
FROM Project P INNER JOIN Works_for W 
ON P.Pnumber = W.Pno
GROUP BY P.Pname;
GO
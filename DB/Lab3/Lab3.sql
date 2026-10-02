-- Q1_Lab3
--Using MyCompany Database and try to  create the following Queries:
       USE MyCompany
--1) Display the Department id, name and id and the name of its manager.

       SELECT D.Dnum, D.Dname, D.MGRSSN AS [Manager ID], E.Fname + ' ' + E.Lname AS [Manager Name]
       FROM Departments D INNER JOIN Employee E 
       ON D.MGRSSN = E.SSN

--2) Display the name of the departments and the name of the projects under its control.

     SELECT D.Dname, P.Pname
     FROM Departments D INNER JOIN Project P 
     ON D.Dnum = P.Dnum

--3) Display the full data about all the dependence associated with the name of the employee they depend on.

    SELECT E.Fname + ' ' + E.Lname AS [Employee Name], D.*
    FROM Dependent D INNER JOIN Employee E 
    ON D.ESSN = E.SSN

--4) Display the Id, name and location of the projects in Cairo or Alex city.

    SELECT Pnumber, Pname, Plocation
    FROM Project
    WHERE City IN ('Cairo', 'Alex')

--5) Display the Projects full data of the projects with a name starting with "a" letter.

    SELECT *
    FROM Project
    WHERE Pname LIKE 'a%'

--6) Display all the employees in department 30 whose salary from 1000 to 2000 LE monthly

    SELECT *
    FROM Employee
    WHERE Dno = 30 AND Salary BETWEEN 1000 AND 2000

--7) Retrieve the names of all employees in department 10 who work more than or equal 10 hours per week on the "AL Rabwah" project.

    SELECT E.Fname, E.Lname
    FROM Employee E INNER JOIN Works_for W 
    ON E.SSN = W.ESSN
    INNER JOIN Project P 
    ON W.Pno = P.Pnumber
    WHERE E.Dno = 10 AND P.Pname = 'Al Rabwah' AND W.Hours >= 10

--8) Retrieve the names of all employees and the names of the projects they are working on, sorted by the project name.

    SELECT E.Fname, E.Lname, P.Pname
    FROM Employee E INNER JOIN Works_for W 
    ON E.SSN = W.ESSN
    INNER JOIN Project P 
    ON W.Pno = P.Pnumber
    ORDER BY P.Pname

--9) For each project located in Cairo City , find the project number, the controlling department name ,the department manager last name ,address and birthdate.

    SELECT P.Pnumber, D.Dname AS [Controlling Department], E.Lname AS [Manager Last Name], E.Address, E.Bdate
    FROM Project P INNER JOIN Departments D 
    ON P.Dnum = D.Dnum
    INNER JOIN Employee E 
    ON D.MGRSSN = E.SSN
    WHERE P.City = 'Cairo'

--10) Display the data of the department which has the smallest employee ID over all employees' ID.
 
    SELECT D.*
    FROM Departments D INNER JOIN Employee E 
    ON D.Dnum = E.Dno
    WHERE E.SSN IN (SELECT MIN(SSN) FROM Employee)
   
--11) Display the employee number and name if he/she has at least one dependent (use exists keyword) self-study.

    SELECT SSN, Fname, Lname
    FROM Employee E
    WHERE EXISTS (SELECT * FROM Dependent D WHERE D.ESSN = E.SSN)

--12) For each department -- if its average salary is less than the average salary of all employees displays its number, name and number of its employees.

    SELECT Dnum, Dname, COUNT(SSN) AS [Number of Employees]
    FROM Departments  INNER JOIN Employee 
    ON Dnum = Dno
    GROUP BY Dnum, Dname
    HAVING AVG(Salary) < (SELECT AVG(Salary) FROM Employee)
--============================================================================
-- Q2_Lab3
-- Use ITI DB
   USE ITI
--1) Retrieve a number of students who have a value in their age. 

    SELECT COUNT(*) AS [Num of Students With Age]
    FROM Student
    WHERE St_Age IS NOT NULL

--2) Display number of courses for each topic name 

    SELECT T.Top_Name, COUNT(C.Crs_Id) AS [Num Courses]
    FROM Topic T LEFT OUTER JOIN Course C 
    ON T.Top_Id = C.Top_Id
    GROUP BY T.Top_Name

--3) Display student with the following Format (use isNull function)

    SELECT S.St_Id AS [Student ID], S.St_Fname + ' ' + S.St_Lname AS [Student Full Name], ISNULL(D.Dept_Name, 'No Department') AS [Department name]
    FROM Student S LEFT OUTER JOIN Department D 
    ON S.Dept_Id = D.Dept_Id

--4) Select instructor name and his salary but if there is no salary display value ‘0000’ . “use one of Null Function”

    SELECT Ins_Name, ISNULL(CAST(Salary AS VARCHAR(10)), '0000') AS Salary
    FROM Instructor

--5) Select Supervisor first name and the count of students who supervises on them

    SELECT Sup.St_Fname AS [Supervisor First Name], COUNT(S.St_Id) AS [Number of Students Supervised]
    FROM Student S INNER JOIN Student Sup 
    ON S.St_super = Sup.St_Id
    GROUP BY Sup.St_Fname

--6) Display max and min salary for instructors

    SELECT MAX(Salary) AS [Max Salary], MIN(Salary) AS [Min Salary]
    FROM Instructor

--7) Select Average Salary for instructors 

    SELECT AVG(Salary) AS AvgSalary
    FROM Instructor

--8) Display instructors who have salaries less than the average salary of all instructors.

    SELECT Ins_Name, Salary
    FROM Instructor
    WHERE Salary < (SELECT AVG(Salary) FROM Instructor)

--9) Display the Department name that contains the instructor who receives the minimum salary

    SELECT D.Dept_Name
    FROM Department D INNER JOIN Instructor I ON D.Dept_Id = I.Dept_Id
    WHERE I.Salary = (SELECT MIN(Salary) FROM Instructor)

--10) Display department names that have more than 3 instructors.

    SELECT D.Dept_Name, COUNT(I.Ins_Id) AS NumInstructors
    FROM Department D INNER JOIN Instructor I 
    ON D.Dept_Id = I.Dept_Id
    GROUP BY D.Dept_Name
    HAVING COUNT(I.Ins_Id) > 3

--11) Display Course names that have total working hours (by all instructors) ≥ 40

    SELECT Crs_Name
    FROM Course
    WHERE Crs_Duration * (SELECT COUNT(*)
                          FROM Ins_Course
                          WHERE Ins_Course.Crs_Id = Course.Crs_Id) >= 40

USE AutoMapperSampleTest;
CREATE TABLE Employees
    (
      EmployeeId NVARCHAR(50) ,
      FirstName NVARCHAR(50) ,
      LastName NVARCHAR(50) ,
      DateOfJoining DATETIME
    );
GO

INSERT  INTO Employees
        ( EmployeeId ,
          FirstName ,
          LastName ,
          DateOfJoining
        )
        SELECT  'EMP1' ,
                'John' ,
                'Doe' ,
                '2011-03-03'
        UNION ALL
        SELECT  'EMP2' ,
                'Jane' ,
                'BeGood' ,
                '2011-02-11'
        UNION ALL
        SELECT  'EMP3' ,
                'Rich' ,
                'Guy' ,
                '2013-05-03'
        UNION ALL
        SELECT  'EMP4' ,
                'Bill' ,
                'Gates' ,
                '2010-12-03' 
GO

CREATE PROCEDURE [dbo].[GetAllEmployees]
AS
    SELECT  EmployeeId ,
            FirstName ,
            LastName ,
            DateOfJoining
    FROM    dbo.Employees
GO

CREATE PROCEDURE [dbo].[GetAllEmployeeWithId]
@Id NVARCHAR(10)
AS
    SELECT  EmployeeId ,
            FirstName ,
            LastName ,
            DateOfJoining
    FROM    dbo.Employees
	WHERE EmployeeId = @Id
GO

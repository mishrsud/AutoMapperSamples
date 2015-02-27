DECLARE @dbname nvarchar(128)
SET @dbname = N'AutoMapperSampleTest'

IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
BEGIN
    Alter database [AutoMapperSampleTest] set single_user with rollback immediate
    DROP DATABASE [AutoMapperSampleTest]
	CREATE DATABASE AutoMapperSampleTest
END
ELSE
BEGIN
    CREATE DATABASE AutoMapperSampleTest
END
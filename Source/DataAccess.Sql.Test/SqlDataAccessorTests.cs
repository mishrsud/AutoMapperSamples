using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Sql.Test
{
	[TestClass]
	public class SqlDataAccessorTests
	{
		public TestContext TestContext { get; set; }

		[ClassInitialize]
		public static void Initialize(TestContext testContext)
		{
			DatabaseHelper.SetupDatabase();
		}

		[TestMethod, TestCategory("Unit")]
		public void NullConnectionString_Throws_ArgumentNullException()
		{
			Action act = () => new SqlDataAccessor(null);

			act.ShouldThrow<ArgumentNullException>();
		}

		[TestMethod, TestCategory("Unit")]
		public void NullQueryOrStoredProc_Throws_ArgumentNullException()
		{
			var sqlDataAccessor = new SqlDataAccessor(ConfigurationManager.AppSettings["DBConnection"]);
			Action act = () => sqlDataAccessor.RetrieveSingleDomainObjectFromDatabase<object>(null);

			act.ShouldThrow<ArgumentNullException>();
		}

		[TestMethod, TestCategory("Integration")]
		public void SqlDataAccessor_ReturnsExpectedCollection_From_Database()
		{
			var connectionString = ConfigurationManager.AppSettings["DBConnection"];
			var sqlDataAccessor = new SqlDataAccessor(connectionString);

			List<Employee> employeesList = sqlDataAccessor.RetrieveDomainObjectCollectionFromDatabase<Employee>("dbo.GetAllEmployees").ToList();

			employeesList
				.Should()
				.NotBeEmpty()
				.And.HaveCount(4)
				.And.ContainItemsAssignableTo<Employee>();
		}

		[TestMethod, TestCategory("Integration")]
		public void SqlDataAccessor_Returns_ExpectedSingleObject_From_Database()
		{
			var connectionString = ConfigurationManager.AppSettings["DBConnection"];
			var sqlDataAccessor = new SqlDataAccessor(connectionString);

			Employee employee = sqlDataAccessor.RetrieveSingleDomainObjectFromDatabase<Employee>(
				"dbo.GetAllEmployeeWithId", 
				new Dictionary<string, object>()
				{
					{"@Id", "EMP1"}
				});

			employee.Should().NotBeNull("because we know the employee exists");
		}

		[TestMethod, TestCategory("Integration")]
		[Ignore] // Does not work yet
		public void SqlDataAccessor_Returns_ExpectedSingleObject_From_Database_WithTextCommand()
		{
			var connectionString = ConfigurationManager.AppSettings["DBConnection"];
			var sqlDataAccessor = new SqlDataAccessor(connectionString);

			Employee employee = sqlDataAccessor.RetrieveSingleDomainObjectFromDatabase<Employee>(
				"SELECT EmployeeId, FirstName ,LastName ,DateOfJoining FROM dbo.Employees WHERE EmployeeId = @Id AND FirstName = @FName",
				new Dictionary<string, object>()
				{
					{"@Id", "EMP1"},
					{"@FName", "John"}
				});

			employee.Should().NotBeNull("because we know the employee exists");
		}

		[ClassCleanup]
		public static void Cleanup()
		{
			DatabaseHelper.ResetDatabase();
		}
	}
}

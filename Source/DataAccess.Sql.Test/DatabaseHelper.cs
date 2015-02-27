using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Subtext.Scripting;

namespace DataAccess.Sql.Test
{
	/// <summary>
	/// Helper class to Prepare and cleanup test database
	/// </summary>
	internal class DatabaseHelper
	{
		/// <summary>
		/// Sets up the database.
		/// </summary>
		internal static void SetupDatabase()
		{
			var connectionString = GetConnectionString();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var createDbStmt = GetTextFromEmbededFile("DataAccess.Sql.Test.Scripts.CreateDb.sql");
				using (var cmd = new SqlCommand(createDbStmt, connection))
				{
					cmd.ExecuteNonQuery();
				}

				var queryString = GetTextFromEmbededFile("DataAccess.Sql.Test.Scripts.SetupDb.sql");
				var runner = new SqlScriptRunner(queryString);

				using (var transaction = connection.BeginTransaction())
				{
					runner.Execute(transaction);
					transaction.Commit();
				}
			}
		}

		/// <summary>
		/// Resets the database i.e. drop the database.
		/// </summary>
		internal static void ResetDatabase()
		{
			var connectionString = GetConnectionString();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var queryString = GetTextFromEmbededFile("DataAccess.Sql.Test.Scripts.DropDb.sql");
				using (var cmd = new SqlCommand(queryString, connection))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}

		/// <summary>
		/// Gets the text from a file included as an embedded resource in the currently 
		/// executing assembly.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		private static string GetTextFromEmbededFile(string fileName)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = fileName;
			var result = string.Empty;

			using (var stream = assembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null) return result;
				var reader = new StreamReader(stream);
				result = reader.ReadToEnd();

			}

			return result;
		}

		/// <summary>
		/// Gets the connection string.
		/// </summary>
		private static string GetConnectionString()
		{
			return ConfigurationManager.AppSettings["ServerConnection"];
		}
	}
}

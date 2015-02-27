using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;

namespace DataAccess.Sql
{
	/// <summary>
	/// A data access provider for MS SQL Server. Uses <see cref="AutoMapper"/> to map data records to object
	/// </summary>
	public class SqlDataAccessor
	{
		private readonly string _connectionString;
		private SqlConnection _sqlConnection;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDataAccessor"/> class.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <exception cref="System.ArgumentNullException">connectionString</exception>
		public SqlDataAccessor(string connectionString)
		{
			if (connectionString == null) throw new ArgumentNullException("connectionString");

			_connectionString = connectionString;
		}

		/// <summary>
		/// Retrieves the domain object from database.
		/// </summary>
		/// <typeparam name="TDomainObject">The type of the domain object.</typeparam>
		/// <param name="storedProcOrQuery">The stored proc or query.</param>
		/// <param name="queryParametersDictionary">The query parameters dictionary.</param>
		/// <returns></returns>
		public IEnumerable<TDomainObject> RetrieveDomainObjectCollectionFromDatabase<TDomainObject>(
			string storedProcOrQuery,
			IDictionary<string, object> queryParametersDictionary = null)
		{
			if (storedProcOrQuery == null) throw new ArgumentNullException("storedProcOrQuery");

			var result = new List<TDomainObject>();

			Mapper.CreateMap<IDataRecord, TDomainObject>();

			if (ConnectToDatabase())
			{
				using (_sqlConnection)
				{
					using (SqlCommand sqlCommand = CreateDbCommand(storedProcOrQuery, queryParametersDictionary))
					{
						using (var dataReader = sqlCommand.ExecuteReader())
						{
							while (dataReader.Read())
							{
								result.Add(Mapper.Map<IDataRecord, TDomainObject>(dataReader));
							}
						}
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Retrieves a single domain object from database.
		/// </summary>
		/// <typeparam name="TDomainObject">The type of the domain object.</typeparam>
		/// <param name="storedProcOrQuery">The stored proc or query.</param>
		/// <param name="queryParametersDictionary">The query parameters dictionary.</param>
		public TDomainObject RetrieveSingleDomainObjectFromDatabase<TDomainObject>(
			string storedProcOrQuery,
			IDictionary<string, object> queryParametersDictionary = null)
		{
			if (storedProcOrQuery == null) throw new ArgumentNullException("storedProcOrQuery");

			TDomainObject result = default(TDomainObject);
			
			Mapper.CreateMap<IDataRecord, TDomainObject>();

			if (ConnectToDatabase())
			{
				using (_sqlConnection)
				{
					using (SqlCommand sqlCommand = CreateDbCommand(storedProcOrQuery, queryParametersDictionary))
					{
						using (var dataReader = sqlCommand.ExecuteReader())
						{
							while (dataReader.Read())
							{
								result = Mapper.Map<IDataRecord, TDomainObject>(dataReader);
							}
						}
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Creates the database command.
		/// </summary>
		/// <param name="storedProcOrQuery">The stored proc or query.</param>
		/// <param name="queryParametersDictionary">The query parameters dictionary.</param>
		/// <returns></returns>
		private SqlCommand CreateDbCommand(
			string storedProcOrQuery,
			ICollection<KeyValuePair<string, object>> queryParametersDictionary)
		{
			if (queryParametersDictionary == null)
			{
				return new SqlCommand(storedProcOrQuery, _sqlConnection);
			}

			var parameters = new SqlParameter[queryParametersDictionary.Count];
			int parameterCounter = 0;

			foreach (KeyValuePair<string, object> parameterNameAndTypePair in queryParametersDictionary)
			{
				parameters[parameterCounter] = new SqlParameter(parameterNameAndTypePair.Key, parameterNameAndTypePair.Value);
				parameterCounter++;
			}

			var sqlCommand = _sqlConnection.CreateCommand();
			sqlCommand.CommandText = storedProcOrQuery;
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddRange(parameters);

			return sqlCommand;
		}

		/// <summary>
		/// Connects to database.
		/// </summary>
		private bool ConnectToDatabase()
		{
			bool result = false;

			try
			{
				_sqlConnection = new SqlConnection(_connectionString);
				_sqlConnection.Open();
				result = true;
			}
			catch (Exception exc)
			{
				// TODO LOG 
				throw;
			}

			return result;
		}
	}
}

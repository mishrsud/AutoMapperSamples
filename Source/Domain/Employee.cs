using System;

namespace Domain
{
	/// <summary>
	/// An Employee Prototype
	/// </summary>
	public class Employee : IEquatable<Employee>
	{
		/// <summary>
		/// Gets or sets the employee identifier.
		/// </summary>
		public string EmployeeId { get; set; }

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets the date of joining.
		/// </summary>
		public DateTime DateOfJoining { get; set; }

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		public bool Equals(Employee other)
		{
			if (other.EmployeeId == this.EmployeeId && other.FirstName == this.FirstName && other.LastName == this.LastName)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Employee))
			{
				return false;
			}

			var other = obj as Employee;

			if (other.EmployeeId == this.EmployeeId && other.FirstName == this.FirstName && other.LastName == this.LastName)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = (int)2166136261;

				hash = hash * 16777619 ^ EmployeeId.GetHashCode();
				hash = hash * 16777619 ^ FirstName.GetHashCode();
				hash = hash * 16777619 ^ LastName.GetHashCode();
				return hash;
			}
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="employee1">The employee1.</param>
		/// <param name="employee2">The employee2.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==(Employee employee1, Employee employee2)
		{
			return employee1.EmployeeId.Equals(employee2.EmployeeId, StringComparison.OrdinalIgnoreCase) &&
				   employee1.FirstName.Equals(employee2.FirstName, StringComparison.OrdinalIgnoreCase) &&
				   employee1.LastName.Equals(employee2.LastName, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="employee1">The employee1.</param>
		/// <param name="employee2">The employee2.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=(Employee employee1, Employee employee2)
		{
			return !(employee1 == employee2);
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			var formattedString = string.Format("Id: {0}, First Name: {1}, Last Name: {2}, Date of Joining {3}", EmployeeId, FirstName, LastName, DateOfJoining.ToString("D"));
			return formattedString;
		}
	}
}

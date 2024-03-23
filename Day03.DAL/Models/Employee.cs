﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Day03.DAL.Models
{
	public enum Gender
	{
		[EnumMember(Value ="Male")]
		Male=1,
		[EnumMember(Value = "Female")]

		Female = 2
	}

	public enum EmpType
	{
		FullTime=1,
		PartTime=2
	}
	public class Employee
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50, ErrorMessage = "Max Length of name is 50 Chars")]
		[MinLength(5, ErrorMessage = "Min Length of name is 5 Chars")]
		public string Name { get; set; }

		[Range(22, 30)]
		public int? Age { get; set; }

		[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
          , ErrorMessage = "Address must be Like 123-Street-City-Country")]
		public string Address { get; set; }

		[DataType(DataType.Currency)]
        public decimal Salary { get; set; }

		[Display(Name="Is Active")]
        public bool IsActive { get; set; }

		[EmailAddress]
        public string Email { get; set; }

		[Display(Name="Phone Number")]
		[Phone]
        public string PhoneNumber { get; set; }

		[Display(Name="Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmpType EmployeeType { get; set; }
        public DateTime CreationDate { get; set; }

		public bool IsDeleted { get; set; } = false;
    }
}
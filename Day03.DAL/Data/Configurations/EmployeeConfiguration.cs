﻿using Day03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03.DAL.Data.Configurations
{
	internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			//Fluent APIs
			builder.Property(E=>E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(E => E.Address).IsRequired();
			builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");
			builder.Property(E => E.Gender).HasConversion(
				(Gender) => Gender.ToString(),
				(GenderAsString) => (Gender)Enum.Parse(typeof(Gender), GenderAsString, true)
				) ;
		}
	}
}

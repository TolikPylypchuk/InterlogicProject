﻿using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace CloudCalendar.Data.Models
{
	public class User : IdentityUser
	{
		[Required(ErrorMessage = "Введіть ім'я")]
		[StringLength(30)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Введіть по-батькові")]
		[StringLength(30)]
		public string MiddleName { get; set; }

		[Required(ErrorMessage = "Введіть прізвище")]
		[StringLength(30)]
		public string LastName { get; set; }

		[NotMapped]
		public string FullName
			=> $"{this.LastName} {this.FirstName} {this.MiddleName}";

		[NotMapped]
		public IList<string> RoleNames { get; set; }

		public override string ToString() => this.FullName;
	}
}

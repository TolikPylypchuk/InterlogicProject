﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace InterlogicProject.DAL.Models
{
	public class User : IdentityUser
	{
		[Required(ErrorMessage = "Please enter the first name")]
		[StringLength(30)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter the middle name")]
		[StringLength(30)]
		public string MiddleName { get; set; }

		[Required(ErrorMessage = "Please enter the last name")]
		[StringLength(30)]
		public string LastName { get; set; }

		[NotMapped]
		public string FullName
			=> $"{this.LastName} {this.FirstName} {this.MiddleName}";
	}
}

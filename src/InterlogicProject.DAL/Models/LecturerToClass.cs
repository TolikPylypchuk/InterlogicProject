﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.LecturersToClasses))]
	public class LecturerToClass : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть викладача")]
		public int LecturerId { get; set; }

		[Required(ErrorMessage = "Вкажіть пару")]
		public int ClassId { get; set; }

		[ForeignKey(nameof(LecturerId))]
		public Lecturer Lecturer { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }
	}
}

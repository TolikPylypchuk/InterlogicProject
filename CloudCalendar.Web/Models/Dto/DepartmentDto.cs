﻿namespace CloudCalendar.Web.Models.Dto
{
	public class DepartmentDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int FacultyId { get; set; }

		public override string ToString() => this.Name;
	}
}

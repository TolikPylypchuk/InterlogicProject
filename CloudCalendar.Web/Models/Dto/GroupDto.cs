﻿namespace CloudCalendar.Web.Models.Dto
{
	public class GroupDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Year { get; set; }
		public int CuratorId { get; set; }

		public override string ToString() => this.Name;
	}
}

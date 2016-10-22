﻿using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class GroupRepository : BaseRepository<Group>
	{
		public GroupRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Groups;
		}
	}
}

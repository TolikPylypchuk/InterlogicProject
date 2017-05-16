﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	[Authorize(Roles = "Student")]
	public class StudentController : Controller
	{
	}
}

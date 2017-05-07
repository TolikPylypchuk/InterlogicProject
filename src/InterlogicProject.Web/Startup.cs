﻿using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using AutoMapper;

using Swashbuckle.AspNetCore.Swagger;

using InterlogicProject.DAL;
using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Infrastructure;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			this.Configuration = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddJsonFile("appsettings.json")
				.Build();
		}

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			Program.EmailDomain =
				this.Configuration["Settings:EmailDomain"];
			Program.DefaultPassword =
				this.Configuration["Settings:DefaultPassword"];

			Program.HomeworksPath =
				this.Configuration["Settings:HomeworksPath"];
			Program.MaterialsPath =
				this.Configuration["Settings:MaterialsPath"];

			services.AddDbContext<AppDbContext>(
				options =>
					options.UseSqlServer(
						this.Configuration.GetConnectionString(
							"DefaultConnection")),
				ServiceLifetime.Scoped);

			services.AddIdentity<User, IdentityRole>(options => {
				options.User.RequireUniqueEmail = true;
				options.User.AllowedUserNameCharacters =
					"abcdefghijklmnopqrstuvwxyz" +
					"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
					"1234567890" +
					"@.-!#$%&'*+-/=?^_`{|}~";

				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
			}).AddEntityFrameworkStores<AppDbContext>()
			  .AddUserValidator<CustomUserValidator>()
			  .AddErrorDescriber<CustomIdentityErrorDescriber>();

			services.AddScoped<
				IRepository<Building>,
				BuildingRepository>();
			services.AddScoped<
				IRepository<Class>,
				ClassRepository>();
			services.AddScoped<
				IRepository<ClassPlace>,
				ClassPlaceRepository>();
			services.AddScoped<
				IRepository<Classroom>,
				ClassroomRepository>();
			services.AddScoped<
				IRepository<Comment>,
				CommentRepository>();
			services.AddScoped<
				IRepository<Department>,
				DepartmentRepository>();
			services.AddScoped<
				IRepository<Faculty>,
				FacultyRepository>();
			services.AddScoped<
				IRepository<Group>,
				GroupRepository>();
			services.AddScoped<
				IRepository<GroupClass>,
				GroupClassRepository>();
			services.AddScoped<
				IRepository<Homework>,
				HomeworkRepository>();
			services.AddScoped<
				IRepository<Lecturer>,
				LecturerRepository>();
			services.AddScoped<
				IRepository<LecturerClass>,
				LecturerClassRepository>();
			services.AddScoped<
				IRepository<Material>,
				MaterialRepository>();
			services.AddScoped<
				IRepository<UserNotification>,
				UserNotificationRepository>();
			services.AddScoped<
				IRepository<Notification>,
				NotificationRepository>();
			services.AddScoped<
				IRepository<Student>,
				StudentRepository>();
			services.AddScoped<
				IRepository<Subject>,
				SubjectRepository>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddMemoryCache();
			services.AddSession();

			services.AddRouting(options =>
			{
				options.LowercaseUrls = true;
			});

			services.AddMvc(options =>
			{
				options.UseCommaDelimitedArrayModelBinding();
			});

			Mapper.Initialize(config =>
			{
				config.CreateMap<Building, BuildingDto>();
				config.CreateMap<Class, ClassDto>();
				config.CreateMap<ClassPlace, ClassPlaceDto>();
				config.CreateMap<Classroom, ClassroomDto>();
				config.CreateMap<Comment, CommentDto>();
				config.CreateMap<Department, DepartmentDto>();
				config.CreateMap<Faculty, FacultyDto>();
				config.CreateMap<Group, GroupDto>();
				config.CreateMap<GroupClass, GroupClassDto>();
				config.CreateMap<Homework, HomeworkDto>();
				config.CreateMap<LecturerClass, LecturerClassDto>();
				config.CreateMap<Material, MaterialDto>();
				config.CreateMap<Subject, SubjectDto>();
				config.CreateMap<User, UserDto>();

				config.CreateMap<Lecturer, LecturerDto>()
					.ForMember(
						dest => dest.FirstName,
						opt => opt.MapFrom(src => src.User.FirstName))
					.ForMember(
						dest => dest.MiddleName,
						opt => opt.MapFrom(src => src.User.MiddleName))
					.ForMember(
						dest => dest.LastName,
						opt => opt.MapFrom(src => src.User.LastName))
					.ForMember(
						dest => dest.FullName,
						opt => opt.MapFrom(src => src.User.FullName))
					.ForMember(
						dest => dest.Email,
						opt => opt.MapFrom(src => src.User.Email));

				config.CreateMap<UserNotification, NotificationDto>()
					.ForMember(
						dest => dest.Text,
						opt => opt.MapFrom(src => src.Notification.Text))
					.ForMember(
						dest => dest.DateTime,
						opt => opt.MapFrom(src => src.Notification.DateTime));

				config.CreateMap<Student, StudentDto>()
					.ForMember(
						dest => dest.FirstName,
						opt => opt.MapFrom(src => src.User.FirstName))
					.ForMember(
						dest => dest.MiddleName,
						opt => opt.MapFrom(src => src.User.MiddleName))
					.ForMember(
						dest => dest.LastName,
						opt => opt.MapFrom(src => src.User.LastName))
					.ForMember(
						dest => dest.FullName,
						opt => opt.MapFrom(src => src.User.FullName))
					.ForMember(
						dest => dest.Email,
						opt => opt.MapFrom(src => src.User.Email));
			});
			
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "Interlogic Project API",
					Description = "An API for the Interlogic Project",
					TermsOfService = "None"
				});

				options.IncludeXmlComments(this.Configuration["Swagger:Path"]);
				options.DescribeAllEnumsAsStrings();
			});
		}

		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory)
		{
			app.UseStaticFiles();
			app.UseIdentity();
			app.UseSession();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseStatusCodePages();
				
				DataInitializer.InitializeDatabaseAsync(
					app.ApplicationServices).Wait();
			}

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
			
			app.UseSwagger(c =>
			{
				c.PreSerializeFilters.Add(
					(swagger, request) =>
					{
						swagger.Host = request.Host.Value;
						swagger.Schemes = new List<string> { "http" };
					});

			});

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
			});
		}
	}
}

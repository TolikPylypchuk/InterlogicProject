﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for materials.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class MaterialsController : Controller
	{
		private IHostingEnvironment env;
		private IRepository<Material> materials;

		/// <summary>
		/// Initializes a new instance of the MaterialsController class.
		/// </summary>
		/// <param name="env">
		/// The hosting environment that this instance will use.
		/// </param>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public MaterialsController(
			IHostingEnvironment env,
			IRepository<Material> repo)
		{
			this.materials = repo;
			this.env = env;
		}

		/// <summary>
		/// Gets all materials from the database.
		/// </summary>
		/// <returns>All materials from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<MaterialDto>))]
		public IEnumerable<MaterialDto> GetAll()
			=> this.materials.GetAll()?.ProjectTo<MaterialDto>();

		/// <summary>
		/// Gets a material with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the material to get.</param>
		/// <returns>A material with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(MaterialDto))]
		public MaterialDto GetById([FromRoute] int id)
			=> Mapper.Map<MaterialDto>(this.materials.GetById(id));

		/// <summary>
		/// Gets all materials with the specified class.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <returns>All materials with the specified class.</returns>
		[HttpGet("classId/{classId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<MaterialDto>))]
		public IEnumerable<MaterialDto> GetForClass([FromRoute] int classId)
			=> this.materials.GetAll()
						    ?.Where(m => m.ClassId == classId)
							 .ProjectTo<MaterialDto>();

		/// <summary>
		/// Adds a new material to the database.
		/// </summary>
		/// <param name="file">The file to add.</param>
		/// <param name="classId">The class related to the material.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost("classId/{classId}")]
		[SwaggerResponse(201)]
		public async Task<IActionResult> Post(
			[FromBody] IFormFile file,
			[FromRoute] int classId)
		{
			if (file == null || classId <= 0)
			{
				return this.BadRequest();
			}
			
			var material = this.materials.GetAll().FirstOrDefault(
				m => m.FileName == file.FileName);

			if (material != null)
			{
				return this.Forbid();
			}

			string filePath = Path.Combine(
				this.env.WebRootPath,
				Program.MaterialsPath,
				$"{classId}_{file.FileName}");

			using (var stream = System.IO.File.Open(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			var materialToAdd = new Material
			{
				ClassId = classId,
				FileName = file.FileName
			};

			this.materials.Add(materialToAdd);
			
			return this.CreatedAtAction(
				nameof(this.GetById),
				new { id = materialToAdd.Id },
				Mapper.Map<MaterialDto>(materialToAdd));
		}

		/// <summary>
		/// Deletes a material.
		/// </summary>
		/// <param name="id">The ID of the material to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Delete([FromRoute] int id)
		{
			var materialToDelete = this.materials.GetById(id);

			if (materialToDelete == null)
			{
				return this.NotFound();
			}

			this.materials.Delete(materialToDelete);

			System.IO.File.Delete(
				Path.Combine(
					this.env.WebRootPath,
					Program.MaterialsPath,
					$"{materialToDelete.ClassId}_{materialToDelete.FileName}"));

			return this.NoContent();
		}
	}
}

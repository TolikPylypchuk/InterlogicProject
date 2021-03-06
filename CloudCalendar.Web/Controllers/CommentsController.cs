﻿using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using CloudCalendar.Data.Models;
using CloudCalendar.Data.Repositories;
using CloudCalendar.Web.Models.Dto;

namespace CloudCalendar.Web.Controllers
{
	/// <summary>
	/// An API for comments.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class CommentsController : Controller
	{
		private IRepository<Comment> comments;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="CommentsController"/> class.
		/// </summary>
		/// <param name="repo">
		/// The repository of comments that this instance will use.
		/// </param>
		public CommentsController(IRepository<Comment> repo)
		{
			this.comments = repo;
		}

		/// <summary>
		/// Gets all comments from the database.
		/// </summary>
		/// <returns>All comments from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetAll()
			=> this.comments.GetAll()?.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets a comment with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the comment to get.</param>
		/// <returns>A comment with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(CommentDto))]
		public CommentDto GetById([FromRoute] int id)
			=> Mapper.Map<CommentDto>(this.comments.GetById(id));

		/// <summary>
		/// Gets all comments with the specified class.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <returns>All comments with the specified class.</returns>
		[HttpGet("classId/{classId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForClass([FromRoute] int classId)
			=> this.comments.GetAll()
						   ?.Where(c => c.ClassId == classId)
							.OrderBy(c => c.DateTime)
							.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets the specified amount of comments with the specified class.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <param name="num">The amount of comments.</param>
		/// <returns>
		/// The specified amount of comments with the specified class.
		/// </returns>
		[HttpGet("classId/{classId}/take/{num}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForClass(
			[FromRoute] int classId,
			[FromRoute] int num)
			=> this.comments.GetAll()
						   ?.Where(c => c.ClassId == classId)
							.OrderBy(c => c.DateTime)
							.Take(num)
							.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets all comments with the specified user.
		/// </summary>
		/// <param name="userId">The ID of the user.</param>
		/// <returns>All comments with the specified user.</returns>
		[HttpGet("userId/{userId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForUser([FromRoute] string userId)
			=> this.comments.GetAll()
						   ?.Where(c => c.UserId == userId)
							.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets all comments with the specified class and user.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <param name="userId">The ID of the user.</param>
		/// <returns>All comments with the specified class and user.</returns>
		[HttpGet("classId/{classId}/userId/{userId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForClassAndUser(
			[FromRoute] int classId,
			[FromRoute] string userId)
			=> this.comments.GetAll()
						   ?.Where(c => c.ClassId == classId)
							.Where(c => c.UserId == userId)
							.OrderBy(c => c.DateTime)
							.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets the specified amount of comments
		/// with the specified class and user.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <param name="userId">The ID of the user.</param>
		/// <param name="num">The amount of comments.</param>
		/// <returns>
		/// The specified amount of comments with the specified class and user.
		/// </returns>
		[HttpGet("classId/{classId}/userId/{userId}/take/{num}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForClassAndUser(
			[FromRoute] int classId,
			[FromRoute] string userId,
			[FromRoute] int num)
			=> this.comments.GetAll()
						   ?.Where(c => c.ClassId == classId)
							.Where(c => c.UserId == userId)
							.OrderBy(c => c.DateTime)
							.Take(num)
							.ProjectTo<CommentDto>();

		/// <summary>
		/// Adds a new comment to the database.
		/// </summary>
		/// <param name="commentDto">The comment to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		public IActionResult Post([FromBody] CommentDto commentDto)
		{
			if (commentDto?.Text == null ||
				commentDto.UserId == null ||
				commentDto.ClassId == 0 ||
				commentDto.DateTime == default(DateTime))
			{
				return this.BadRequest();
			}

			var commentToAdd = new Comment
			{
				ClassId = commentDto.ClassId,
				UserId = commentDto.UserId,
				Text = commentDto.Text,
				DateTime = commentDto.DateTime
			};

			this.comments.Add(commentToAdd);

			commentDto.Id = commentToAdd.Id;

			return this.CreatedAtAction(
				nameof(this.GetById), new { id = commentDto.Id }, commentDto);
		}

		/// <summary>
		/// Updates a comment.
		/// </summary>
		/// <param name="id">The ID of the comment to update.</param>
		/// <param name="commentDto">The comment to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Put(
			[FromRoute] int id,
			[FromBody] CommentDto commentDto)
		{
			if (commentDto?.Text == null)
			{
				return this.BadRequest();
			}

			var commentToUpdate = this.comments.GetById(id);

			if (commentToUpdate == null)
			{
				return this.NotFound();
			}

			commentToUpdate.Text = commentDto.Text;
			this.comments.Update(commentToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a comment.
		/// </summary>
		/// <param name="id">The ID of the comment to update.</param>
		/// <param name="commentDto">The comment to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Patch(
			[FromRoute] int id,
			[FromBody] CommentDto commentDto)
		{
			if (commentDto?.Text == null)
			{
				return this.BadRequest();
			}

			var commentToUpdate = this.comments.GetById(id);

			if (commentToUpdate == null)
			{
				return this.NotFound();
			}

			commentToUpdate.Text = commentDto.Text;
			this.comments.Update(commentToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a comment.
		/// </summary>
		/// <param name="id">The ID of the comment to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Delete([FromRoute] int id)
		{
			var commentToDelete = this.comments.GetById(id);

			if (commentToDelete == null)
			{
				return this.NotFound();
			}

			this.comments.Delete(commentToDelete);

			return this.NoContent();
		}
	}
}

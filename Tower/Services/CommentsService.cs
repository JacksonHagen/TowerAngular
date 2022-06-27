using System;
using System.Collections.Generic;
using Tower.Models;
using Tower.Repositories;

namespace Tower.Services
{
	public class CommentsService
	{
		private readonly CommentsRepository _repo;

		public CommentsService(CommentsRepository repo)
		{
			_repo = repo;
		}

		internal Comment Create(Comment commentData)
		{
			return _repo.Create(commentData);
		}

		internal List<Comment> GetCommentsByEvent(int id)
		{
			return _repo.GetCommentsByEvent(id);
		}
	}
}
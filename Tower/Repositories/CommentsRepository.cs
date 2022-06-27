using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Tower.Models;

namespace Tower.Repositories
{
	public class CommentsRepository
	{
		private readonly IDbConnection _db;

		public CommentsRepository(IDbConnection db)
		{
			_db = db;
		}

		internal Comment Create(Comment commentData)
		{
			string sql = @"
			INSERT INTO comments
			(creatorId, body, isAttending, eventId)
			VALUES
			(@CreatorId, @Body, @IsAttending, @EventId);
			SELECT LAST_INSERT_ID();
			";
			commentData.Id = _db.ExecuteScalar<int>(sql, commentData);
			return commentData;
		}

		internal List<Comment> GetCommentsByEvent(int id)
		{
			string sql = @"
			SELECT
				a.*,
				c.*
			FROM comments c
			JOIN accounts a ON a.id = c.creatorId
			WHERE c.eventId = @id;
			";
			return _db.Query<Profile, Comment, Comment>(sql, (p, c) =>
			{
				c.Creator = p;
				return c;
			}, new { id }).ToList();
		}
	}
}
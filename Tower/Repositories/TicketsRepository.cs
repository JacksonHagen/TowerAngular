using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Tower.Models;

namespace Tower.Repositories
{
	public class TicketsRepository
	{
		private readonly IDbConnection _db;

		public TicketsRepository(IDbConnection db)
		{
			_db = db;
		}

		internal Ticket GetByTicketId(int id)
		{
			string sql = "SELECT * FROM tickets WHERE id = @id;";
			return _db.QueryFirstOrDefault<Ticket>(sql, new { id });
		}

		internal Ticket Create(Ticket ticketData)
		{
			string sql = @"
			INSERT INTO tickets
			(eventId, creatorId)
			VALUES
			(@EventId, @CreatorId);
			SELECT LAST_INSERT_ID();
			";
			ticketData.Id = _db.ExecuteScalar<int>(sql, ticketData);
			return ticketData;
		}

		internal List<Ticket> GetTicketsByEvent(int id)
		{
			string sql = @"
			SELECT
				a.*,
				t.*,
				t.id as ticketId
			FROM tickets t
			JOIN accounts a ON t.creatorId = a.id
			WHERE t.eventId = @id
			";
			return _db.Query<Profile, Ticket, Ticket>(sql, (p, t) =>
			{
				t.Creator = p;
				return t;
			}, new { id }).ToList();
		}

		internal List<Ticket> GetTicketsByAccount(string id)
		{
			string sql = @"
			SELECT
				a.*,
				t.*,
				t.id as ticketId
			FROM tickets t
			JOIN accounts a ON t.creatorId = a.id
			WHERE t.creatorId = @id
			";
			return _db.Query<Profile, Ticket, Ticket>(sql, (p, t) =>
			{
				t.Creator = p;
				return t;
			}, new { id }).ToList();
		}

		internal void Delete(int id)
		{
			string sql = "DELETE FROM tickets WHERE id = @id LIMIT 1";
			_db.Execute(sql, new { id });
		}
	}
}
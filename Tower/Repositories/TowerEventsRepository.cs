using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Tower.Models;

namespace Tower.Repositories
{
	public class TowerEventsRepository
	{
		private readonly IDbConnection _db;

		public TowerEventsRepository(IDbConnection db)
		{
			_db = db;
		}

		internal List<TowerEvent> Get()
		{
			string sql = @"
				SELECT
					t.*,
					a.*
				FROM towerevents t
				JOIN accounts a ON t.creatorId = a.id
			";
			return _db.Query<TowerEvent, Profile, TowerEvent>(sql, (t, p) =>
			{
				t.Creator = p;
				return t;
			}).ToList();
		}

		internal TowerEvent Get(int id)
		{
			string sql = @"
				SELECT
					t.*,
					a.*,
				FROM towerevents t
				JOIN accounts a ON t.creatorId = a.id
				WHERE t.id = @id
			";
			return _db.Query<TowerEvent, Profile, TowerEvent>(sql, (t, p) =>
			{
				t.Creator = p;
				return t;
			}).FirstOrDefault();
		}
	}
}
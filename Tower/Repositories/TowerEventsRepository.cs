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
					a.*
				FROM towerevents t
				JOIN accounts a ON t.creatorId = a.id
				WHERE t.id = @id
			";
			return _db.Query<TowerEvent, Profile, TowerEvent>(sql, (t, p) =>
			{
				t.Creator = p;
				return t;
			}, new { id }).FirstOrDefault();
		}

		internal TowerEvent Create(TowerEvent eventData)
		{
			string sql = @"
			INSERT INTO towerevents
			(creatorId, name, description, coverImg, location, capacity, startDate, isCanceled, type)
			VALUES
			(@CreatorId, @Name, @Description, @CoverImg, @Location, @Capacity, @StartDate, @IsCanceled, @Type);
			SELECT LAST_INSERT_ID();
			";
			eventData.Id = _db.ExecuteScalar<int>(sql, eventData);
			return eventData;
		}

		internal void Edit(TowerEvent target)
		{
			string sql = @"
			UPDATE towerevents
			SET
				name = @Name,
				description = @Description,
				coverImg = @CoverImg,
				location = @Location,
				capacity = @Capacity,
				startDate = @StartDate,
				type = @Type
			WHERE id = @Id
			LIMIT 1;
			";
			_db.Execute(sql, target);
		}

		internal void Cancel(int id)
		{
			string sql = @"
			UPDATE towerevents
			SET
				isCanceled = 1
			WHERE id = @id
			LIMIT 1;
			";
			_db.Execute(sql, new { id });
		}
	}
}
using System;
using System.Collections.Generic;
using Tower.Models;
using Tower.Repositories;

namespace Tower.Services
{
	public class TowerEventsService
	{
		private readonly TowerEventsRepository _repo;

		public TowerEventsService(TowerEventsRepository repo)
		{
			_repo = repo;
		}

		internal List<TowerEvent> Get()
		{
			return _repo.Get();
		}

		internal TowerEvent Get(int id)
		{
			TowerEvent found = _repo.Get(id);
			if (found == null)
			{
				throw new Exception("Could not find event");
			}
			return found;
		}

		internal TowerEvent Create(TowerEvent eventData)
		{
			TowerEvent 
		}
	}
}
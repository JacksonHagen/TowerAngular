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
			return _repo.Create(eventData);
		}

		internal TowerEvent Edit(TowerEvent updateData)
		{
			TowerEvent target = Get(updateData.Id);
			IsOwner(target.CreatorId, updateData.CreatorId);
			target.Name = updateData.Name ?? target.Name;
			target.Description = updateData.Description ?? target.Description;
			target.CoverImg = updateData.CoverImg ?? target.CoverImg;
			target.Location = updateData.Location ?? target.Location;
			target.Capacity = updateData.Capacity;
			target.StartDate = updateData.StartDate ?? target.StartDate;
			target.Type = updateData.Type ?? target.Type;
			_repo.Edit(target);
			return target;
		}
		internal void Cancel(int id, string userId)
		{
			TowerEvent target = Get(id);
			IsOwner(target.CreatorId, userId);
			if (!target.IsCanceled)
			{
				_repo.Cancel(id);
			}
			else
			{
				throw new Exception("This event has already been canceled");
			}
		}
		private void IsOwner(string id1, string id2)
		{
			if (id1 != id2)
			{
				throw new Exception("You do not have permission to modify this event");
			}
		}


	}
}
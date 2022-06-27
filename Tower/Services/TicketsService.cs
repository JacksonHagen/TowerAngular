using System;
using System.Collections.Generic;
using Tower.Models;
using Tower.Repositories;

namespace Tower.Services
{
	public class TicketsService
	{
		private readonly TicketsRepository _repo;

		public TicketsService(TicketsRepository repo)
		{
			_repo = repo;
		}

		internal Ticket GetByTicketId(int id)
		{
			Ticket found = _repo.GetByTicketId(id);
			if (found == null)
			{
				throw new Exception("Could not find ticket");
			}
			return found;
		}

		internal List<Ticket> GetTicketsByEvent(int id)
		{
			return _repo.GetTicketsByEvent(id);
		}

		internal Ticket Create(Ticket ticketData)
		{
			return _repo.Create(ticketData);
		}

		internal void Delete(int id, string userId)
		{
			Ticket target = GetByTicketId(id);
			if (target.CreatorId != userId)
			{
				throw new Exception("You are not authorized to delete this ticket");
			}
			_repo.Delete(id);
		}

		internal List<Ticket> GetTicketsByAccount(string id)
		{
			return _repo.GetTicketsByAccount(id);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tower.Models;
using Tower.Services;

namespace Tower.Controllers
{
	[ApiController]
	[Route("api/events")]
	public class TowerEventsController : ControllerBase
	{
		private readonly TowerEventsService _tes;
		private readonly TicketsService _ts;
		private readonly CommentsService _cs;

		public TowerEventsController(TowerEventsService tes, TicketsService ts, CommentsService cs)
		{
			_tes = tes;
			_ts = ts;
			_cs = cs;
		}

		[HttpGet]
		public ActionResult<List<TowerEvent>> Get()
		{
			try
			{
				List<TowerEvent> events = _tes.Get();
				return Ok(events);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpGet("{id}")]
		public ActionResult<TowerEvent> Get(int id)
		{
			try
			{
				TowerEvent towerEvent = _tes.Get(id);
				return Ok(towerEvent);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<TowerEvent>> Create([FromBody] TowerEvent eventData)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				eventData.CreatorId = userInfo.Id;
				TowerEvent newEvent = _tes.Create(eventData);
				newEvent.Creator = userInfo;
				return Ok(newEvent);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<ActionResult<TowerEvent>> Edit([FromBody] TowerEvent updateData, int id)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				updateData.CreatorId = userInfo.Id;
				updateData.Id = id;
				TowerEvent updatedTowerEvent = _tes.Edit(updateData);
				return Ok(updatedTowerEvent);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult<String>> Cancel(int id)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				_tes.Cancel(id, userInfo.Id);
				return Ok("Event Canceled");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}/tickets")]
		public ActionResult<List<Ticket>> GetTicketsByEvent(int id)
		{
			try
			{
				List<Ticket> tickets = _ts.GetTicketsByEvent(id);
				return Ok(tickets);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}/comments")]
		public ActionResult<List<Comment>> GetCommentsByEvent(int id)
		{
			try
			{
				List<Comment> comments = _cs.GetCommentsByEvent(id);
				return Ok(comments);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
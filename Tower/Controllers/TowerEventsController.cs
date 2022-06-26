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
	[Route("api/[controller]")]
	public class TowerEventsController : ControllerBase
	{
		private readonly TowerEventsService _tes;

		public TowerEventsController(TowerEventsService tes)
		{
			_tes = tes;
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
				TowerEvent newEvent = _tes.Create(eventData);
				newEvent.Creator = userInfo;
				return Ok(newEvent);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
using System;
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
	public class TicketsController : ControllerBase
	{
		private readonly TicketsService _ts;

		public TicketsController(TicketsService ts)
		{
			_ts = ts;
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Ticket>> Create([FromBody] Ticket ticketData)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				ticketData.CreatorId = userInfo.Id;
				Ticket newTicket = _ts.Create(ticketData);
				newTicket.Creator = userInfo;
				return Ok(newTicket);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult<String>> Delete(int id)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				_ts.Delete(id, userInfo.Id);
				return Ok("Ticket Deleted");
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
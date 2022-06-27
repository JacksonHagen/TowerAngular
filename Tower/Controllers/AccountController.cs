using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tower.Models;
using Tower.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tower.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly AccountService _accountService;
		private readonly TicketsService _ts;

		public AccountController(AccountService accountService, TicketsService ts)
		{
			_accountService = accountService;
			_ts = ts;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<Account>> Get()
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				return Ok(_accountService.GetOrCreateProfile(userInfo));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpGet("tickets")]
		[Authorize]
		public async Task<ActionResult<List<Ticket>>> GetTicketsByAccount() {
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				List<Ticket> tickets = _ts.GetTicketsByAccount(userInfo.Id);
				return Ok(tickets);
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}


}
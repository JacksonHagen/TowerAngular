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
	public class CommentsController : ControllerBase
	{
		private readonly CommentsService _cs;

		public CommentsController(CommentsService cs)
		{
			_cs = cs;
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Comment>> Create([FromBody] Comment commentData)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				commentData.CreatorId = userInfo.Id;
				Comment newComment = _cs.Create(commentData);
				newComment.Creator = userInfo;
				return Ok(newComment);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
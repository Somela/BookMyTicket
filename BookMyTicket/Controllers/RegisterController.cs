using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyTicket.DataAccess;
using BookMyTicket.Interface;
using BookMyTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyTicket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class RegisterController: ControllerBase
	{
		private readonly IRegisterService _registerService;
		private readonly LinqDbContext _linqDbContext;
		public RegisterController(IRegisterService registerService, LinqDbContext linqDbContext)
		{
			_registerService = registerService;
			_linqDbContext = linqDbContext;
		}
		[Route("register")]
		[HttpPost]
		public async Task<RegisterResponse> InsertUserInformation([FromBody] RegisterRequest registerRequest)
		{
			RegisterResponse registerResponse = new RegisterResponse();
			if (ModelState.IsValid)
			{
				registerResponse = _registerService.RegisterUserResponse(registerRequest, _linqDbContext);
			}
			return registerResponse;
		}
	}
}

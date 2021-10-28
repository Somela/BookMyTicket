using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyTicket.DataAccess;
using BookMyTicket.Models;

namespace BookMyTicket.Interface
{
	public interface IRegisterService
	{
		RegisterResponse RegisterUserResponse(RegisterRequest registerRequest, LinqDbContext linqDbContext);
	}
}

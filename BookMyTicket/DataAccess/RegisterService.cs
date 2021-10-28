using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyTicket.Interface;
using BookMyTicket.Models;
using LinqToDB;

namespace BookMyTicket.DataAccess
{
	public class RegisterService : IRegisterService
	{
		public RegisterResponse RegisterUserResponse(RegisterRequest registerRequest, LinqDbContext linqDbContext)
		{
			RegisterResponse registerResponse = new RegisterResponse();
			try
			{
				linqDbContext.UserRegister.Add(registerRequest);
				//linqDbContext.Add(registerRequest);
				linqDbContext.SaveChanges();
				registerResponse.Message = "User Registered Successfully";
				registerResponse.StatusCode = 201;
			}
			catch(LinqToDBException ex)
			{
				throw ex;
			}
			return registerResponse;
		}
	}
}

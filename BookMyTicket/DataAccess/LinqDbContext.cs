using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyTicket.Interface;
using BookMyTicket.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyTicket.DataAccess
{
	public class LinqDbContext: DbContext
	{
		public LinqDbContext(DbContextOptions<LinqDbContext> options) : base(options)
		{
		}
		public DbSet<RegisterRequest> UserRegister { get; set; }
	}
}

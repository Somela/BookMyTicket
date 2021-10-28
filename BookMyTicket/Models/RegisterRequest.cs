using System.ComponentModel.DataAnnotations;

namespace BookMyTicket.Models
{
	public class RegisterRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[Key]
		public string EmailId { get; set; }
		public string Password { get; set; }
		public int MobileNumber { get; set; }
		public int RoleId { get; set; }
	}
}

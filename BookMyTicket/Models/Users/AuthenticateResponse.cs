using BookMyTicket.Entities;

namespace BookMyTicket.Models.Users
{
    public class AuthenticateResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public Role RoleId { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(RegisterRequest user, string token)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailId = user.EmailId;
            Role = user.Role;
            Token = token;
        }
    }
}
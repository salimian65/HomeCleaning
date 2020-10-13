using System;

namespace HomeCleaning.Service.Contract
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NationalCode { get; set; }

        public string Cellphone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

using System;

namespace Framework.Domain
{
	public class UserDto
	{
		public UserDto(Guid userId, string username, int organizationId)
		{
			UserId = userId;
			Username = username;
			OrganizationId = organizationId;
		}

		public Guid UserId { get; }

		public string Username { get; }

		public int OrganizationId { get; }
	}
}
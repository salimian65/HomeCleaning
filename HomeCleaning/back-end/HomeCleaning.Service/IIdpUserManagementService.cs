using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeCleaning.Service
{
    public interface IIdpUserManagementService
    {
        Task<IEnumerable<User>> GetUsers(string searchQuery, int first = 0, int count = 25);

        Task<User> GetUserByUsername(string username);

        Task<User> GetUserById(Guid id);

        Task<User> CreateUser(RegisteringUserDto user);

        Task EnableUser(Guid id);

        Task DisableUser(Guid id);

        Task RemoveUser(Guid id);
    }
}
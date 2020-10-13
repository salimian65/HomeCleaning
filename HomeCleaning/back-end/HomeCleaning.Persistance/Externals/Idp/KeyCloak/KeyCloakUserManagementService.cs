using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Framework.Domain.Exceptions;
using HomeCleaning.Domain;
using Infrastructures.DataAccess.Externals.Idp.KeyCloak;
using RestSharp;

namespace HomeCleaning.Persistance.Externals.Idp.KeyCloak
{
    public class KeyCloakUserManagementService : RestCaller, IIdpUserManagementService
    {
        public KeyCloakUserManagementService(string serverUrl = "http://localhost:8080/auth/admin/realms/homecleaning/users")
            : base(serverUrl)
        {
        }

        public async Task<User> CreateUser(RegisteringUserDto user)
        {
            var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(user);
            IRestResponse response = await Execute(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Accepted:
                case HttpStatusCode.Created:
                    return await GetUserByUsername(user.Username);
                case HttpStatusCode.BadRequest:
                    throw new BadDataException(response.StatusDescription);
                case HttpStatusCode.Conflict:
                    throw new DuplicateException(response.StatusDescription);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<IEnumerable<User>> GetUsers(string searchQuery, int first = 0, int count = 25)
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            if (!string.IsNullOrWhiteSpace(searchQuery))
                request.AddQueryParameter("search", searchQuery);
            request.AddQueryParameter("first", first.ToString());
            request.AddQueryParameter("max", count.ToString());
            request.AddQueryParameter("briefRepresentation", "false");
            List<AuthUserDto> users = await Execute<List<AuthUserDto>>(request);
            return users.Select(u => new User(u.Id, u.Username)).ToList();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("username", username);
            List<AuthUserDto> idpUser = await Execute<List<AuthUserDto>>(request);
            return new User(idpUser[0].Id, idpUser[0].Username);
        }

        public async Task<User> GetUserById(Guid id)
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("id", id.ToString());
            List<AuthUserDto> idpUser = await Execute<List<AuthUserDto>>(request);
            return new User(idpUser[0].Id, idpUser[0].Username);
        }

        public async Task EnableUser(Guid id)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = id.ToString();
            request.AddJsonBody(new {enabled = true});
            IRestResponse response = await Execute(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent: break;
                case HttpStatusCode.BadRequest:
                    throw new BadDataException(response.StatusDescription);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task DisableUser(Guid id)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = id.ToString();
            request.AddJsonBody(new {enabled = false});
            IRestResponse response = await Execute(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent: break;
                case HttpStatusCode.BadRequest:
                    throw new BadDataException(response.StatusDescription);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task RemoveUser(Guid id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = id.ToString();
            IRestResponse response = await Execute(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent: return;
                case HttpStatusCode.NotFound:
                    throw new NotFoundException(response.StatusDescription);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
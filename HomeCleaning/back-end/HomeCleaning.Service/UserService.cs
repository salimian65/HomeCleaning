using System;
using System.Threading.Tasks;
using Framework.Domain;
using Framework.Domain.Exceptions;
using HomeCleaning.Domain;
using HomeCleaning.Domain.Repository;
using HomeCleaning.Persistance.DataAccess;
using HomeCleaning.Persistance.Externals.Idp;
using HomeCleaning.Service.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace HomeCleaning.Service
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserService : BaseService
    {
        private readonly ILogger<UserService> logger;
        private readonly IUserRepository userRepository;
        private readonly IIdpUserManagementService idpUserManagement;
        private readonly IUnitOfWork unitOfWork;


        public UserService(IUnitOfWork unitOfWork,
                           IUserRepository userRepository,
                           IIdpUserManagementService idpUserManagement,
                           ILogger<UserService> logger,
                           IUserContext userContext) : base(userContext)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.idpUserManagement = idpUserManagement;
            this.userRepository = userRepository;

        }

        public async Task Create(UserDto userDto)
        {
            RegisteringUserDto registeringUserDto = new RegisteringUserDto
            {
                Id = Guid.NewGuid(),
                Username = userDto.Username,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Attributes = new AttributesDto("OrganizationName", "OrganizationId"),
                Credentials = new[] { new CredentialDto(userDto.Password) }
            };

            User user = await idpUserManagement.CreateUser(registeringUserDto);
            user.FirstNameSetter(userDto.FirstName);
            user.LastnameSetter(userDto.LastName);
            user.NationalCodeSetter(userDto.NationalCode);
            user.CellphoneSetter(userDto.Cellphone);
            user.EmailSetter(userDto.Email);
            await userRepository.Add(user);
            await unitOfWork.CommitAsync();
        }

        public async Task<AuthUserDto> GetUser(string username)
        {
            var authUserDto = new AuthUserDto();
            var user = await idpUserManagement.GetUserByUsername(username);
            return new AuthUserDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        public async Task<AuthUserDto> GetUserById(Guid userId)
        {
            var authUserDto = new AuthUserDto();
            var user = await userRepository.Get().FirstOrDefaultAsync();
            //  return authUserDto.FromEntity(user);
            return new AuthUserDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        public async Task Remove(Guid id)
        {
            User user = await userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException("user not found");
            await idpUserManagement.RemoveUser(id);
        }

        public async Task Enable(Guid id)
        {
            User user = await userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException("user not found");
            await idpUserManagement.EnableUser(id);
        }

        public async Task Disable(Guid id)
        {
            User user = await userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException("user not found");
            await idpUserManagement.DisableUser(id);
        }

        public void ChangePassword()
        {
        }

        public void GetbyId()
        {
        }

        public void GetByPersonId()
        {
        }

        public void GetAll()
        {
        }
    }
}
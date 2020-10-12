using System;
using System.Threading.Tasks;
using Framework.Domain;
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
        private readonly NaturalPersonService naturalPersonService;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository,
            IIdpUserManagementService idpUserManagement, NaturalPersonService naturalPersonService,
            ILogger<UserService> logger,
            IUserContext userContext) : base( userContext)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.idpUserManagement = idpUserManagement;
            this.userRepository = userRepository;
            this.naturalPersonService = naturalPersonService;
        }

        public async Task Create(UserManipulationDto userManipulationDto)
        {
            NaturalPerson person = await naturalPersonService.GetPersonById(userManipulationDto.PersonId);
            if (person == null)
            {
                throw new NotFoundException("person not found!");
            }

            RegisteringUserDto registeringUserDto = new RegisteringUserDto(Guid.NewGuid(), userManipulationDto.Username,
                person.FirstName, person.Surname,
                person.Email, "OrganizationName", "OrganizationId", userManipulationDto.Password);
            User user = await idpUserManagement.CreateUser(registeringUserDto);
            person.AddNewUser(user);
            await unitOfWork.CommitAsync();
        }

        public async Task<UserDto> GetUser(string username)
        {
            var user = await idpUserManagement.GetUserByUsername(username);
            return UserDto.FromEntity(user);
        }

        public async Task<UserDto> GetUserById(Guid userId)
        {
            var user = await userRepository.Get().Include(u => u.Person)
                .Include(u => u.Organization).FirstOrDefaultAsync();
            return UserDto.FromEntity(user);
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
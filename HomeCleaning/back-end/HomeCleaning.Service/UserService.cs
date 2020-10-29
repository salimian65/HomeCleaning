using System.Threading.Tasks;
using Framework.Domain;
using HomeCleaning.Domain;
using HomeCleaning.Persistance.DataAccess;
using HomeCleaning.Persistance.Email;
using HomeCleaning.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace HomeCleaning.Service
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserService : BaseService
    {
        private readonly ILogger<UserService> logger;
        private UserManager<ApplicationUser> userManager;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IUserValidator<ApplicationUser> userValidator;
        //  private readonly IUserRepository userRepository;
        // private readonly IIdpUserManagementService idpUserManagement;
        private readonly IUnitOfWork unitOfWork;


        public UserService(IUnitOfWork unitOfWork,
                           // IUserRepository userRepository,
                           // IIdpUserManagementService idpUserManagement,
                           ILogger<UserService> logger,
                           IUserContext userContext,
                          UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher, IPasswordValidator<ApplicationUser> passwordValidator, IUserValidator<ApplicationUser> userValidator) : base(userContext)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.passwordValidator = passwordValidator;
            this.userValidator = userValidator;
            this.unitOfWork = unitOfWork;
            // this.idpUserManagement = idpUserManagement;
            // this.userRepository = userRepository;

        }

        public async Task Create(UserDto dto)
        {
            ApplicationUser appUser = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Cellphone = dto.Cellphone
            };

            IdentityResult result = await userManager.CreateAsync(appUser, dto.Password);
            if (result.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                var confirmationLink =
                    "ConfirmEmail/Email"; //Url.Action("ConfirmEmail", "Email", new { token, email = dto.Email }, Request.Scheme);
                EmailHelper emailHelper = new EmailHelper();
                bool emailResponse = emailHelper.SendEmail(dto.Email, confirmationLink);
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    //  ModelState.AddModelError("", error.Description);
                }

                //RegisteringUserDto registeringUserDto = new RegisteringUserDto
                //{
                //    Id = Guid.NewGuid(),
                //    Username = userDto.Username,
                //    FirstName = userDto.FirstName,
                //    LastName = userDto.LastName,
                //    Email = userDto.Email,
                //    Attributes = new AttributesDto("OrganizationName", "OrganizationId"),
                //    Credentials = new[] { new CredentialDto(userDto.Password) }
                //};

                //User user = await idpUserManagement.CreateUser(registeringUserDto);
                //user.FirstNameSetter(userDto.FirstName);
                //user.LastnameSetter(userDto.LastName);
                //user.NationalCodeSetter(userDto.NationalCode);
                //user.CellphoneSetter(userDto.Cellphone);
                //user.EmailSetter(userDto.Email);
                //await userRepository.Add(user);
                //await unitOfWork.CommitAsync();
            }
        }
        //public async Task<AuthUserDto> GetUser(string username)
            //{
            //    var authUserDto = new AuthUserDto();
            //    var user = await idpUserManagement.GetUserByUsername(username);
            //    return new AuthUserDto
            //    {
            //        Id = user.Id,
            //        Username = user.Username
            //    };
            //}

            //public async Task<AuthUserDto> GetUserById(Guid userId)
            //{
            //    var authUserDto = new AuthUserDto();
            //    var user = await userRepository.Get().FirstOrDefaultAsync();
            //    //  return authUserDto.FromEntity(user);
            //    return new AuthUserDto
            //    {
            //        Id = user.Id,
            //        Username = user.Username
            //    };
            //}

            //public async Task Remove(Guid id)
            //{
            //    User user = await userRepository.GetById(id);
            //    if (user == null)
            //        throw new NotFoundException("user not found");
            //    await idpUserManagement.RemoveUser(id);
            //}

            //public async Task Enable(Guid id)
            //{
            //    User user = await userRepository.GetById(id);
            //    if (user == null)
            //        throw new NotFoundException("user not found");
            //    await idpUserManagement.EnableUser(id);
            //}

            //public async Task Disable(Guid id)
            //{
            //    User user = await userRepository.GetById(id);
            //    if (user == null)
            //        throw new NotFoundException("user not found");
            //    await idpUserManagement.DisableUser(id);
            //}

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
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly IPasswordValidator<ApplicationUser> passwordValidator;
        private readonly IUserValidator<ApplicationUser> userValidator;
        //  private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork,
                           // IUserRepository userRepository,
                           ILogger<UserService> logger,
                           IUserContext userContext,
                           UserManager<ApplicationUser> userManager,
                           IPasswordHasher<ApplicationUser> passwordHasher, 
                           IPasswordValidator<ApplicationUser> passwordValidator, 
                           IUserValidator<ApplicationUser> userValidator,
                           RoleManager<IdentityRole> roleManager) : base(userContext)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.passwordValidator = passwordValidator;
            this.userValidator = userValidator;
            this.roleManager = roleManager;
            this.unitOfWork = unitOfWork;
            // this.userRepository = userRepository;

        }

        public async Task Create(UserDto dto,string roleName)
        {
            ApplicationUser appUser = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Cellphone = dto.Cellphone
            };

            var role = roleManager.FindByNameAsync(roleName).Result;
            IdentityResult user = await userManager.CreateAsync(appUser, dto.Password);
            if (!userManager.IsInRoleAsync(appUser, role.Name).Result)
            {
                _ = userManager.AddToRoleAsync(appUser, role.Name).Result;
            }
            if (user.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
               // var confirmationLink = @"<a target=""_blank"" href=""http://localhost:8080/emailconfirmation?email={}&token={}"">Home cleaning email confirmation</a>";
                var confirmationLink = $"<a target=\"_blank\" " +
                                        $"href=\"http://localhost:8080/emailconfirmation?email={dto.Email}&token={token}\">" +
                                        $"Welcame to Home Cleaning. it is the email confirmation</a>";
                //Url.Action("ConfirmEmail", "Email", new { token, email = dto.Email }, Request.Scheme);
                EmailHelper emailHelper = new EmailHelper();
                bool emailResponse = emailHelper.SendEmail(dto.Email, confirmationLink);
            }
            else
            {
                foreach (IdentityError error in user.Errors)
                {
                    //  ModelState.AddModelError("", error.Description);
                }
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
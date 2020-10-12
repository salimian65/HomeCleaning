using Framework.Domain;

namespace HomeCleaning.Service
{
    public class BaseService
    {
        protected readonly IUserContext userContext;
      

        public BaseService( IUserContext userContext)
        {
          
            this.userContext = userContext;
        }
    }
}
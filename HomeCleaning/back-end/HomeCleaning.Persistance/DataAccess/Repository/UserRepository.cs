using HomeCleaning.Domain;
using HomeCleaning.Domain.Repository;

namespace HomeCleaning.Persistance.DataAccess.Repository
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HomeCleaningContext context) : base(context)
        {
        }
    }
}
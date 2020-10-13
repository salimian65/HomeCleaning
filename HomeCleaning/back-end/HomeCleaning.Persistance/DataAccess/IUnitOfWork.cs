using System.Threading.Tasks;

namespace HomeCleaning.Persistance.DataAccess
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}
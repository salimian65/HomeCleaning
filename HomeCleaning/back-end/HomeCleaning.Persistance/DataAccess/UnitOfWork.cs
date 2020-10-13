using System.Threading.Tasks;
using HomeCleaning.Persistance.Helper;
using Infrastructures.Helper;
using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.Persistance.DataAccess
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly HomeCleaningContext context;
    

        public UnitOfWork(HomeCleaningContext context)
        {
            this.context = context;
           
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }
    }
}
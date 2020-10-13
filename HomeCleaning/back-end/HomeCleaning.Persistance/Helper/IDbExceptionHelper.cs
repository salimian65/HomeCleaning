using Framework.Domain.Exceptions;
using Infrastructures.Helper;
using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.Persistance.Helper
{
    public interface IDbExceptionHelper
    {
        ExceptionResult Translate(DbUpdateException ex);
    
        BusinessException TranslateToException(DbUpdateException ex);
    }
}
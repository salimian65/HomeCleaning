namespace Framework.Domain.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
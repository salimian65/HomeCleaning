namespace Framework.Domain
{
    public interface IUserContext
    {
        UserDto CurrentUser { get; }
        
    }
}
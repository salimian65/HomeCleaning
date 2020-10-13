namespace Framework.Domain
{
    public interface IUserContext
    {
        UserPrincipalDto CurrentUserPrincipal { get; }
        
    }
}
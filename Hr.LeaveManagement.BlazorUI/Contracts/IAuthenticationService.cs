namespace Hr.LeaveManagement.BlazorUI.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync (string email, string password);
        Task<bool> RegisterAsync (string firstNmae, string lastName,string userName,string email,string password);
        Task Logout();
    }
}

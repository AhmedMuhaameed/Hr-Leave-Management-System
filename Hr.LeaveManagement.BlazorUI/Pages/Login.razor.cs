using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models;
using Hr.LeaveManagement.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hr.LeaveManagement.BlazorUI.Pages
{
    public partial class Login
    {
        public LoginVM Model { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Message { get; set; }
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        public Login()
        {

        }
        protected override async Task OnInitializedAsync()
        {
            Model = new LoginVM();
        }

        protected async Task HandleLogin()
        {
            if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Message = "Username or password combination unknown";

            }
        }


    }
}
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models;
using Hr.LeaveManagement.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hr.LeaveManagement.BlazorUI.Pages
{
    public partial class Register
    {
        public RegisterVM  Model { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Message { get; set; }
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Model = new RegisterVM();
        }

        public Register()
        {

        }
        protected async Task HandleRegister()
        {
            var result = await AuthenticationService.RegisterAsync(Model.FirstName, Model.LastName, Model.UserName, Model.Email, Model.Password);
            if(result)
            {
                NavigationManager.NavigateTo("/");
            }
            Message = "SomeThing went wrong, please try again.";
        }


    }
}
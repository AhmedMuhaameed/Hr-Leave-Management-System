using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models;
using Hr.LeaveManagement.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hr.LeaveManagement.BlazorUI.Pages
{
    public partial class Logout
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }

        public Logout()
        {

        }


    }
}
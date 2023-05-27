using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models;
using Hr.LeaveManagement.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;

namespace Hr.LeaveManagement.BlazorUI.Shared
{
    public partial class RedirectToLogin
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public RedirectToLogin()
        {

        }
        protected override async Task OnInitializedAsync()
        {
            NavigationManager.NavigateTo("login");
        }


    }
}
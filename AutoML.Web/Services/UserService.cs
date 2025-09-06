using AutoML.Web.Interfaces;
using AutoML.Web.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AutoML.Web.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider authStateProvider;

        public UserService(AuthenticationStateProvider authStateProvider)
        {
            this.authStateProvider = authStateProvider;
        }

        public async Task<UserViewModel?> GetCurrentUserAsync()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = user.FindFirst(ClaimTypes.Name)?.Value;

            if (id == null || name == null)
            {
                return null;
            }

            return new UserViewModel
            {
                Id = id,
                Name = name
            };
        }
    }
}

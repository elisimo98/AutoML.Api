using AutoML.Web.Models;

namespace AutoML.Web.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel?> GetCurrentUserAsync();
    }
}

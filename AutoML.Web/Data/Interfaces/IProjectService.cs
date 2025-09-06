using AutoML.Web.Models.Data;

namespace AutoML.Web.Data.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectByUserIdAsync(string userId);
        Task<Project> CreateProjectAsync(string name, string description, string userId);
    }
}

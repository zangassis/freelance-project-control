using FreelanceProjectControl.Models;

namespace FreelanceProjectControl.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllProjects();
        Task<Project> GetProjectById(string id);
        Task<bool> CreateProject(Project project);
        Task<bool> UpdateProject(Project project);
        Task<bool> DeleteProject(string id);
    }
}
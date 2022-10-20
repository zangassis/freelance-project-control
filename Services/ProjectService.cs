using FreelanceProjectControl.Data;
using FreelanceProjectControl.Models;

namespace FreelanceProjectControl.Services;
public class ProjectService : IProjectService
{
    private readonly ProjectRepository _repository;

    public ProjectService(ProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Project>> GetAllProjects() =>
    await _repository.GetAll();

    public async Task<Project> GetProjectById(string id) =>
        await _repository.GetById(id);

    public async Task<bool> CreateProject(Project project) =>
        await _repository.Create(project);

    public async Task<bool> UpdateProject(Project project) =>
        await _repository.Update(project);

    public async Task<bool> DeleteProject(string id) =>
        await _repository.Delete(id);
}

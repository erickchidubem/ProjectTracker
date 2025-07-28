using ProjectTracker.Application.Interfaces;
using ProjectTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Application.Services;

public class ProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public Task<IEnumerable<Project>> GetAllProjectsAsync() => _projectRepository.GetAllAsync();
    public Task<Project?> GetProjectByIdAsync(int id) => _projectRepository.GetByIdAsync(id);
    public Task<Project> CreateProjectAsync(Project project) => _projectRepository.AddAsync(project);
    public Task UpdateProjectAsync(Project project) => _projectRepository.UpdateAsync(project);
    public Task DeleteProjectAsync(Project project) => _projectRepository.DeleteAsync(project);
}

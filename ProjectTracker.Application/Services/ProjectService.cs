using ProjectTracker.Application.Interfaces;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public async Task<Project> CreateProjectAsync(Project project)
    {
        SanitizeProject(project);
        return await _projectRepository.AddAsync(project);
    }
    public async Task UpdateProjectAsync(Project project)
    {
        SanitizeProject(project);
        await _projectRepository.UpdateAsync(project);
    }
    public Task DeleteProjectAsync(Project project) => _projectRepository.DeleteAsync(project);

    private static void SanitizeProject(Project project)
    {
        project.Name = WebUtility.HtmlEncode(project.Name);
        if (!string.IsNullOrEmpty(project.Description))
            project.Description = WebUtility.HtmlEncode(project.Description);
    }

    public async Task<(IEnumerable<Project> Items, int TotalCount)> GetProjectsPagedAsync(int pageNumber, int pageSize)
    {
        return await _projectRepository.GetPagedAsync(pageNumber, pageSize);
    }

    public Task<IEnumerable<Project>> GetProjectsByStatusAsync(ProjectStatus status)
    {
        return _projectRepository.GetByStatusAsync(status);
    }

}

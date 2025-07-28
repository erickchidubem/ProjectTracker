using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Application.DTOs;
using ProjectTracker.Application.Services;
using ProjectTracker.Domain.Entities;

namespace ProjectTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly IMapper _mapper;

    public ProjectsController(ProjectService projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(_mapper.Map<IEnumerable<ProjectDto>>(projects));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null) return NotFound();
        return Ok(_mapper.Map<ProjectDto>(project));
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateUpdateProjectDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var project = _mapper.Map<Project>(dto);
        var created = await _projectService.CreateProjectAsync(project);

        var result = _mapper.Map<ProjectDto>(created);
        return CreatedAtAction(nameof(GetProject), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProject(int id, [FromBody] CreateUpdateProjectDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existing = await _projectService.GetProjectByIdAsync(id);
        if (existing == null) return NotFound();

        _mapper.Map(dto, existing);
        await _projectService.UpdateProjectAsync(existing);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(int id)
    {
        var existing = await _projectService.GetProjectByIdAsync(id);
        if (existing == null) return NotFound();

        await _projectService.DeleteProjectAsync(existing);
        return NoContent();
    }
}

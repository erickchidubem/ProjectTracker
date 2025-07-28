using ProjectTracker.Application.Services;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Infrastructure.Repositories;
using ProjectTracker.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Tests.Services;

public class ProjectServiceTests
{
    [Fact]
    public async Task CreateProjectAsync_ShouldSaveProject()
    {
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repo = new ProjectRepository(context);
        var service = new ProjectService(repo);

        var project = new Project
        {
            Name = "Service Project",
            Description = "Service Layer",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(5)
        };

        var created = await service.CreateProjectAsync(project);
        var all = await service.GetAllProjectsAsync();

        Assert.NotNull(created);
        Assert.Equal("Service Project", all.First().Name);
    }
}

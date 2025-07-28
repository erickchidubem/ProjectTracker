using ProjectTracker.Domain.Entities;
using ProjectTracker.Infrastructure.Repositories;
using ProjectTracker.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Tests.Repository;

public class ProjectRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldAddProject()
    {
        // Arrange
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new ProjectRepository(context);

        var project = new Project
        {
            Name = "Test Project",
            Description = "Test Description",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(10)
        };

        // Act
        var result = await repository.AddAsync(project);
        var all = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(all);
        Assert.Equal("Test Project", all.First().Name);
    }
}

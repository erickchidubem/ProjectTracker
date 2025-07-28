using ProjectTracker.Domain.Enums;
using ProjectTracker.Domain.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Project name is required")]
    [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Start date is required")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required")]
    [DateGreaterThan("StartDate", ErrorMessage = "End date must be after start date")]
    public DateTime EndDate { get; set; }

    public ProjectStatus Status { get; set; } = ProjectStatus.Planned;
}
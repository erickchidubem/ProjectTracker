using ProjectTracker.Domain.Enums;
using ProjectTracker.Domain.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Application.DTOs;

public class CreateUpdateProjectDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    [DateGreaterThan("StartDate", ErrorMessage = "EndDate must be greater than StartDate.")]
    public DateTime EndDate { get; set; }

    public ProjectStatus Status { get; set; } = ProjectStatus.Planned;
}
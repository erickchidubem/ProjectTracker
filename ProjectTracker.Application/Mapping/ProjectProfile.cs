using AutoMapper;
using ProjectTracker.Application.DTOs;
using ProjectTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Application.Mapping;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<Project, CreateUpdateProjectDto>().ReverseMap();
    }
}

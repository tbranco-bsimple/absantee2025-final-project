using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;

namespace Infrastructure;

public class DataModelMappingProfile : Profile
{
    public DataModelMappingProfile()
    {
        CreateMap<ISprint, SprintDataModel>();
        CreateMap<SprintDataModel, ISprint>()
            .ConvertUsing<SprintDataModelConverter>();
        CreateMap<IProject, ProjectDataModel>();
        CreateMap<ProjectDataModel, IProject>()
            .ConvertUsing<ProjectDataModelConverter>();
    }
}
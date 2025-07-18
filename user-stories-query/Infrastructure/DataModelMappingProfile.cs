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
        CreateMap<IUserStory, UserStoryDataModel>();
        CreateMap<UserStoryDataModel, IUserStory>()
            .ConvertUsing<UserStoryDataModelConverter>();
    }
}
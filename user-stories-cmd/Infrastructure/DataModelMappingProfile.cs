using AutoMapper;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;

namespace Infrastructure;

public class DataModelMappingProfile : Profile
{
    public DataModelMappingProfile()
    {
        CreateMap<UserStory, UserStoryDataModel>();
        CreateMap<UserStoryDataModel, UserStory>()
            .ConvertUsing<UserStoryDataModelConverter>();
    }

}
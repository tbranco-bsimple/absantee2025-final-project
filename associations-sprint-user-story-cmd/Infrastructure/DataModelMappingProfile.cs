using AutoMapper;
using Domain.Interfaces;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;

namespace Infrastructure;

public class DataModelMappingProfile : Profile
{
    public DataModelMappingProfile()
    {
        CreateMap<IAssociationSprintUserStory, AssociationSprintUserStoryDataModel>();
        CreateMap<AssociationSprintUserStoryDataModel, IAssociationSprintUserStory>()
            .ConvertUsing<AssociationSprintUserStoryDataModelConverter>();
        CreateMap<ISprint, SprintDataModel>();
        CreateMap<SprintDataModel, ISprint>()
            .ConvertUsing<SprintDataModelConverter>();
        CreateMap<IUserStory, UserStoryDataModel>();
        CreateMap<UserStoryDataModel, IUserStory>()
            .ConvertUsing<UserStoryDataModelConverter>();
        CreateMap<ICollaborator, CollaboratorDataModel>();
        CreateMap<CollaboratorDataModel, ICollaborator>()
            .ConvertUsing<CollaboratorDataModelConverter>();
    }
}
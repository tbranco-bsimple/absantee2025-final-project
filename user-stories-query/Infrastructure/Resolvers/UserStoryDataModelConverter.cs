using AutoMapper;
using Domain.Interfaces;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class UserStoryDataModelConverter : ITypeConverter<UserStoryDataModel, IUserStory>
{
    private readonly IUserStoryFactory _factory;

    public UserStoryDataModelConverter(IUserStoryFactory factory)
    {
        _factory = factory;
    }

    public IUserStory Convert(UserStoryDataModel source, IUserStory destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class UserStoryDataModelConverter : ITypeConverter<UserStoryDataModel, UserStory>
{
    private readonly IUserStoryFactory _factory;

    public UserStoryDataModelConverter(IUserStoryFactory factory)
    {
        _factory = factory;
    }

    public UserStory Convert(UserStoryDataModel source, UserStory destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class AssociationSprintUserStoryDataModelConverter : ITypeConverter<AssociationSprintUserStoryDataModel, IAssociationSprintUserStory>
{
    private readonly IAssociationSprintUserStoryFactory _factory;

    public AssociationSprintUserStoryDataModelConverter(IAssociationSprintUserStoryFactory factory)
    {
        _factory = factory;
    }

    public IAssociationSprintUserStory Convert(AssociationSprintUserStoryDataModel source, IAssociationSprintUserStory destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
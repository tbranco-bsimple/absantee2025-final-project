using AutoMapper;
using Domain.Interfaces;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class SprintDataModelConverter : ITypeConverter<SprintDataModel, ISprint>
{
    private readonly ISprintFactory _factory;

    public SprintDataModelConverter(ISprintFactory factory)
    {
        _factory = factory;
    }

    public ISprint Convert(SprintDataModel source, ISprint destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
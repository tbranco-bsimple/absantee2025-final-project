using AutoMapper;
using Domain.Interfaces;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class ProjectDataModelConverter : ITypeConverter<ProjectDataModel, IProject>
{
    private readonly IProjectFactory _factory;

    public ProjectDataModelConverter(IProjectFactory factory)
    {
        _factory = factory;
    }

    public IProject Convert(ProjectDataModel source, IProject destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
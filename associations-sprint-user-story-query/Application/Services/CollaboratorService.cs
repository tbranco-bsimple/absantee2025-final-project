using Application.DTOs;
using Application.Interfaces;
using Domain.Factory;
using Domain.IRepository;
using Infrastructure.DataModel;

namespace Application.Services;

public class CollaboratorService : ICollaboratorService
{
    private ICollaboratorRepository _collaboratorRepository;
    private ICollaboratorFactory _collaboratorFactory;

    public CollaboratorService(ICollaboratorRepository collaboratorRepository, ICollaboratorFactory collaboratorFactory)
    {
        _collaboratorRepository = collaboratorRepository;
        _collaboratorFactory = collaboratorFactory;
    }

    public async Task AddConsumed(CreateCollaboratorFromMessageDTO collaboratorDTO)
    {
        var collaborator = await _collaboratorRepository.GetByIdAsync(collaboratorDTO.Id);

        if (collaborator != null)
        {
            Console.WriteLine($"CollaboratorConsumed not added, already exists with Id: {collaboratorDTO.Id}");
            return;
        }

        var collaboratorVisitor = new CollaboratorDataModel()
        {
            Id = collaboratorDTO.Id,
            Period = collaboratorDTO.Period,
        };

        var newCollaborator = _collaboratorFactory.Create(collaboratorVisitor);
        await _collaboratorRepository.AddAsync(newCollaborator);
    }
}
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceAdapters.Controllers;

[Route("api/associations-sprint-userstory")]
[ApiController]
public class AssociationSprintUserStoryController : ControllerBase
{
    private readonly IAssociationSprintUserStoryService _associationSprintUserStoryService;

    public AssociationSprintUserStoryController(IAssociationSprintUserStoryService associationSprintUserStoryService)
    {
        _associationSprintUserStoryService = associationSprintUserStoryService;
    }

    [HttpPost]
    public async Task<ActionResult<CreatedAssociationSprintUserStoryDTO>> Create([FromBody] CreateAssociationSprintUserStoryDTO associationSprintUserStoryDTO)
    {
        var associationSprintUserStoryCreatedDTO = await _associationSprintUserStoryService.Create(associationSprintUserStoryDTO);

        return associationSprintUserStoryCreatedDTO.ToActionResult();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<UpdatedAssociationSprintUserStoryDTO>> EditEffortCompletion(Guid id, [FromBody] UpdateEffortAndCompletionDTO updateEffortAndCompletionDTO)
    {
        var associationSprintUserStoryUpdatedDTO = await _associationSprintUserStoryService.UpdateEffortCompletion(id, updateEffortAndCompletionDTO);

        return associationSprintUserStoryUpdatedDTO.ToActionResult();
    }
}
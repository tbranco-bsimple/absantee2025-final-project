using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceAdapters.Controllers;

[Route("api/associations-sprint-user-story")]
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
}
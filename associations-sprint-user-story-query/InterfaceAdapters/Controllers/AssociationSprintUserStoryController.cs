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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssociationSprintUserStoryDTO>>> GetAll()
    {
        var associationsSprintUserStoryDTO = await _associationSprintUserStoryService.GetAll();

        return associationsSprintUserStoryDTO.ToActionResult();
    }
}
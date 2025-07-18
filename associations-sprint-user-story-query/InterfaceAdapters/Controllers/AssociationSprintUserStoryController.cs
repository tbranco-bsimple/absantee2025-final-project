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

    [HttpGet("{id}")]
    public async Task<ActionResult<AssociationSprintUserStoryDTO>> GetById(Guid id)
    {
        var associationSprintUserStoryDTO = await _associationSprintUserStoryService.GetById(id);

        return associationSprintUserStoryDTO.ToActionResult();
    }

    [HttpGet("sprint/{sprintId}")]
    public async Task<ActionResult<IEnumerable<UserStoryDTO>>> GetAllUserStoriesOfSprint(Guid sprintId)
    {
        var userStoriesDTO = await _associationSprintUserStoryService.GetAllUserStoriesOfSprint(sprintId);

        return userStoriesDTO.ToActionResult();
    }

    [HttpGet("sprint/{sprintId}/userstory/{userStoryId}")]
    public async Task<ActionResult<UserStoryDTO>> GetUserStoryOfSprint(Guid sprintId, Guid userStoryId)
    {
        var userStoryOfSprintDTO = await _associationSprintUserStoryService.GetUserStoryOfSprint(sprintId, userStoryId);

        return userStoryOfSprintDTO.ToActionResult();
    }
}
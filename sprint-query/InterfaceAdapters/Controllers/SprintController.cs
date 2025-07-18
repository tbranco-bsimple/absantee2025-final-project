using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceAdapters.Controllers;

[Route("api/sprints")]
[ApiController]
public class SprintController : ControllerBase
{
    private readonly ISprintService _sprintService;

    public SprintController(ISprintService sprintService)
    {
        _sprintService = sprintService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SprintDTO>>> GetAll()
    {
        var sprintsDTO = await _sprintService.GetAll();

        return sprintsDTO.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SprintDTO>> GetAll(Guid id)
    {
        var sprintDTO = await _sprintService.GetById(id);

        return sprintDTO.ToActionResult();
    }

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<SprintDTO>>> GetAllByProjectId(Guid projectId)
    {
        var sprintsDTO = await _sprintService.GetAllByProjectId(projectId);

        return sprintsDTO.ToActionResult();
    }
}
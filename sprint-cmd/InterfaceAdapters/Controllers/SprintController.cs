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

    [HttpPost]
    public async Task<ActionResult<CreatedSprintDTO>> Create([FromBody] CreateSprintDTO sprintDTO)
    {
        var sprintCreatedDTO = await _sprintService.Create(sprintDTO);

        return sprintCreatedDTO.ToActionResult();
    }
}
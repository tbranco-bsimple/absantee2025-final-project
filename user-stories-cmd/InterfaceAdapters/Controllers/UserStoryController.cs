using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceAdapters.Controllers;

[Route("api/userstories")]
[ApiController]
public class UserStoryController : ControllerBase
{
    private readonly IUserStoryService _userStoryService;

    public UserStoryController(IUserStoryService userStoryService)
    {
        _userStoryService = userStoryService;
    }

    [HttpPost]
    public async Task<ActionResult<CreatedUserStoryDTO>> Create([FromBody] CreateUserStoryDTO userStoryDTO)
    {
        var usCreatedDTO = await _userStoryService.Create(userStoryDTO);

        return usCreatedDTO.ToActionResult();
    }
}
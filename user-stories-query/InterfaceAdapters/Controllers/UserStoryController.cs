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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserStoryDTO>>> GetAll()
    {
        var userStoriesDTO = await _userStoryService.GetAll();

        return userStoriesDTO.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserStoryDTO>> GetById(Guid id)
    {
        var userStoryDTO = await _userStoryService.GetById(id);

        return userStoryDTO.ToActionResult();
    }
}
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs;

public record CreateUserStoryDTO
{
    public string Description { get; set; }

    [Range(1, 4)]
    public Priority Priority { get; set; }

    [Range(1, 4)]
    public Risk Risk { get; set; }
}
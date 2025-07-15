using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserStoryRepository : IUserStoryRepository
{
    protected readonly UserStoriesContext _context;
    private readonly IMapper _mapper;

    public UserStoryRepository(UserStoriesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IUserStory> AddAsync(IUserStory userStory)
    {
        var userStoryDataModel = _mapper.Map<UserStory, UserStoryDataModel>((UserStory)userStory);

        await _context.Set<UserStoryDataModel>().AddAsync(userStoryDataModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserStoryDataModel, UserStory>(userStoryDataModel);
    }

    public async Task<IUserStory?> GetByIdAsync(Guid id)
    {
        var userStoryDataModel = await _context.Set<UserStoryDataModel>()
            .FirstOrDefaultAsync(us => us.Id == id);

        if (userStoryDataModel == null)
            return null;

        return _mapper.Map<UserStoryDataModel, UserStory>(userStoryDataModel);
    }
}
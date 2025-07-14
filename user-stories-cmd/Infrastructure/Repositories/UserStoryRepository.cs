using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;

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

    public async Task<IUserStory> AddAsync(IUserStory us)
    {
        var usDataModel = _mapper.Map<UserStory, UserStoryDataModel>((UserStory)us);

        _context.Set<UserStoryDataModel>().Add(usDataModel);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserStoryDataModel, UserStory>(usDataModel);
    }
}
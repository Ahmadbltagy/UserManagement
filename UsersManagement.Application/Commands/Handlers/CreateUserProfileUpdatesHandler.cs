using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class CreateUserProfileUpdatesHandler : IRequestHandler<CreateUserProfileUpdatesCommand, bool>
{
    private IUserProfileUpdatesRepository _userProfileUpdatesRepository;
    
    public CreateUserProfileUpdatesHandler(IUserProfileUpdatesRepository userProfileUpdatesRepository)
    {
        _userProfileUpdatesRepository = userProfileUpdatesRepository;
    }
    
    public async Task<bool> Handle(CreateUserProfileUpdatesCommand request, CancellationToken cancellationToken)
    {
        await _userProfileUpdatesRepository.AddAsync(request.UserProfileUpdates);
        return await _userProfileUpdatesRepository.SaveChanagesAsync();
    }
}
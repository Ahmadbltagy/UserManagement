using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class CreateUserProfileUpdatesHandler : IRequestHandler<CreateUserProfileUpdatesCommand, bool>
{
    private IUserProfilePendingUpdatesRepository _iUserProfilePendingUpdatesRepository;
    
    public CreateUserProfileUpdatesHandler(IUserProfilePendingUpdatesRepository iUserProfilePendingUpdatesRepository)
    {
        _iUserProfilePendingUpdatesRepository = iUserProfilePendingUpdatesRepository;
    }
    
    public async Task<bool> Handle(CreateUserProfileUpdatesCommand request, CancellationToken cancellationToken)
    {
        await _iUserProfilePendingUpdatesRepository.AddAsync(request.UserProfilePendingUpdates);
        return await _iUserProfilePendingUpdatesRepository.SaveChanagesAsync();
    }
}
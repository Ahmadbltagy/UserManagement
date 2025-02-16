using MediatR;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries.Handlers;

public class GetUserProfileUpdateByIdHandler : IRequestHandler<GetUserProfileUpdateByIdQuery, UserProfilePendingUpdates?>
{
    private readonly IUserProfilePendingUpdatesRepository _iUserProfilePendingUpdatesRepository;

    public GetUserProfileUpdateByIdHandler(IUserProfilePendingUpdatesRepository iUserProfilePendingUpdatesRepository)
    {
        _iUserProfilePendingUpdatesRepository = iUserProfilePendingUpdatesRepository;
    }

    public async Task<UserProfilePendingUpdates?> Handle(GetUserProfileUpdateByIdQuery request, CancellationToken cancellationToken)
    {
        return await _iUserProfilePendingUpdatesRepository.GetByIdAsync(request.RequestId);
    }
}
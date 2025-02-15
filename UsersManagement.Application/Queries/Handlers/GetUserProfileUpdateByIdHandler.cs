using MediatR;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries.Handlers;

public class GetUserProfileUpdateByIdHandler : IRequestHandler<GetUserProfileUpdateByIdQuery, UserProfileUpdates?>
{
    private readonly IUserProfileUpdatesRepository _userProfileUpdatesRepository;

    public GetUserProfileUpdateByIdHandler(IUserProfileUpdatesRepository userProfileUpdatesRepository)
    {
        _userProfileUpdatesRepository = userProfileUpdatesRepository;
    }

    public async Task<UserProfileUpdates?> Handle(GetUserProfileUpdateByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userProfileUpdatesRepository.GetByIdAsync(request.RequestId);
    }
}
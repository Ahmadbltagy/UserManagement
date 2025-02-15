using MediatR;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries.Handlers;

public class GetUserProfileHandler : IRequestHandler<GetUserProfileByUserIdQuery, UserProfile?>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public GetUserProfileHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<UserProfile?> Handle(GetUserProfileByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _userProfileRepository.GetByUserId(request.UserId);
    }
}
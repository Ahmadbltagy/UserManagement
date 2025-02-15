using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class ApproveUserProfileUpdateRequestHandler : IRequestHandler<ApproveUserProfileUpdateRequestCommand, bool>
{
    private readonly IUserProfileUpdatesRepository _userProfileUpdatesRepository;
    

    public ApproveUserProfileUpdateRequestHandler(IUserProfileUpdatesRepository userProfileUpdatesRepository)
    {
        _userProfileUpdatesRepository = userProfileUpdatesRepository;
    }

    public Task<bool> Handle(ApproveUserProfileUpdateRequestCommand request, CancellationToken cancellationToken)
    {
       request.UserProfileUpdates.IsApproved = true;
       request.UserProfileUpdates.ApprovalDate = DateTime.Now;
       _userProfileUpdatesRepository.Update(request.UserProfileUpdates);
       
       return Task.FromResult(true);
    }
}
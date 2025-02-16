using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class ApproveUserProfileUpdateRequestHandler : IRequestHandler<ApproveUserProfileUpdateRequestCommand, bool>
{
    private readonly IUserProfilePendingUpdatesRepository _iUserProfilePendingUpdatesRepository;
    

    public ApproveUserProfileUpdateRequestHandler(IUserProfilePendingUpdatesRepository iUserProfilePendingUpdatesRepository)
    {
        _iUserProfilePendingUpdatesRepository = iUserProfilePendingUpdatesRepository;
    }

    public Task<bool> Handle(ApproveUserProfileUpdateRequestCommand request, CancellationToken cancellationToken)
    {
       request.UserProfilePendingUpdates.IsApproved = true;
       request.UserProfilePendingUpdates.ApprovalDate = DateTime.Now;
       _iUserProfilePendingUpdatesRepository.Update(request.UserProfilePendingUpdates);
       
       return Task.FromResult(true);
    }
}
using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, bool>
{
    private IUserProfileRepository _userProfileRepository;

    public UpdateUserProfileHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }


    public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        request.UserProfile.FirstName = request.UpdateUserProfileDto.FirstName ?? request.UserProfile.FirstName;
        request.UserProfile.LastName = request.UpdateUserProfileDto.LastName ?? request.UserProfile.LastName;
        request.UserProfile.DateOfBirth = request.UpdateUserProfileDto.DateOfBirth?? request.UserProfile.DateOfBirth;
        
        _userProfileRepository.Update(request.UserProfile);
        return await _userProfileRepository.SaveChanagesAsync();

    }
}
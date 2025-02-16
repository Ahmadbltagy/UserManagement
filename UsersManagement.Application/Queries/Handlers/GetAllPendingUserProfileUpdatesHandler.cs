using MediatR;
using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Queries.Handlers;

public class GetAllPendingUserProfileUpdatesHandler : IRequestHandler<GetAllPendingUserProfileUpdatesQuery, PaginatedResponse<UserProfilePendingUpdatesResponseDto>>
{
    private readonly IUserProfilePendingUpdatesRepository _iUserProfilePendingUpdatesRepository;
    
    public GetAllPendingUserProfileUpdatesHandler(IUserProfilePendingUpdatesRepository iUserProfilePendingUpdatesRepository)
    {
        _iUserProfilePendingUpdatesRepository = iUserProfilePendingUpdatesRepository;
    }
    public async Task<PaginatedResponse<UserProfilePendingUpdatesResponseDto>> Handle(GetAllPendingUserProfileUpdatesQuery request, CancellationToken cancellationToken)
    {
        var (data,totalCount) = await _iUserProfilePendingUpdatesRepository.GetAllPendingUpdates(request.PageNumber, request.PageSize);
        
        var userProfileDto  = data.Select(p=>new UserProfilePendingUpdatesResponseDto()
        {
            RequestId = p.RequestId,
            FirstName = p.FirstName,
            LastName = p.LastName,
            InsertionDate = p.InsertionDate,
            UserId = p.UserId,
            DateOfBirth = p.DateOfBirth,
        });
   

        
        return new PaginatedResponse<UserProfilePendingUpdatesResponseDto>(userProfileDto, totalCount, request.PageNumber, request.PageSize);
        
    }
    
    
}
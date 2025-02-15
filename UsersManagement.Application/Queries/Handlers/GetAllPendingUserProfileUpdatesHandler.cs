using MediatR;
using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Queries.Handlers;

public class GetAllPendingUserProfileUpdatesHandler : IRequestHandler<GetAllPendingUserProfileUpdatesQuery, PaginatedResponse<UserProfileUpdatesResponseDto>>
{
    private readonly IUserProfileUpdatesRepository _userProfileUpdatesRepository;
    
    public GetAllPendingUserProfileUpdatesHandler(IUserProfileUpdatesRepository userProfileUpdatesRepository)
    {
        _userProfileUpdatesRepository = userProfileUpdatesRepository;
    }
    public async Task<PaginatedResponse<UserProfileUpdatesResponseDto>> Handle(GetAllPendingUserProfileUpdatesQuery request, CancellationToken cancellationToken)
    {
        var (data,totalCount) = await _userProfileUpdatesRepository.GetAllPendingUpdates(request.PageNumber, request.PageSize);
        
        var userProfileDto  = data.Select(p=>new UserProfileUpdatesResponseDto()
        {
            RequestId = p.RequestId,
            FirstName = p.FirstName,
            LastName = p.LastName,
            InsertionDate = p.InsertionDate,
            UserId = p.UserId,
            DateOfBirth = p.DateOfBirth,
        });
   

        
        return new PaginatedResponse<UserProfileUpdatesResponseDto>(userProfileDto, totalCount, request.PageNumber, request.PageSize);
        
    }
    
    
}
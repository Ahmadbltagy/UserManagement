using MediatR;
using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Queries;

public class GetAllPendingUserProfileUpdatesQuery : IRequest<PaginatedResponse<UserProfilePendingUpdatesResponseDto>>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    
    public GetAllPendingUserProfileUpdatesQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    
}
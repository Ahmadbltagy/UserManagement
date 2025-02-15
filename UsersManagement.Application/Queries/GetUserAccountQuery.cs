using MediatR;
using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Queries;

public class GetUserAccountQuery : IRequest<PaginatedResponse<UserAccountWithProfileDto>>
{
    public bool IsActive { get; set; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public GetUserAccountQuery(int pageNumber, int pageSize, bool isActive)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        IsActive = isActive;
    }
}
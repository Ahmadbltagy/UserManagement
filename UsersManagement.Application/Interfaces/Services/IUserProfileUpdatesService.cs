using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Interfaces.Services;

public interface IUserProfileUpdatesService
{
    Task<PaginatedResponse<UserProfilePendingUpdatesResponseDto>> GetUserProfileUpdates(int pageNumber, int pageSize);
    Task<UserApproveResponseDto> ApproveUserRequest(int requestId);
}
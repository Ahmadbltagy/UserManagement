using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Interfaces.Services;

public interface IUserAccountService
{
    Task<UserAccountResponseDto> CreateAccount(CreateUserAccountDto createUserAccountDto);
    Task<UserAccountResponseDto> UpdatePassword(UpdateUserAccountPasswordDto updateUserAccountPasswordDto);
    Task<PaginatedResponse<UserAccountWithProfileDto>> GetUserAccounts(int pageNumber, int pageSize,bool isActive);
    Task<UserAccountResponseDto> ReactiveUserAccounts(string email);

}
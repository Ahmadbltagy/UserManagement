using MediatR;
using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Queries.Handlers;

public class GetUserAccountHandler : IRequestHandler<GetUserAccountQuery, PaginatedResponse<UserAccountWithProfileDto>>
{
    private readonly IUserAccountRepository _userAccountRepository;
    public GetUserAccountHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }
    public async Task<PaginatedResponse<UserAccountWithProfileDto>> Handle(GetUserAccountQuery request, CancellationToken cancellationToken)
    {
        var (data,totalCount) =  await _userAccountRepository.GetUserAccountWithProfile(request.PageNumber,request.PageSize,request.IsActive);
        
        return new PaginatedResponse<UserAccountWithProfileDto>(data, totalCount, request.PageNumber, request.PageSize);
    }
}
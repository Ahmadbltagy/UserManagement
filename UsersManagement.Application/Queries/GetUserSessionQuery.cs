using MediatR;
using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Queries;

public class GetUserSessionQuery : IRequest<List<UserSessionInfoDto>>
{
    
}
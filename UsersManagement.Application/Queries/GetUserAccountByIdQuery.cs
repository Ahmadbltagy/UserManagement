using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries;

public class GetUserAccountByIdQuery : IRequest<UserAccount?>
{
    public int UserAccountId { get; }
    
    public GetUserAccountByIdQuery(int userAccountId)
    {
        UserAccountId = userAccountId;
    }

}
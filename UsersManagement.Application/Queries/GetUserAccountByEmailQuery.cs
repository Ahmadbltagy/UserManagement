using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries;

public class GetUserAccountByEmailQuery : IRequest<UserAccount?>
{
    public string Email { get;}

    public GetUserAccountByEmailQuery(string email)
    {
        this.Email = email;
    }
}
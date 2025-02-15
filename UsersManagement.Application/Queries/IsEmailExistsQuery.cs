using MediatR;

namespace UsersManagement.Application.Queries;

public class IsEmailExistsQuery : IRequest<bool>
{
    public string Email { get;}

    public IsEmailExistsQuery(string email)
    {
        this.Email = email;
    }
}
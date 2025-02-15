using MediatR;
using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Commands;

public class CreateUserAccountCommand : IRequest<bool>
{
    public CreateUserAccountDto CreateUserAccountDto { get; }

    public CreateUserAccountCommand(CreateUserAccountDto createUserAccountDto)
    {
        CreateUserAccountDto = createUserAccountDto;
    }
    
}
using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries;

public class GetUserForSuspendQuery : IRequest<List<UserAccount>>
{
    public GetUserForSuspendQuery(DateTime thresholdDate)
    {
        ThresholdDate = thresholdDate;
    }

    public DateTime ThresholdDate { get; set; }
}
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UsersManagement.Application.Commands;
using UsersManagement.Application.Queries;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Infrastructure.Services;

public class UserSuspensionService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserSuspensionService> _logger;
    private IMediator _mediator;
    public UserSuspensionService(
        IConfiguration configuration, 
        ILogger<UserSuspensionService> logger, IMediator mediator)
    {
        _configuration = configuration;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task SuspendInactiveUsers()
    {
        var inactiveUsersResult = await GetAllInactiveUsers();
        if (!inactiveUsersResult.Any()) return;
        
        var suspendCommand = new SuspendInActiveUserAccountCommand(inactiveUsersResult);
        await _mediator.Send(suspendCommand);
        
        _logger.LogInformation($"{inactiveUsersResult.Count} users suspended due to inactivity.");
    }
    
    private async Task<List<UserAccount>> GetAllInactiveUsers()
    {
        var suspensionDays = Convert.ToInt32(_configuration["UserSuspensionDays"]);
        var thresholdDate = DateTime.Now.AddDays(-suspensionDays);

        var inactiveUsersQuery = new GetUserForSuspendQuery(thresholdDate);
        return await _mediator.Send(inactiveUsersQuery);
    }
}
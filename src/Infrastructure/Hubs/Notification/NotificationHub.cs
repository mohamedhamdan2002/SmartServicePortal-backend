using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Hubs.Notification;

//[Authorize(AuthenticationSchemes = "Bearer")]
public class NotificationHub : Hub<INotificationClient>
{
    private readonly AppDbContext _appDbContext;
    public NotificationHub(AppDbContext appDbContext)
        => _appDbContext = appDbContext;

    public override async Task OnConnectedAsync()
    {
        var _userId = Context?.User?.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;
        if (_userId != null)
        {
            var userConnection = new UsersConnection
            {
                UserId = _userId,
                ConnectionId = Context!.ConnectionId
            };
            _appDbContext.Set<UsersConnection>().Add(userConnection);
            await _appDbContext.SaveChangesAsync();
        }
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _appDbContext.Set<UsersConnection>().Where(u => u.ConnectionId == Context.ConnectionId).ExecuteDeleteAsync();
        await base.OnDisconnectedAsync(exception);
    }
}

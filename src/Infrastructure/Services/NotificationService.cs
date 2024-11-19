using Application.Dtos.NotificationDtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.Hubs.Notification;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementation;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub, INotificationClient> _hub;
    private readonly AppDbContext _appDbContext;
    public NotificationService(IHubContext<NotificationHub, INotificationClient> hub, AppDbContext appDbContext)
    {
        _hub = hub;
        _appDbContext = appDbContext;
    }

    public async Task NotifyBy(string userId, string service, StatusEnum status)
    {
        var notification = new NotificationDto
        {
            Message = $"Your reservation status for '{service}' has been updated to {status}."
        };
        //await _hub.Clients.User(userId).onReservationUpdated(notification);
        var customerConnections = await _appDbContext.Set<UsersConnection>().Where(u => u.UserId == userId).Select(c => c.ConnectionId).ToListAsync();
        foreach (var connectionId in customerConnections)
        {
            //await Clients.User(userId).onReservationUpdated(notification);
            await _hub.Clients.Client(connectionId).onReservationUpdated(notification);
            //await Clients.User(userId).onReservationUpdated(notification);
        }
    }

    public async Task NotifyBy(string userId, ReservationNotificationDto notificationDto)
    {
        var customerConnections = await _appDbContext.Set<UsersConnection>().Where(u => u.UserId == userId).Select(c => c.ConnectionId).ToListAsync();
        foreach (var connectionId in customerConnections)
        {
            //await Clients.User(userId).onReservationUpdated(notification);
            await _hub.Clients.Client(connectionId).onReservationUpdated(notificationDto);
            //await Clients.User(userId).onReservationUpdated(notification);
        }
    }
}

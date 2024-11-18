﻿using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Repositories;

public interface IRepositoryManager : IAsyncDisposable
{
    IRepository<Service> ServiceRepository { get; }
    IRepository<Category> CategoryRepository { get; }
    IRepository<Reservation> ReservationRepository { get; }
    IRepository<Review> ReviewRepository { get; }
    Task SaveChangesAsync();
}

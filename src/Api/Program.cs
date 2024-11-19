using Api.Extensions;
using Api.Utilities;
using Infrastructure.Hubs.Notification;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();
await app.ApplyMigrationsAndSeedDataAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseCors(Constants.MyAppPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();
app.MapHub<NotificationHub>("api/notification-hub");

app.Run();

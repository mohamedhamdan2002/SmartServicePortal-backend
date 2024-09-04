using SmartGallery.Api.Extensions;
using SmartGallery.Api.Utilities;

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
app.UseCors(ApiConstants.MyAppPolicy);
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();

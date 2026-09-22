using MySpot.Api.Entities;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using MySpot.Core.ValueObjects;
using MySpot.Infrastucture;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure()
    .AddControllers();


var app = builder.Build();

app.MapControllers();
app.Run();
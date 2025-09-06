using App.Api.DI;
using App.Application.DI;
using App.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseInfrastructure();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
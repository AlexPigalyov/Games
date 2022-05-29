using System.Reflection;
using Games.Domain.Profiles;
using Games.Infrastructure;
using Games.Infrastructure.Game.Triggers;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var con = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<GamesDbContext>(options =>
{
    options.UseSqlServer(con);
    options.UseTriggers(triggerOptions => { triggerOptions.AddTrigger<AddOrUpdateDateTrigger>(); });
});

var assemblies = new List<Assembly?>
{
    Assembly.GetAssembly(typeof(DomainProfile))
};
builder.Services.AddAutoMapper(assemblies, ServiceLifetime.Singleton);

// Add services to the container.

builder.Services.AddMediatR(typeof(GamesDbContext).GetTypeInfo().Assembly);

builder.Services.AddScoped<GamesDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
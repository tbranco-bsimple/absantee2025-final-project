using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Factory;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Resolvers;
using InterfaceAdapters;
using InterfaceAdapters.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SprintsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//Services
builder.Services.AddTransient<ISprintService, SprintService>();
builder.Services.AddTransient<IProjectService, ProjectService>();


//Repositories
builder.Services.AddTransient<ISprintRepository, SprintRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();


//Factories
builder.Services.AddTransient<ISprintFactory, SprintFactory>();
builder.Services.AddTransient<IProjectFactory, ProjectFactory>();


//Mappers
builder.Services.AddTransient<SprintDataModelConverter>();
builder.Services.AddTransient<ProjectDataModelConverter>();
builder.Services.AddAutoMapper(cfg =>
{
    //DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    //DTO
    cfg.CreateMap<Sprint, SprintDTO>();
});

// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SprintCreatedConsumer>();
    x.AddConsumer<ProjectCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        var instance = InstanceInfo.InstanceId;
        cfg.ReceiveEndpoint($"sprints-query-{instance}", e =>
        {
            e.ConfigureConsumer<SprintCreatedConsumer>(context);
            e.ConfigureConsumer<ProjectCreatedConsumer>(context);
        });
    });
});

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

using Application.DTOs;
using Application.Interfaces;
using Application.IPublishers;
using Application.Services;
using Domain.Factory;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Resolvers;
using InterfaceAdapters;
using InterfaceAdapters.Consumers;
using InterfaceAdapters.Publishers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AssociationsSprintUserStoryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//Services
builder.Services.AddTransient<IAssociationSprintUserStoryService, AssociationSprintUserStoryService>();
builder.Services.AddTransient<ISprintService, SprintService>();
builder.Services.AddTransient<IUserStoryService, UserStoryService>();
builder.Services.AddTransient<ICollaboratorService, CollaboratorService>();


//Repositories
builder.Services.AddTransient<IAssociationSprintUserStoryRepository, AssociationSprintUserStoryRepository>();
builder.Services.AddTransient<ISprintRepository, SprintRepository>();
builder.Services.AddTransient<IUserStoryRepository, UserStoryRepository>();
builder.Services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();


//Factories
builder.Services.AddTransient<IAssociationSprintUserStoryFactory, AssociationSprintUserStoryFactory>();
builder.Services.AddTransient<ISprintFactory, SprintFactory>();
builder.Services.AddTransient<IUserStoryFactory, UserStoryFactory>();
builder.Services.AddTransient<ICollaboratorFactory, CollaboratorFactory>();


//Mappers
builder.Services.AddTransient<AssociationSprintUserStoryDataModelConverter>();
builder.Services.AddTransient<SprintDataModelConverter>();
builder.Services.AddTransient<UserStoryDataModelConverter>();
builder.Services.AddTransient<CollaboratorDataModelConverter>();
builder.Services.AddAutoMapper(cfg =>
{
    //DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    //DTO
    cfg.CreateMap<AssociationSprintUserStory, AssociationSprintUserStoryDTO>();
});

// MassTransit
builder.Services.AddTransient<IMessagePublisher, MassTransitPublisher>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AssociationSprintUserStoryCreatedConsumer>();
    x.AddConsumer<SprintCreatedConsumer>();
    x.AddConsumer<UserStoryCreatedConsumer>();
    x.AddConsumer<CollaboratorCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        var instance = InstanceInfo.InstanceId;
        cfg.ReceiveEndpoint($"associations-sprint-user-story-cmd-{instance}", e =>
        {
            e.ConfigureConsumer<AssociationSprintUserStoryCreatedConsumer>(context);
            e.ConfigureConsumer<SprintCreatedConsumer>(context);
            e.ConfigureConsumer<UserStoryCreatedConsumer>(context);
            e.ConfigureConsumer<CollaboratorCreatedConsumer>(context);
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

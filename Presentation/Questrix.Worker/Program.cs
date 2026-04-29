using Questrix.Application;
using Questrix.Infrastructure;
using Questrix.Mapper;
using Questrix.Persistence;

var builder = Host.CreateApplicationBuilder(args);

IHostEnvironment env = builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

builder.Services.AddApplication()
    .AddCustomMapper()
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);

IHost app = builder.Build();
app.Run();

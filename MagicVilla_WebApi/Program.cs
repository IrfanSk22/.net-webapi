using MagicVilla_WebApi.Data;
using MagicVilla_WebApi.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES TO THE CONTAINER.

// Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
    // .WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
// builder.Host.UseSerilog();
// builder.Services.AddSingleton<ILogging, LoggingV2>();

builder.Services.AddControllers(option =>
{
    // option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson();
// AddXmlDataContractSerializerFormatters()

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
    {
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
    }
);

var app = builder.Build();

// CONFIGURE THE HTTP REQUEST PIPELINE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

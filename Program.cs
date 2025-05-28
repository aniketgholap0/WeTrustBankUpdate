using Microsoft.Extensions.DependencyInjection;
using WeTrustBank.Common;
using WeTrustBank.Repository.Concrete;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Concrete;
using WeTrustBank.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
//builder.Services.AddSingleton<DataBaseService>();
builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service Injection
builder.Services.AddScoped<IAccountTypeService, AccountTypeService>();
builder.Services.AddScoped<IAccountStatusService, AccountStatusService>();
builder.Services.AddScoped<ICardStatusService, CardStatusService>();


// Repository Injection    
builder.Services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
builder.Services.AddScoped<IAccountStatusRepository, AccountStatusRepository>();
builder.Services.AddScoped<ICardStatusRepository, CardStatusRepository>();


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

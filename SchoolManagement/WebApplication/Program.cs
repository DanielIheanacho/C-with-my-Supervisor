using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Service.Interfaces;
using SchoolManagementSystem.Service.Repositories;
using SchoolManagementSystem.Service.Service;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

var builder = WebApplication.CreateBuilder();

var services = builder.Services;

var connectionString = builder.Configuration.GetConnectionString("Default");

services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<AuthenticationService>();
services.AddScoped<UserService>();

var app = builder.Build();


app.Run();
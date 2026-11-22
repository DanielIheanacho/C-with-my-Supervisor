using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.Service.Interfaces;
using SchoolManagementSystem.Service.Repositories;
using SchoolManagementSystem.Service.Service;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

var issuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer is missing from configuration.");
var audience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience is missing from configuration.");
var key = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing from configuration.");

if (key.Length < 32)
{
    throw new InvalidOperationException("Jwt:Key is less than 32 characters.");
}

var secretBytes = Encoding.UTF8.GetBytes(key);

var securityKey = new SymmetricSecurityKey(secretBytes);

var rules = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = issuer,
    ValidateAudience = true,
    ValidAudience = audience,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = securityKey,
    ValidateLifetime = true
};

var services = builder.Services;

services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<AuthenticationService>();
services.AddScoped<UserService>();

services.AddEndpointsApiExplorer();

services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT access token."
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
});


services.AddControllers();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = rules;
    });
services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Admin");
    });

    options.AddPolicy("AdminSameSchool", policy =>
    {
        policy.RequireRole("Admin");
        policy.AddRequirements(new SameSchoolRequirement());
    });

    options.AddPolicy("IsMe", policy =>
    {
        policy.AddRequirements(new IsMeRequirement());
    });
});

services.AddScoped<IAuthorizationHandler, SameSchoolHandler>();
services.AddScoped<IAuthorizationHandler, IsMeHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("SchoolManagementSystem").MapControllers();

app.Run();
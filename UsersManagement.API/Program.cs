using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UsersManagement.Application;
using UsersManagement.Infrastructure;
using UsersManagement.Infrastructure.Services;
using UsersManagement.Persistence.DbContext;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("UserManagement")));

builder.Services.AddHangfireServer();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);


builder.Services
    .AddAuthentication(options =>
    {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWTConfig:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWTConfig:Audience"],
            
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:SecretKey"])),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer();

RecurringJob.AddOrUpdate<UserSuspensionService>(
    "suspend-inactive-users",
    service => service.SuspendInactiveUsers(),
    Cron.Daily
);




app.UseHttpsRedirection();

app.UseMiddleware<SessionValidationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
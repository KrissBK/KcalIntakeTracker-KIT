using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using KcalIntakeTracker_KIT.Interfaces;
using KcalIntakeTracker_KIT.Repository;
using KcalIntakeTracker_KIT.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDailyLogRepository, DailyLogRepository>();


// Register KITDbContext
builder.Services.AddDbContext<KITDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KITDatabase")));

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

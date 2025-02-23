using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using KcalIntakeTracker_KIT.Interfaces;
using KcalIntakeTracker_KIT.Repository;
using KcalIntakeTracker_KIT.Data;
using KcalIntakeTracker_KIT.Helper;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.AddDbContext<KITDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KITDatabase")));


// Register the repositories
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDailyLogRepository, DailyLogRepository>();
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();

// Add AutoMapper
//builder.Services.AddAutoMapper(typeof(MappingProfile));


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

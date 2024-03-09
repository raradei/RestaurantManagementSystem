using CustomerFeedbackAPI.Interfaces;
using CustomerFeedbackAPI.Services;
using Microsoft.EntityFrameworkCore;
using RestaurantRepositoryLibrary;
using RestaurantRepositoryLibrary.Repositories;
using RestaurantRepositoryLibrary.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RestaurantContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowSwaggerPolicy = "";

if (builder.Environment.IsDevelopment())
{
    allowSwaggerPolicy = "allowSwaggerOrigin";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(allowSwaggerPolicy, policy =>
        {
            policy.AllowAnyHeader();
            policy.WithOrigins("https://localhost:7065", "https://localhost:7066");
        });
    });
}

builder.Services.AddScoped<ICustomerFeedbackRepository, CustomerFeedbackRepositoy>();
builder.Services.AddScoped<ICustomerFeedbackService, CustomerFeedbackService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(allowSwaggerPolicy);
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

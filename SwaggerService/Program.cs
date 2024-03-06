var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        setupAction.SwaggerEndpoint("https://localhost:7207/swagger/v1/swagger.json", "CustomerFeedbackAPI");
        setupAction.SwaggerEndpoint("https://localhost:7052/swagger/v1/swagger.json", "FeedbackResponseAPI");
        setupAction.SwaggerEndpoint("https://localhost:7050/swagger/v1/swagger.json", "RestaurantManagementAPI");
    });
}

app.UseHttpsRedirection();
app.Run();

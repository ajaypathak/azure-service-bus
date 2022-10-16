using Serilog;

using TopicsAndSubscriptions.Abstraction;
using TopicsAndSubscriptions.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductProvider, ProductService>();
builder.Services.AddTransient<IIndianStateProvider, IndianStateService>();
var log = new LoggerConfiguration()
    .WriteTo.File("log-{Date}.txt")
    .CreateLogger();
Log.Logger = log;

Log.Information("This will be written to the rolling file set");
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

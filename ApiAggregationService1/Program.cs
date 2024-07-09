using API_aggregation_service.Services;
using ApiAggregationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient<INewsService, NewsService>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddHttpClient<IGitHubService, GitHubService>();

builder.Services.AddScoped<IAggregationService, AggregationService>();

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

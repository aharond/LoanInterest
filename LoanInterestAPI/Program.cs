using Infrastructure;
using Application;
using System.Text.Json.Serialization;
using Domain.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<LoanSettings>(builder.Configuration.GetSection("LoanData"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services
    .AddInfrastructure()
    .AddApplication();

builder.Services
       .AddRouting(options => { options.LowercaseUrls = true; });

// compression
builder.Services.AddResponseCompression(options => { });
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Cors added to pipeline
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(options =>
     options.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

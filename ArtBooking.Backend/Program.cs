using ArtBooking.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adding default CORS policy that will all to make a call from browser with any origin (url) with any http method and any http header.
builder.Services.AddCors(o => o.AddDefaultPolicy(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddControllers();

// set lowercase urls
builder.Services.AddRouting(o => o.LowercaseUrls = true);
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add storage mockup
builder.Services.AddArtBookingStorageMockup();

// Add aplication core bussines
builder.Services.AddArtBookingCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enabling cors with default policy
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

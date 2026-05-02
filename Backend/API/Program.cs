using API.Filters;
using Database.Context;
using Database.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.Requests;
using Model.Dtos.Responses;
using Services.Interfaces;
using Services.Interfaces.Parsers;
using Services.Services;
using Services.Services.Parsers;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddDbContext<SDPSContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers(f =>
{
    f.Filters.Add<ExceptionFilter>();
});

// Enable Mapster
builder.Services.AddMapster();

// Add services to the container.
builder.Services.AddScoped<IParserService, PdfParserService>();
builder.Services.AddScoped<IParserService, CSVParserService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

TypeAdapterConfig<ParserResponse, Document>
    .NewConfig()
    .Ignore(dest => dest.Id);
TypeAdapterConfig<DocumentUpdateRequest, Document>
    .NewConfig()
    .Ignore(dest => dest.Id);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(
    options => options
        .SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SDPSContext>();

    var pendingMigrations = dataContext.Database.GetPendingMigrations();

    if (pendingMigrations.Any())
    {
        dataContext.Database.Migrate();
    }
}

app.Run();

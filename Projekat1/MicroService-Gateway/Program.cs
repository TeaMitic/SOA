using System.Reflection;
using MicroService_Gateway.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    {
        Version = "v1",
        Title = "Songs and Lyrics Api",
        Description = ".Net 6 web api for crud and gateway service to work with songs.",
        Contact = new OpenApiContact
        {
            Name = "Tea Mitic & Dimitrije Mitic",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense 
        {
            Name = "No license"
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddTransient<ISongService, SongService>();
builder.Services.AddTransient<ILyricsService, LyricsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();

using AwesomeDevEvents.Api.Mappers;
using AwesomeDevEvents.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

// Add services to the container.
// builder.Services.AddDbContext<DevEventsDbContext>(opts => opts.UseInMemoryDatabase("DevEventsDb"));
builder.Services.AddDbContext<DevEventsDbContext>(opts => opts.UseNpgsql(connectionString));
builder.Services.AddAutoMapper(typeof(DevEventProfile).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// config swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AwesomeDevEvents.Api",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "André",
            Email = "anndre.junior15@gmail.com"
        },
        Description = "Qualquer dúvida envie um email para o endereço citado ou crie uma issue no repositório: " + new Uri("https://github.com/AnndreJunior/AwesomeDevEvents")
    });
    var xmlFile = "AwesomeDevEvents.Api.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

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

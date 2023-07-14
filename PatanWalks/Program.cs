using Microsoft.EntityFrameworkCore;
using PatanWalks.Data;
using PatanWalks.Mappings;
using PatanWalks.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MaharashtraDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MaharashtraConnectionString")));

builder.Services.AddScoped<IDivisionRepository, SQLDivisionRepository>();
builder.Services.AddScoped<IDistrictRepository, SQLDistrictRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
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

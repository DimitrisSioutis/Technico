using Technico.Repositories;
using Technico.Models;
using Microsoft.EntityFrameworkCore;
using Technico.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<Owner, Guid>, OwnerRepository>(); //add scoped to create new instance of repo in each request
builder.Services.AddScoped<IRepository<Professional, Guid>, ProfessionalRepository>();
builder.Services.AddScoped<IRepository<Property, Guid>, PropertyRepository>();
builder.Services.AddScoped<IRepository<Repair, Guid>, RepairRepository>();
builder.Services.AddDbContext<TechnicoDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TechnicoDBContext")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

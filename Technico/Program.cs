using Technico.Repositories;
using Technico.Services;
using Microsoft.EntityFrameworkCore;
using Technico.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PropertyRepository>();
builder.Services.AddScoped<RepairRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PropertyService>();
builder.Services.AddScoped<RepairService>();

builder.Services.AddDbContext<TechnicoDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TechnicoDBContext")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

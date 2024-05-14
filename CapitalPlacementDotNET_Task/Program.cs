using CapitalPlacementDotNET_Task.AppContext;
using CapitalPlacementDotNET_Task.Implementation.Repository;
using CapitalPlacementDotNET_Task.Implementation.Services;
using CapitalPlacementDotNET_Task.Interface.IRepository;
using CapitalPlacementDotNET_Task.Interface.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//string dbConnStr = builder.Configuration.GetConnectionString("Default");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    string dbConnStr = builder.Configuration.GetConnectionString("Default");
    options.UseSqlServer(dbConnStr);
});
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<ICustomQuestionRepository, CustomQuestionRepository>();
builder.Services.AddScoped<IFormService,FormService>();
builder.Services.AddScoped<IApplicationFormResponseRepository, ApplicationFormResponseRepository>();    
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

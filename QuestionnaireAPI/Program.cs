using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Application.Interfaces;
using QuestionnaireAPI.Application.Services;
using QuestionnaireAPI.Persistence.Context;
using QuestionnaireAPI.Persistence.Interfaces;
using QuestionnaireAPI.Persistence.Mappers;
using QuestionnaireAPI.Persistence.Repository;
using WorkQuestionnaire.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<WebAPIContext>(options =>
     options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(MappingProfile)/*AppDomain.CurrentDomain.GetAssemblies()*/);
builder.Services.AddTransient<IQuestionnaireFormService, QuestionnaireFormService>();
builder.Services.AddTransient<IQuestionnaireFormRepository, QuestionnaireFormRepository>();
builder.Services.AddTransient<IAnswerService, AnswerService>();
builder.Services.AddTransient<IAnswerRepository, AnswerRepository>();
builder.Services.AddTransient<IMetricsService, MetricsService>();
builder.Services.AddTransient<IMetricsRepository, MetricsRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

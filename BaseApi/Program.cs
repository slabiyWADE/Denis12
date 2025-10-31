using BaseApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Разрешаем запросы с фронтенда (http://localhost:5189)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5189",
            "https://localhost:5189"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5189") // порт твоего фронтенда
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// ✅ Подключаем базу
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Контроллеры и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Разрешаем CORS
app.UseCors("AllowFrontend");

// ✅ Swagger должен быть ДО app.Run()
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowFrontend");
app.MapControllers();

// ✅ Добавим тестовый эндпоинт
app.MapGet("/", () => Results.Ok("API is running! Go to /swagger"));

app.Run();

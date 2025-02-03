using API.Configurations;
using DotNetEnv;
using Persistance;

Env.Load();

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddEnvironmentVariables();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));


// Конфигурация сервисов
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthorization();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureRepositories<UniversityDbContext>();
builder.Services.ConfigureServices();


// Добавление контроллеров
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();



var app = builder.Build();


// Конфигурация пайплайна
app.ConfigureCors();
app.ConfigureSwagger(app.Environment); // Вызываем метод для Swagger


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.ConfigureEndpoints();

app.Run();

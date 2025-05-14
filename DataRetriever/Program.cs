using System.Text;
using DataRetriever.DataStorage;
using DataRetriever.Factory;
using DataRetriever.Repository;
using DataRetriever.Services.Concrete;
using DataRetriever.Services.Interfaces;
using DataRetriever.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using FluentValidation;
using DataRetriever.Dtos;
using DataRetriever.Dtos.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.
    GetSection("MongoDbSettings"));
builder.Services.AddScoped<IMongoDbSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
    
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(builder.Configuration["AllowedHosts"] ?? "*") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IDataRetrieverService, DataRetrieverService>();
builder.Services.AddScoped<IDataStorage, CacheStorage>();
builder.Services.AddScoped<IDataStorage, FileStorage>();
builder.Services.AddScoped<IDataStorage, DbStorage>();
builder.Services.AddScoped<IDataRepository, MongoDbRepository>();
builder.Services.AddScoped<IDataStorageFactory, DataStorageFactory>();
builder.Services.AddSingleton<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddScoped<IValidator<CreateDataDto>, CreateDataDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateDataDto>, UpdateDataDtoValidator>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "DataApi:";
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Key", "JWT key is not configured"))),
            ClockSkew = TimeSpan.Zero 
        };
    });

builder.WebHost.UseUrls("http://0.0.0.0:8080");
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(System.Net.IPAddress.Any, 8080);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting(); 
app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.Run();



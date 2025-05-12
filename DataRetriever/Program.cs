using DataRetriever.DataStorage;
using DataRetriever.Factory;
using DataRetriever.Services.Concrete;
using DataRetriever.Services.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.
    GetSection("MongoDbSettings"));
builder.Services.AddScoped<IMongoDbSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
    
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataRetrieverService, DataRetrieverService>();
builder.Services.AddScoped<IDataStorage, CacheStorage>();
builder.Services.AddScoped<IDataStorage, FileStorage>();
builder.Services.AddScoped<IDataStorage, DbStorage>();
builder.Services.AddScoped<IDataStorageFactory, DataStorageFactory>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "DataApi:";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();



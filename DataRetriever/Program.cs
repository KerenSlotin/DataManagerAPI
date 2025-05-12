using DataRetriever.Data;
using DataRetriever.DataStorage;
using DataRetriever.Factory;
using DataRetriever.Services.Concrete;
using DataRetriever.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddScoped<IDataRetrieverService, DataRetrieverService>();
builder.Services.AddScoped<IDataStorage, CacheStorage>();
builder.Services.AddScoped<IDataStorage, FileStorage>();
builder.Services.AddScoped<IDataStorage, DbStorage>();
builder.Services.AddScoped<IDataStorageFactory, DataStorageFactory>();

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



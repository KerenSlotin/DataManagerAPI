using DataRetriever.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDataRepository, DataRepository>();
builder.Services.AddSingleton<IDataRetrieverService, DataRetrieverService>();


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



using Perguntech.Data.Repositories; 
using Perguntech.Services; 
using MongoDB.Driver;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoSettings");
builder.Services.AddSingleton<IMongoClient>(provider => new MongoClient(mongoSettings["ConnectionString"]));

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>(provider =>
{
    var mongoSettings = builder.Configuration.GetSection("MongoSettings");
    var connectionString = mongoSettings["ConnectionString"];
    var databaseName = mongoSettings["DatabaseName"];
    var questionCollectionName = mongoSettings["QuestionCollectionName"];
    var categoryCollectionName = mongoSettings["CategoryCollectionName"];
    return new QuestionRepository(connectionString, databaseName, questionCollectionName, categoryCollectionName);
});


var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

builder.Services.AddScoped<QuestionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
builder.Services.AddSingleton<RedisService>();

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

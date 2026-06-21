using Microsoft.AspNetCore.Http.Features;
using eduLib.Application.Tracking;
using eduLib.Infrastructure.Storage;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("MongoAtlas")!;
var databaseName = "book";

// === Register Services ===
builder.Services.AddSingleton<BookmarkManager>();
builder.Services.AddSingleton<ReadingStateMachine>();
builder.Services.AddSingleton(new MongoTrackingRepository(connectionString, databaseName));


int maxFileSizeMB = builder.Configuration.GetValue<int>("StorageSettings:MaxFileSizeMB");
long maxFileSizeBytes = (maxFileSizeMB * 1024 * 1024) + (50 * 1024 * 1024);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = maxFileSizeBytes;
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = maxFileSizeBytes;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
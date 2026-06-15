using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

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
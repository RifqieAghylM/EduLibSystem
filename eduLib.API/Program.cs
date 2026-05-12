using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// --- 1. BACA BUKU SOP (appsettings.json) ---
// Membaca angka 50 dari StorageSettings:MaxFileSizeMB
int maxFileSizeMB = builder.Configuration.GetValue<int>("StorageSettings:MaxFileSizeMB");

// Mengubah Megabyte menjadi Byte (50 * 1024 * 1024 = 52.428.800 Bytes)
// Kita tambah sedikit buffer (+ 5MB) untuk form metadata
long maxFileSizeBytes = (maxFileSizeMB * 1024 * 1024) + (50 * 1024 * 1024);


// --- 2. TERAPKAN ATURAN KE SATPAM (KESTREL) ---
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Kestrel sekarang patuh pada isi appsettings.json!
    serverOptions.Limits.MaxRequestBodySize = maxFileSizeBytes;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = maxFileSizeBytes;
});

// Tambahkan dukungan Controller
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
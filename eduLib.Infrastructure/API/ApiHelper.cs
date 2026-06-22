using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eduLib.Infrastructure.API 
{
    public class ApiHelper
    {
        // 1. IMPLEMENTASI RUNTIME CONFIGURATION
        public static string GetBaseUrl()
        {
            if (!File.Exists("appsettings.json"))
                return "https://localhost:7053/api/Books"; // Fallback default

            try
            {
                string jsonString = File.ReadAllText("appsettings.json");

                // Menggunakan JsonDocument agar aman dari struktur JSON yang kompleks
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    if (doc.RootElement.TryGetProperty("BaseUrl", out JsonElement baseUrl))
                    {
                        return baseUrl.GetString();
                    }
                }
            }
            catch (Exception)
            {
                // Jika gagal membaca, biarkan lanjut memakai URL default di bawah
            }

            return "https://localhost:7053/api/Books";
        }

  
        // <T> memungkinkan method ini menerima/mengembalikan tipe data apa saja secara dinamis
        public static async Task<List<T>> GetListAsync<T>(string url)
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<T>>(json, options);
            }

            return new List<T>();
        }
    }
}
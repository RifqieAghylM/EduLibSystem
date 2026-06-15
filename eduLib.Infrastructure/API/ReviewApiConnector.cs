using System;
using System.Threading.Tasks;

namespace eduLib.Infrastructure.API
{
    public class ReviewApiConnector
    {
        // Teknik: API Integration (Simulasi HTTP Call untuk ulasan)
        public async Task<bool> SendReviewAsync(string bookTitle, string comment)
        {
            // Defensive Programming: Precondition
            if (string.IsNullOrWhiteSpace(comment)) return false;

            // Simulasi proses network (Mock API)
            await Task.Delay(100);
            Console.WriteLine($"API: Ulasan untuk '{bookTitle}' berhasil dikirim.");

            return true;
        }
    }
}
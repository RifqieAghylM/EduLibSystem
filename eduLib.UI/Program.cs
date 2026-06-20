using System;
using System.Windows.Forms;

namespace eduLib.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Menjalankan FormReview milik Rifqie sebagai halaman startup awal
            System.Windows.Forms.Application.Run(new FormReview());
            System.Windows.Forms.Application.Run(new FormViewReviews());
        }
    }
}
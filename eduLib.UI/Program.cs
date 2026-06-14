
namespace eduLib.UI
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Tambahkan "System.Windows.Forms." di depan Application
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Baris ini memanggil form yang sudah Anda buat
            System.Windows.Forms.Application.Run(new ReadDownloadForm());
        }
    }
}

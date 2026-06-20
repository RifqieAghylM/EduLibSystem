using System;

namespace eduLib.UI
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new EduLibAdmin1());
            System.Windows.Forms.Application.Run(new KelolaAdmin());
            

        }
    }
}
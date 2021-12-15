using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
            //Application.Run(new frmAddFood());
            //Application.Run(new frmMain());
            //Application.Run(new frmAdAccount());
            // Application.Run(new frmAdCategory());
            //Application.Run(new frmAdFood());
            //Application.Run(new frmAdTables());
            //Application.Run(new frmPay());
            //Application.Run(new frmNhanvien());
            //Application.Run(new Block());
            //Application.Run(new ReFood());

            //Application.Run(new reportNhanVien());
            //Application.Run(new ReportMon());

        }
    }
}

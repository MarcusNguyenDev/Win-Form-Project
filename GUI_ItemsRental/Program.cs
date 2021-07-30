using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace GUI_ItemsRental
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            VRModel.Initializer.CreateDatabase();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMenu());
            Debug.Write("Debugging the program");
            Console.WriteLine("Debug will be break after this line");
            Debugger.Break();
        }

        public static void Test(Type type, Type basetype)
        {
            Debug.Assert(type != null,"It is null");
        }

    }
}

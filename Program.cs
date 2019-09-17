using System;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace FloatingWindow
{
	
	internal sealed class Program
	{
		[DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        
		[STAThread]
		private static void Main(string[] args)
		{
/*			#if DEBUG
            AllocConsole();
            #endif
*/          
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			
		}
		
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using log4net;
using log4net.Core;

namespace PracaMagisterska
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
			Bootstrapper.Initialize();
			_logger.InfoFormat("Application started");
			Application.Run(new Form1());
		}

		private static ILog _logger = LogManager.GetLogger(typeof (Program));
	}
}

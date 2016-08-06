using System;
using System.Diagnostics;
using System.IO;
namespace ImageProcessing
{
	public class CmdExecutor : ICmdExecutor
	{
		public void Run(string cmd, string args)
		{
			run_cmd(cmd, args);
		}

		private void run_cmd(string cmd, string args)
		{
			var start = new ProcessStartInfo
			{
				FileName = cmd,
				Arguments = args,
				UseShellExecute = false,
				RedirectStandardOutput = true
			};

			using (Process process = Process.Start(start))
			{
				using (StreamReader reader = process.StandardOutput)
				{
					string result = reader.ReadToEnd();
					Console.Write(result);
				}
			}
		}
	}
}
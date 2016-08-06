using System.Collections.Generic;
using System.IO;
using ImageProcessing;

namespace PracaMagisterska
{
	public class Processor
	{
		public Queue<Algorithm> Queue = new Queue<Algorithm>();
		public const string FileName = "processedImage";
		public CmdExecutor Executor = new CmdExecutor();
		public bool Runned;

		public void Process(string path)
		{
			if (File.Exists(Algorithms.AlgorithmsLocation + "\\" + FileName + ".bmp"))
			{
				File.Delete(Algorithms.AlgorithmsLocation + "\\" + FileName + ".bmp");
			}
			System.IO.File.Copy(path, Algorithms.AlgorithmsLocation + "\\" + FileName + ".bmp");
			var f = Algorithms.AlgorithmsLocation + "\\" + FileName + ".bmp";

			foreach (var algorithm in Queue)
			{
				while (true)
				{
					if (!Runned)
					{
						var arg = Algorithms.AlgorithmsLocation + "\\" + algorithm.Name + " " + f + " ";
						foreach (var algorithmParameter in algorithm.Parameters)
						{
							arg += algorithmParameter.Value + " ";
						}
						Runned = true;
						Executor.Run("python", arg);
					}
					if (File.Exists(FileName + ".tiff"))
					{
						Runned = false;
						f = FileName + ".tiff";
						break;
					}
				}
			}
		}
	}
}
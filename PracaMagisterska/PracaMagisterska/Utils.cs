using System.IO;
using System.Text;

namespace PracaMagisterska
{
	public static class Utils
	{
		public static string ReadFile(string path)
		{
			return File.ReadAllText(path, Encoding.UTF8);
		}
	}
}

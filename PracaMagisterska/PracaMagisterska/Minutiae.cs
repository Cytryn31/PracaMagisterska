
namespace PracaMagisterska
{
	public class Minutiae
	{
		public bool[,] Shape { get; set; }
		public MinutiaeType Type { get; set; }
		public Position Position { get; set; }
		public int Orientation { get; set; }
	}

	public class Position
	{
		public int X { get; set; }
		public int Y { get; set; }
	}

	public enum MinutiaeType
	{
		Unknown = 0
	}
}

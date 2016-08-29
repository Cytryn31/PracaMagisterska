
namespace PracaMagisterska
{
	public class Minutiae
	{
		public MinutiaeType Type { get; set; }
		public Position Position { get; set; }
		public int Orientation { get; set; }

		protected bool Equals(Minutiae other)
		{
			return Type == other.Type && Equals(Position, other.Position) && Orientation == other.Orientation;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (int) Type;
				hashCode = (hashCode*397) ^ (Position?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ Orientation;
				return hashCode;
			}
		}
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

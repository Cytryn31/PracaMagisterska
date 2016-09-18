using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PracaMagisterska
{
	public static class ColorExtensions
	{
		public static int ToIntConverter(this Color color)
		{
			var colorToCompare = Color.FromArgb(color.R, color.B, color.G);
			return !(colorToCompare.A.Equals(Color.White.A) &&
				colorToCompare.R.Equals(Color.White.R) &&
				colorToCompare.G.Equals(Color.White.G) &&
				colorToCompare.B.Equals(Color.White.B)) ? 1 : 0;
		}
	}
	public class FeaturesExtractor : Singleton<FeaturesExtractor>
	{

		public List<Minutiae> ExtractFeatures(Image image, int height, int width)
		{
			List<Minutiae> list = new List<Minutiae>();
			using (Bitmap bmp = new Bitmap(image))
			{
				for (int i = 1; i < width - 1; i++)
				{
					for (int j = 1; j < height - 1; j++)
					{
						var col = bmp.GetPixel(i, j);
						if (bmp.GetPixel(i, j).Equals(Color.White))
						{
							break;
						}
						foreach (var pattern in BiPatterns)
						{
							var bmpArray = new[,]
							{
								{bmp.GetPixel(i, j - 1).ToIntConverter(), bmp.GetPixel(i + 1, j - 1).ToIntConverter()},
								{bmp.GetPixel(i, j).ToIntConverter(), bmp.GetPixel(i + 1, j).ToIntConverter()},
								{bmp.GetPixel(i, j + 1).ToIntConverter(), bmp.GetPixel(i + 1, j + 1).ToIntConverter()},
							};
							var tmp = IsOk(pattern, bmpArray);
							if (tmp)
							{
								list.Add(new Minutiae()
								{
									Orientation = 0,
									Position = new Position()
									{
										X = i,
										Y = j,
									},
									Type = MinutiaeType.Bi
								});
								break;

							}
							tmp = IsOk(RotateToLeft(pattern), RotateToLeft(bmpArray));
							if (tmp)
							{
								list.Add(new Minutiae()
								{
									Orientation = 0,
									Position = new Position()
									{
										X = i,
										Y = j,
									},
									Type = MinutiaeType.Bi
								});
								break;
							}
						}

						foreach (var pattern in EndPatterns)
						{
							var bmpArray = new[,]
							{
								{bmp.GetPixel(i - 1, j).ToIntConverter(), bmp.GetPixel(i - 1, j + 1).ToIntConverter()},
								{bmp.GetPixel(i, j).ToIntConverter(), bmp.GetPixel(i, j + 1).ToIntConverter()},
								{bmp.GetPixel(i + 1, j).ToIntConverter(), bmp.GetPixel(i + 1, j + 1).ToIntConverter()},
							};
							var tmp = IsOk(pattern, bmpArray);
							if (tmp)
							{
								list.Add(new Minutiae()
								{
									Orientation = 0,
									Position = new Position()
									{
										X = i,
										Y = j,
									},
									Type = MinutiaeType.Ending
								});
								break;

							}
							tmp = IsOk(RotateToLeft(pattern), RotateToLeft(bmpArray));
							if (tmp)
							{
								list.Add(new Minutiae()
								{
									Orientation = 0,
									Position = new Position()
									{
										X = i,
										Y = j,
									},
									Type = MinutiaeType.Ending
								});
								break;
							}
						}
					}
				}

			}
			return list;
		}

		bool IsOk(int[,] data1, int[,] data2)
		{
			return data1.Rank == data2.Rank &&
				Enumerable.Range(0, data1.Rank).All(dimension => data1.GetLength(dimension) == data2.GetLength(dimension)) &&
				data1.Cast<int>().SequenceEqual(data2.Cast<int>());
		}

		int[,] RotateToLeft(int[,] array)
		{
			return new[,]
			{
				{array[1, 0], array[1, 1], array[2, 1]},
				{array[0, 0], array[0, 1], array[2, 0]},
			};
		}


		public List<int[,]> EndPatterns = new List<int[,]>
		{
			new[,]
			{
				{0, 0},
				{0, 1},
				{0, 0},
			},
			new[,]
			{
				{0, 0},
				{1, 0},
				{0, 0},
			},
		};

		public List<int[,]> BiPatterns = new List<int[,]>
		{
			new [,]
			{
				{1,1},
				{1,0},
				{1,1},
			},
			new [,]
			{
				{1,1},
				{0,1},
				{1,1},
			},
			new [,]
			{
				{1,0},
				{0,1},
				{1,1},
			},
			new [,]
			{
				{1,1},
				{0,1},
				{1,0},
			},
			new [,]
			{
				{1,1},
				{1,0},
				{0,1},
			},
			new [,]
			{
				{0,1},
				{1,0},
				{1,1},
			},
			new [,]
			{
				{1,0},
				{0,1},
				{1,0},
			},
			new [,]
			{
				{0,1},
				{1,0},
				{0,1},
			},
		};

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using PracaMagisterska.Common;
using System.IO;
using System.Drawing;

namespace PracaMagisterska
{
	public enum PictureBoxType
	{
		Ref = 0,
		Calc = 1
	}
	public class MinutiaeManager : Singleton<MinutiaeManager>
	{
		public MinutiaeWithImage<Minutiae> ReferenceImage = new MinutiaeWithImage<Minutiae>();
		public MinutiaeWithImage<Minutiae> CalculatedImage = new MinutiaeWithImage<Minutiae>();
		public static double TranslationMax = 5;
		public static double RotationMax = 8;
		public static double DirMax = 8;

		public static double ScalingMax = 0.3;
		public static double RidgeMax = 5;

		public static double Alpha = 0.4;
		public static double Beta = 0.4;
		public static double Gamma = 0.2;
		public static Node Tree = new Node();

		public static Image FromFile(string path)
		{
			var bytes = File.ReadAllBytes(path);
			var ms = new MemoryStream(bytes);
			var img = Image.FromStream(ms);
			return img;
		}

		public void CallDrawEclipses(Views.PictureBoxControl boxControl, PictureBoxType type )
		{
			if(type == PictureBoxType.Ref)
			{
			//	boxControl.UpdateImage((ReferenceImage.ImagePath));
				foreach (var min in ReferenceImage.Minutiaes)
				{
					boxControl.DrawEclipse(min.Position,min.Orientation);
				}
				
			}
			else
			{
			//	boxControl.UpdateImage((CalculatedImage.ImagePath));
				foreach (var min in CalculatedImage.Minutiaes)
				{
					boxControl.DrawEclipse(min.Position, min.Orientation);
				}
			}
		}

		public void CallDrawline(Views.PictureBoxControl boxControl, PictureBoxType type, Position pos)
		{
			//boxControl.UpdateImage();
		//	boxControl.DrawLastEclipse(pos);
		}

		public List<Common.Tuple<Minutiae>> PairMinutaes()
		{
			var pairs = new List<Common.Tuple<Minutiae>>();
			return ReferenceImage.Minutiaes
				.Aggregate(pairs, (current, minutiae) =>
				current?
				.Concat(CalculatedImage.Minutiaes
				.Where(minutiae.IsPair)
				.Select(p => CreateMinutiaePair(minutiae, p))) as List<Common.Tuple<Minutiae>>);
		}

		public List<Common.Tuple<Common.Tuple<Minutiae>>> RandomPairTouples(List<Common.Tuple<Minutiae>> list)
		{
			var pairs = new List<Common.Tuple<Common.Tuple<Minutiae>>>();
			pairs.AddRange(list.Select(minutiae => new Common.Tuple<Common.Tuple<Minutiae>>()
			{
				First = minutiae,
				Second = list[IndexPair(list.IndexOf(minutiae), list.Count)]
			}));
			return pairs;
		}

		public void BuildTree(List<MinutiaeTuple> list)
		{
			Tree.SummaryCost = 0;
			Insert(Tree, list.OrderBy(p => p.CostFunctionValue).ToList());
		}

		private void Insert(Node node, List<MinutiaeTuple> list)
		{
			if (node.Tuple == null)
			{
				node.Childrens = list.Select(p => new Node(p, node.SummaryCost)).ToList();
			}
			if(list.Count() < 0) return;

			foreach (var children in node.Childrens)
			{
				Insert(children,list.Where(p => children.Tuple.IsConsistent()  ).ToList());
			}
		}

		private List<Common.Tuple<Common.Tuple<Minutiae>>> CreateMinutiaeTuple(List<Common.Tuple<Minutiae>> pairs)
		{
			return (from touple in pairs
					from pair in pairs.Where(p => !p.First.Equals(touple.First) && !p.Second.Equals(touple.Second))
					select new Common.Tuple<Common.Tuple<Minutiae>>()
					{
						First = touple,
						Second = pair
					}).ToList();
		}


		private Common.Tuple<Minutiae> CreateMinutiaePair(Minutiae first, Minutiae second)
		{
			return new Common.Tuple<Minutiae>()
			{
				First = first,
				Second = second
			};
		}

		private MinutiaeTuple CreateMinutiaeTuple(Common.Tuple<Minutiae> first, Common.Tuple<Minutiae> second)
		{
			var midPointFirst = new Position()
			{
				X = (first.First.Position.X + first.Second.Position.X) / 2,
				Y = (first.First.Position.Y + first.Second.Position.Y) / 2,
			};

			var midPointSecond = new Position()
			{
				X = (second.First.Position.X + second.Second.Position.X) / 2,
				Y = (second.First.Position.Y + second.Second.Position.Y) / 2,
			};

			var angleFirst = MinutiaeExtension.Angle(first.First.Position.X, first.First.Position.Y, first.Second.Position.X,
				first.Second.Position.Y);
			var angleSecond = MinutiaeExtension.Angle(second.First.Position.X, second.First.Position.Y, second.Second.Position.X,
	second.Second.Position.Y);

			return new MinutiaeTuple()
			{
				Rot = Math.Abs(angleFirst - angleSecond),
				Transaltion = MinutiaeExtension.Distance(midPointFirst.X, midPointFirst.Y, midPointSecond.X, midPointSecond.Y),
				Scaling =
					(1 - (MinutiaeExtension.Distance(first.First.Position.X, first.First.Position.Y, first.Second.Position.X, first.Second.Position.Y) /
				   MinutiaeExtension.Distance(second.First.Position.X, second.First.Position.Y, second.Second.Position.X, second.Second.Position.Y))),
				Dir = Math.Abs(angleFirst - angleSecond),
				Tuple = new Common.Tuple<Common.Tuple<Minutiae>>()
				{
					First = first,
					Second = second
				},
			};
		}

		private int IndexPair(int indexOf, int size)
		{
			var random = new Random();
			while (true)
			{
				int retIndex = random.Next(size);
				if (retIndex != indexOf)
				{
					return retIndex;
				}
			}
		}
	}

	public static class MinutiaeExtension
	{
		public static bool IsPair(this Minutiae first, Minutiae second)
		{
			return Distance(first.Position.X, first.Position.Y, second.Position.X, second.Position.Y) < MinutiaeManager.TranslationMax
				   && Math.Abs(first.Orientation - second.Orientation) < MinutiaeManager.RotationMax + MinutiaeManager.DirMax;
		}

		public static bool IsConsistent(this MinutiaeTuple tuple)
		{
			return tuple.Transaltion < MinutiaeManager.TranslationMax
				   && tuple.Rot < MinutiaeManager.RotationMax
				   && tuple.Scaling < MinutiaeManager.ScalingMax;
		}

		public static double Angle(double x1, double y1, double x2, double y2) => Math.Atan2(y2 - y1, x2 - x1) * 180.0 / Math.PI;
		public static double Distance(double x1, double y1, double x2, double y2) => Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
	}
}

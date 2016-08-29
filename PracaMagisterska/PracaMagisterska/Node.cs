using System.Collections.Generic;

namespace PracaMagisterska
{
	public class Node
	{
		public MinutiaeTuple Tuple { get; set; }
		public double SummaryCost { get; set; }
		public List<Node> Childrens { get; set; }

		public Node()
		{
		}

		public Node(MinutiaeTuple tuple, double sum)
		{
			Tuple = tuple;
			Childrens = new List<Node>();
			SummaryCost = sum;
		}
	}
}

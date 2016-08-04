using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using static System.Configuration.ConfigurationSettings;

namespace PracaMagisterska
{
	public class Algorithms : Singleton<Algorithms>
	{
		string AlgorithmsLocation { get; } = AppSettings["AlgorithmsLocation"];

		List<Algorithm> _items = new List<Algorithm>();
		public List<Algorithm> Items
		{
			get
			{
				if (_items.Count == 0)
				{
					_items = Directory
						.GetFiles(AlgorithmsLocation)
						.Where(file => file.Contains(".alg"))
						.Select(file => JsonConvert.DeserializeObject<Algorithm>(File.ReadAllText(file)))
						.ToList();
				}
				return _items;
			}
		}
	}
}

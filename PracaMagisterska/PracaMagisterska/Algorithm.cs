using System.Collections.Generic;
using Newtonsoft.Json;

namespace PracaMagisterska
{
	public class Algorithm
	{
		[JsonProperty]
		public string Name { get; set; }
		[JsonProperty]
		public string Description { get; set; }
		[JsonProperty]
		public List<AlgorithmParameter> Parameters { get; set; } = new List<AlgorithmParameter>();
	}
}

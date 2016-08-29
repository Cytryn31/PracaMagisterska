using System.Collections.Generic;
using Newtonsoft.Json;
using PracaMagisterska.Common;

namespace PracaMagisterska
{
	public class MinutiaeWithImage<T>
	{
		[JsonProperty]
		public string ImagePath { get; set; }
		[JsonProperty]
		public List<T> Minutiaes { get; set; }
	}
}

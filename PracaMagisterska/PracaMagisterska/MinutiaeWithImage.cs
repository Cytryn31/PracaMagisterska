using System.Collections.Generic;
using Newtonsoft.Json;
using PracaMagisterska.Common;
using System.Drawing;

namespace PracaMagisterska
{
	public class MinutiaeWithImage<T>
	{
		[JsonProperty]
		public Image ImagePath { get; set; }
		[JsonProperty]
		public List<T> Minutiaes { get; set; } = new List<T>();
	}
}

using System;
using Newtonsoft.Json;

namespace PracaMagisterska
{
	public class AlgorithmParameter
	{
		[JsonProperty]
		public string Name { get; set; }
		[JsonProperty]
		public string Description { get; set; }
		[JsonProperty]
		public string Value { get; set; }
		[JsonProperty]
		public string PossibleValues { get; set; }
		[JsonProperty]
		public ParameterType ParameterType { get; set; }
	}

	public enum ParameterType
	{
		Int = 0,
		Text = 1,
		Double = 2,
		Enum = 3,
        NzInt = 4,
        NzDouble = 5
    }

}

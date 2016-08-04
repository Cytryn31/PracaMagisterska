using System.Collections.Generic;

namespace PracaMagisterska
{
	public class MinutiaeManager : Singleton<MinutiaeManager>
	{
		public List<Minutiae> Container { get; set; } = new List<Minutiae>();
	}
}

namespace PracaMagisterska
{
	public class Bootstrapper
	{
		public static void Initialize()
		{
			log4net.Config.XmlConfigurator.Configure();
		}
	}
}

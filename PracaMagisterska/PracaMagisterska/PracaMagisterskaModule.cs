using Autofac;

namespace PracaMagisterska
{
	class PracaMagisterskaModule :  Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule(new LoggingModule());
		}
	}
}

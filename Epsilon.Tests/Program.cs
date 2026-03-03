using System.Reflection;
using NUnitLite;

namespace Epsilon.Tests
{
	public static class Program
	{
		public static int Main(string[] args)
		{
			
			return new AutoRun(Assembly.GetExecutingAssembly()).Execute(args);
		}
	}
}

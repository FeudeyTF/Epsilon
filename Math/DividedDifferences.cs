namespace Epsilon.Math
{
	public class DividedDifferences
	{
		private readonly double[] _functionDomain;

		private readonly double[] _functionCodomain;

		public DividedDifferences(double[] functionDomain, double[] functionCodomain)
		{
			_functionDomain = functionDomain;
			_functionCodomain = functionCodomain;
		}

		public double Calculate(params int[] indexes)
		{
			double r = 0;
			for (int i = 0; i < indexes.Length; i++)
			{
				double m = 1;
				for (int l = 0; l < indexes.Length; l++)
				{
					if (l != i)
						m *= _functionDomain[indexes[i]] - _functionDomain[indexes[l]];
				}
				r += _functionCodomain[indexes[i]] / m;
			}
			return r;
		}
	}
}

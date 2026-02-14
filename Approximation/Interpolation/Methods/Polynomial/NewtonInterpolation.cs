using Epsilon.Math;

namespace Epsilon.Approximation.Interpolation.Methods.Polynomial
{
	public class NewtonInterpolation : IPolynomialInterpolation
	{
		private readonly double[] _functionDomain;

		private readonly double[] _functionCodomain;

		private readonly DividedDifferences _dividedDifferences;

		public NewtonInterpolation(double[] functionDomain, double[] functionCodomain)
		{
			_functionCodomain = functionCodomain;
			_functionDomain = functionDomain;
			if (_functionDomain.Length != _functionCodomain.Length)
				throw new Exception("Function's codomain and domain lengths must be equal!");
			_dividedDifferences = new(_functionDomain, _functionCodomain);
		}

		public double Interpolate(double x)
		{
			double r = _functionCodomain[0];
			for (int i = 1; i < _functionDomain.Length; i++)
			{
				double deltaX = 1;
				int[] indexes = new int[i + 1];
				indexes[0] = 0;
				for (int j = 1; j < i + 1; j++)
				{
					indexes[j] = j;
					deltaX *= x - _functionDomain[j - 1];
				}
				r += deltaX * _dividedDifferences.Calculate(indexes);
			}
			return r;
		}

	}
}

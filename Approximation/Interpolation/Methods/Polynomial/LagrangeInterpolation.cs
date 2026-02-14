namespace Epsilon.Approximation.Interpolation.Methods.Polynomial
{
	public class LagrangeInterpolation : IPolynomialInterpolation
	{
		private readonly double[] _functionDomain;

		private readonly double[] _functionCodomain;

		public LagrangeInterpolation(double[] functionDomain, double[] functionCodomain)
		{
			_functionCodomain = functionCodomain;
			_functionDomain = functionDomain;
			if (_functionDomain.Length != _functionCodomain.Length)
				throw new Exception("Function's codomain and domain lengths must be equal!");
		}

		public double Interpolate(double x)
		{
			double r = 0;
			for (int i = 0; i < _functionDomain.Length; i++)
				r += (OmegaK(x, i) / OmegaK(_functionDomain[i], i)) * _functionCodomain[i];
			return r;
		}

		private double OmegaK(double x, int k)
		{
			double r = 1;
			for (int j = 0; j < _functionDomain.Length; j++)
				if (j != k)
					r *= x - _functionDomain[j];
			return r;
		}
	}
}

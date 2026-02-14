using Epsilon.Equations.Linear.Systems;
using Epsilon.Math;
using Epsilon.Matrices;

namespace Epsilon.Approximation.Interpolation.Methods.Spline
{
	public class SplineInterpolation : IInterpolation
	{
		private readonly double[] _functionDomain;

		private readonly double[] _functionCodomain;

		private readonly double[] _a;

		private readonly double[] _b;

		private readonly double[] _c;

		private readonly double[] _d;

		private readonly ILinearEquationsSystemSolver _solver;

		private readonly DividedDifferences _dividedDifferences;

		public SplineInterpolation(double[] functionDomain, double[] functionCodomain, ILinearEquationsSystemSolver solver)
		{
			_functionCodomain = functionCodomain;
			_functionDomain = functionDomain;
			_solver = solver;
			_dividedDifferences = new(_functionDomain, _functionCodomain);

			if (_functionDomain.Length != _functionCodomain.Length)
				throw new Exception("Function's codomain and domain lengths must be equal!");
			if (_functionDomain.Length < 4)
				throw new Exception("Function's domain must have at least 4 points");

			var ratio = CalculateRatio();
			_a = ratio[0];
			_b = ratio[1];
			_c = ratio[2];
			_d = ratio[3];
		}

		private double[][] CalculateRatio()
		{
			double[] a = new double[_functionDomain.Length - 1];
			double[] b = new double[_functionDomain.Length - 1];
			double[] c = new double[_functionDomain.Length - 1];
			double[] d = new double[_functionDomain.Length - 1];
			double[] h = new double[_functionDomain.Length - 1];

			for (int i = 0; i < _functionCodomain.Length - 1; i++)
			{
				h[i] = _functionDomain[i + 1] - _functionDomain[i];
				a[i] = _functionCodomain[i];
			}

			int n = _functionDomain.Length;
			SquareMatrix<double> hValues = new(n);
			for (int i = 0; i < n; i++)
			{
				if (i == 0)
				{
					hValues[i, 0] = -h[i];
					hValues[i, 1] = h[i];
				}
				else if (i == n - 1)
				{
					hValues[i, i] = -h[i - 1];
					hValues[i, i - 1] = h[i - 1];
				}
				else
				{
					hValues[i, i] = 2 * (h[i - 1] + h[i]);
					hValues[i, i - 1] = h[i - 1];
					hValues[i, i + 1] = h[i];
				}
			}

			Vector<double> a1 = new(n);

			a1[0, 0] = h[0] * h[0] * _dividedDifferences.Calculate([0, 1, 2, 3]);
			a1[n - 1, 0] = -h[n - 2] * h[n - 2] * _dividedDifferences.Calculate([n - 4, n - 3, n - 2, n - 1]);

			for (int i = 1; i < n - 1; i++)
				a1[i, 0] = _dividedDifferences.Calculate([i, i + 1]) - _dividedDifferences.Calculate([i - 1, i]);

			var sigmas = _solver.Solve(hValues, a1);
			for (int i = 0; i < n - 1; i++)
			{
				b[i] = (_functionCodomain[i + 1] - _functionCodomain[i]) / h[i] - h[i] * (sigmas[i + 1] + 2 * sigmas[i]);
				c[i] = 3 * sigmas[i];
				d[i] = (sigmas[i + 1] - sigmas[i]) / h[i];
			}

			return [a, b, c, d];
		}

		public double Interpolate(double x)
		{
			for (int i = 0; i < _functionDomain.Length - 1; i++)
			{
				if (_functionDomain[i] <= x && x < _functionDomain[i + 1])
				{
					var t = x - _functionDomain[i];
					return _a[i] + _b[i] * t + _c[i] * t * t + _d[i] * t * t * t;
				}
			}

			throw new Exception($"Value '{x}' is outside the interpolation domain!");
		}
	}
}

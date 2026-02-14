using Epsilon.Equations.Linear.Systems;
using Epsilon.Matrices;

namespace Epsilon.Integration
{
	public class NewtonCotesIntegration : IIntegration
	{
		public double EstimatedError { get; private set; }

		public double Flag { get; private set; }

		public int NoFun { get; private set; }

		private readonly Func<double, double> _function;

		private readonly double _absoluteError;

		private readonly double _relativeError;

		private readonly int _pointsCount = 9;

		private readonly ILinearEquationsSystemSolver _solver;

		public NewtonCotesIntegration(Func<double, double> function, double absoluteError, double relativeError, ILinearEquationsSystemSolver solver)
		{
			_function = function;
			_absoluteError = absoluteError;
			_relativeError = relativeError;
			_solver = solver;
		}

		public double Integrate(double a, double b)
		{
			EstimatedError = 0;
			Flag = 0;
			NoFun = 0;
			double result = 0;

			Stack<(double left, double right, int depth)> stack = new();
			stack.Push((a, b, 0));

			while (stack.Count > 0)
			{
				var (left, right, depth) = stack.Pop();
				double mid = (left + right) / 2;
				double p = Integrate(left, right, _pointsCount);
				double q = Integrate(left, right, _pointsCount * 2);

				double error = System.Math.Abs(p - q) / 1023;
				double maxError = (right - left) / (b - a) * System.Math.Max(
					_absoluteError,
					_relativeError * System.Math.Abs(q)
				);

				if (error <= maxError || depth >= 30 || NoFun >= 5000)
				{
					if (depth >= 30)
						Flag++;
					if (!double.IsNaN(q) && !double.IsNaN(p))
					{
						result += p;
						EstimatedError += error;
					}
				}
				else
				{
					stack.Push((mid, right, depth + 1));
					stack.Push((left, mid, depth + 1));
				}
			}

			return result;
		}

		private double Integrate(double a, double b, int pointsCount)
		{
			var h = (b - a) / (pointsCount - 1);
			double[] xValues = new double[pointsCount];
			for (int i = 0; i < xValues.Length; i++)
				xValues[i] = a + i * h;

			SquareMatrix<double> matrix = new(pointsCount);
			Vector<double> values = new(pointsCount);
			for (int i = 0; i < pointsCount; i++)
			{
				for (int j = 0; j < pointsCount; j++)
					matrix[i, j] = System.Math.Pow(xValues[j], i);
				values[i] = (System.Math.Pow(b, i + 1) - System.Math.Pow(a, i + 1)) / (i + 1);
			}

			double r = 0;
			var aValues = _solver.Solve(matrix, values);
			for (int i = 0; i < aValues.Size; i++)
				r += aValues[i] * _function(xValues[i]);

			NoFun += pointsCount;
			return r;
		}
	}
}

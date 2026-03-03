using Epsilon.Matrices;

namespace Epsilon.Equations.Differential
{
	public class RungeKuttaFehlbergSolver : IDifferentialEquationSolver
	{
		private static double[] _c4 = [25.0 / 216.0, 0.0, 1408.0 / 2565.0, 2197.0 / 4104.0, -1.0 / 5.0, 0.0];

		private static double[] _c5 = [16.0 / 135.0, 0.0, 6656.0 / 12825.0, 28561.0 / 56430.0, -9.0 / 50.0, 2.0 / 55.0];

		private static double[] _a = [0.0, 0.25, 3.0 / 8.0, 12.0 / 13.0, 1.0, 0.5];

		private static double[][] _b =
			[
				[0.25],
				[3.0 / 32.0, 9.0 / 32.0],
				[1932.0 / 2197.0, -7200.0 / 2197.0, 7296.0 / 2197.0],
				[439.0 / 216.0, -8.0, 3680.0 / 513.0, -845.0 / 4104.0],
				[-8.0 / 27.0, 2.0, -3544.0 / 2565.0, 1859.0 / 4104.0, -11.0 / 40.0]
			];

		private readonly Func<double, Vector<double>, Vector<double>> _function;

		private readonly double _startX;

		private readonly Vector<double> _startY;

		private readonly double _absoluteError;

		private readonly double _relativeError;

		public RungeKuttaFehlbergSolver(Func<double, Vector<double>, Vector<double>> function, double startX, Vector<double> startY, double absoluteError, double relativeError)
		{
			_function = function;
			_startX = startX;
			_startY = startY;
			_absoluteError = absoluteError;
			_relativeError = relativeError;
		}

		public double Evaluate(double x)
		{
			double h = 0.001;
			double t = _startX;
			Vector<double> y = _startY;
			while (t < x)
			{
				if (t + h > x)
					h = x - t;
				Vector<double>[] k = new Vector<double>[6];
				k[0] = h * _function(t + _a[0] * h, y);
				k[1] = h * _function(t + _a[1] * h, y + _b[0][0] * k[0]);
				k[2] = h * _function(t + _a[2] * h, y + _b[1][0] * k[0] + _b[1][1] * k[1]);
				k[3] = h * _function(t + _a[3] * h, y + _b[2][0] * k[0] + _b[2][1] * k[1] + _b[2][2] * k[2]);
				k[4] = h * _function(t + _a[4] * h, y + _b[3][0] * k[0] + _b[3][1] * k[1] + _b[3][2] * k[2] + _b[3][3] * k[3]);
				k[5] = h * _function(t + _a[5] * h, y + _b[4][0] * k[0] + _b[4][1] * k[1] + _b[4][2] * k[2] + _b[4][3] * k[3] + _b[4][4] * k[4]);

				Vector<double> rk4 = y;
				Vector<double> rk5 = y;
				for (int i = 0; i < _c5.Length; i++)
				{
					rk4 += _c4[i] * k[i];
					rk5 += _c5[i] * k[i];
				}

				double tolerance = System.Math.Abs(rk5[0] - rk4[0]);
				double error = System.Math.Max(_absoluteError, _relativeError * h);

				if (tolerance <= error)
				{
					t += h;
					y = rk5;
				}

				h = 0.84 * h * System.Math.Pow(error / tolerance, 0.25);
			}
			return y[0];
		}
	}
}

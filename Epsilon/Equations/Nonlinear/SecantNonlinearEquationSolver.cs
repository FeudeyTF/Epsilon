namespace Epsilon.Equations.Nonlinear
{
	public class SecantNonlinearEquationSolver : INonlinearEquationSolver
	{
		private readonly Func<double, double> _function;

		private readonly double _tolerance;

		public SecantNonlinearEquationSolver(Func<double, double> function, double tolerance)
		{
			_function = function;
			_tolerance = tolerance;
		}

		public double Solve(double a, double b)
		{
			while (System.Math.Abs(b - a) > _tolerance)
			{
				double fa = _function(a);
				double fb = _function(b);
				var c = a - (b - a) * fa / (fb - fa);
				if (System.Math.Sign(fa) == System.Math.Sign(_function(c)))
					a = c;
				else
					b = c;
			}
			return b;
		}
	}
}

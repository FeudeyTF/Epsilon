namespace Epsilon.Equations.Nonlinear
{
	public class BisectionNonlinearEquationSolver : INonlinearEquationSolver
	{
		private readonly Func<double, double> _function;

		private readonly double _tolerance;

		public BisectionNonlinearEquationSolver(Func<double, double> function, double tolerance)
		{
			_function = function;
			_tolerance = tolerance;
		}

		public double Solve(double a, double b)
		{
			while (System.Math.Abs(b - a) > _tolerance)
			{
				var c = (a + b) / 2;
				if (System.Math.Sign(_function(a)) == System.Math.Sign(_function(c)))
					a = c;
				else
					b = c;
			}
			return b;
		}
	}
}

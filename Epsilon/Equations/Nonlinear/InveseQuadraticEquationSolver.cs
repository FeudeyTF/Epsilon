namespace Epsilon.Equations.Nonlinear
{
	public class InverseQuadraticNonlinearEquationSolver : INonlinearEquationSolver
	{
		private readonly Func<double, double> _function;

		private readonly double _tolerance;

		private readonly SecantNonlinearEquationSolver _secantSolver;

		public InverseQuadraticNonlinearEquationSolver(Func<double, double> function, double tolerance)
		{
			_function = function;
			_tolerance = tolerance;
			_secantSolver = new(function, tolerance);
		}

		public double Solve(double a, double b)
		{
			double x1 = a;
			double x2 = (a + b) / 2;
			double x3 = b;
			double x4 = 0;
			while (System.Math.Abs(x3 - x2) > _tolerance)
			{
				double f1 = _function(x1);
				double f2 = _function(x2);
				double f3 = _function(x3);
				if(f1 == f2 || f2 == f3 || f1 == f3)
					return _secantSolver.Solve(x1, x3);
				x4 = f2 * f3 / ((f1 - f2) * (f1 - f3)) * x1 + f1 * f3 / ((f2 - f1) * (f2 - f3)) * x2 + f1 * f2 / ((f3 - f1) * (f3 - f2)) * x3;
				x1 = x2;
				x2 = x3;
				x3 = x4;
			}
			return x4;
		}
	}
}

using Epsilon.Matrices;

namespace Epsilon.Equations.Linear.Systems
{
	public class GaussSolver : ILinearEquationsSystemSolver
	{
		public Vector<double> Solve(SquareMatrix<double> matrix, Vector<double> vector)
		{
			var n = vector.Size;
			var (l, u) = matrix.Decompose();

			Vector<double> x = new(n);
			Vector<double> y = new(n);
			for (int i = 0; i < n; i++)
			{
				y[i] = vector[i];
				for (int j = 0; j < i; j++)
					y[i] -= l[i, j] * y[j];
			}

			for (int i = n - 1; i >= 0; i--)
			{
				x[i] = y[i];
				for (int j = i + 1; j < n; j++)
					x[i] -= u[i, j] * x[j];
				x[i] /= u[i, i];
			}
			return x;
		}
	}
}

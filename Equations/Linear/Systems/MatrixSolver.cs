using Epsilon.Matrices;

namespace Epsilon.Equations.Linear.Systems
{
	public class MatrixSolver : ILinearEquationsSystemSolver
	{
		public Vector<double> Solve(SquareMatrix<double> a, Vector<double> b)
		{
			// WoW sO eFfIcIeNt AnD cOoL (No)
			Vector<double> r = new(b.Size);
			var m = a.Invert() * b;
			for (int i = 0; i < r.Size; i++)
				r[i] = m[i, 0];
			return r;
		}
	}
}

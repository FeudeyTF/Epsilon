using Epsilon.Matrices;

namespace Epsilon.Equations.Linear.Systems
{
	public interface ILinearEquationsSystemSolver
	{
		public Vector<double> Solve(SquareMatrix<double> matrix, Vector<double> vector);
	}
}

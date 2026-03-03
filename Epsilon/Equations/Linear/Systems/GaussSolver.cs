using Epsilon.Matrices;
using Epsilon.Matrices.Decompositions;

namespace Epsilon.Equations.Linear.Systems
{
	public class GaussSolver : ILinearEquationsSystemSolver
	{
		public Vector<double> Solve(SquareMatrix<double> matrix, Vector<double> vector)
		{
			return new LuDecomposition<double>(matrix).Solve(vector);
		}
	}
}

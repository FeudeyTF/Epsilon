using System.Numerics;

namespace Epsilon.Matrices.Decompositions
{
	public interface IDecomposition<TValue> where TValue : INumberBase<TValue>
	{
		public Vector<TValue> Solve(Vector<TValue> vector);
	}
}

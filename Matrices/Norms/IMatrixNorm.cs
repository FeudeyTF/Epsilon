using System.Numerics;

namespace Epsilon.Matrices.Norms
{
	public interface IMatrixNorm<TValue> where TValue : INumberBase<TValue>
	{
		public TValue Calculate(Matrix<TValue> matrix);
	}
}

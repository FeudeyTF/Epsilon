using System.Numerics;
using Epsilon.Matrices.Norms;

namespace Epsilon.Matrices.ConditionNumbers
{
	public class NormConditionNumber<TValue> : IConditionNumber<TValue> where TValue : INumberBase<TValue>
	{
		private readonly IMatrixNorm<TValue> _matrixNorm;

		public NormConditionNumber(IMatrixNorm<TValue> matrixNorm)
		{
			_matrixNorm = matrixNorm;
		}

		public TValue Calculate(SquareMatrix<TValue> matrix)
		{
			return _matrixNorm.Calculate(matrix) * _matrixNorm.Calculate(matrix.Invert());
		}
	}
}

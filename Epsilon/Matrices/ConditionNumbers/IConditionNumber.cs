using System.Numerics;

namespace Epsilon.Matrices.ConditionNumbers
{
    public interface IConditionNumber<TValue> where TValue : INumberBase<TValue>
    {
		public TValue Calculate(SquareMatrix<TValue> matrix);
    }
}

using System.Numerics;

namespace Epsilon.Matrices
{
	public class Vector<TValue> : Matrix<TValue> where TValue : INumberBase<TValue>
	{
		public int Size { get; }

		public Vector(int size) : base(size, 1)
		{
			Size = size;
		}

		public Vector(TValue[][] values) : base(values)
		{
			Size = values.Length;
		}

		public TValue this[int row]
		{
			get => _values[row][0];
			set => _values[row][0] = value;
		}
	}
}

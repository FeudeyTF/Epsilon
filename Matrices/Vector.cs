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

		public static Vector<TValue> operator *(TValue a, Vector<TValue> b)
		{
			Vector<TValue> result = new(b.Size);
			for(int i = 0; i < b.Size; i++)
				result[i] = b[i] * a;
			return result;
		}

		public static Vector<TValue> operator +(Vector<TValue> a, Vector<TValue> b)
		{
			Vector<TValue> result = new(b.Size);
			for(int i = 0; i < b.Size; i++)
				result[i] = b[i] + a[i];
			return result;
		}

		public static implicit operator Vector<TValue>(TValue[] values)
		{
			Vector<TValue> result = new(values.Length);
			for(int i = 0; i < values.Length; i++)
				result[i] = values[i];
			return result;
		}
	}
}

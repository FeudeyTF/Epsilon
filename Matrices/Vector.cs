using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Epsilon.Matrices
{
	[CollectionBuilder(typeof(VectorBuilder), nameof(VectorBuilder.Create))]
	public class Vector<TValue> : Matrix<TValue>, IEnumerable<TValue> where TValue : INumberBase<TValue>
	{
		public int Size { get; }

		public Vector(ReadOnlySpan<TValue> buffer) : this(buffer.Length)
		{
			_values = buffer.ToArray();
		}

		public Vector(int size) : base(size, 1)
		{
			if (size == 0)
				throw new Exception("Vector's size must be greater than zero!");
			Size = size;
		}

		public TValue this[int index]
		{
			get => _values[index];
			set => _values[index] = value;
		}

		public new IEnumerator<TValue> GetEnumerator()
		{
			for (int i = 0; i < Size; i++)
				yield return this[i];
		}

		public static Vector<TValue> operator *(TValue a, Vector<TValue> b)
		{
			Vector<TValue> result = new(b.Size);
			for (int i = 0; i < b.Size; i++)
				result[i] = b[i] * a;
			return result;
		}

		public static Vector<TValue> operator +(Vector<TValue> a, Vector<TValue> b)
		{
			Vector<TValue> result = new(b.Size);
			for (int i = 0; i < b.Size; i++)
				result[i] = b[i] + a[i];
			return result;
		}

		public static implicit operator Vector<TValue>(TValue[] array)
		{
			var size = array.Length;
			Vector<TValue> result = new(size);
			for (int i = 0; i < size; i++)
				result[i] = array[i];
			return result;
		}

	}

	public static class VectorBuilder
	{
		public static Vector<TValue> Create<TValue>(ReadOnlySpan<TValue> values) where TValue : INumberBase<TValue>
		{
			return new(values);
		}
	}
}

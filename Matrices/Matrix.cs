using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Epsilon.Matrices
{
	[CollectionBuilder(typeof(MatrixBuilder), nameof(MatrixBuilder.Create))]
	public class Matrix<TValue> : IEnumerable<TValue[]>, IMultiplyOperators<Matrix<TValue>, TValue, Matrix<TValue>> where TValue : INumberBase<TValue>
	{
		public int Rows { get; private set; }

		public int Columns { get; private set; }

		protected TValue[] _values;

		public Matrix(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
			_values = new TValue[Rows * Columns];
		}

		public Matrix(ReadOnlySpan<TValue[]> values)
		{
			if (values.Length == 0)
				throw new Exception("Matrix can't be empty!");
			var firstRow = values[0];
			if (firstRow.Length == 0)
				throw new Exception("Matrix can't be empty!");

			Rows = values.Length;
			Columns = firstRow.Length;
			_values = new TValue[Rows * Columns];

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
					this[i, j] = values[i][j];
			}
		}

		public Matrix<TValue> Transpose()
		{
			Matrix<TValue> result = new(Columns, Rows);
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
					result[j, i] = this[i, j];
			}
			return result;
		}

		public Matrix<TValue> Add(Matrix<TValue> matrix)
		{
			var result = Copy();
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Columns; j++)
					result[i, j] += matrix[i, j];
			return result;
		}

		public Matrix<TValue> Subtract(Matrix<TValue> matrix)
		{
			var result = Copy();
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Columns; j++)
					result[i, j] -= matrix[i, j];
			return result;
		}

		public Matrix<TValue> Multiply(TValue value)
		{
			var result = Copy();
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Columns; j++)
					result[i, j] *= value;
			return result;
		}

		public Matrix<TValue> Multiply(Matrix<TValue> matrix)
		{
			Matrix<TValue> result = new(Rows, matrix.Columns);
			for (int i = 0; i < result.Rows; i++)
			{
				for (int j = 0; j < result.Columns; j++)
				{
					TValue r = TValue.Zero;
					for (int k = 0; k < Columns; k++)
						r += this[i, k] * matrix[k, j];
					result[i, j] = r;
				}
			}
			return result;
		}

		public void SwitchColumns(int i, int j)
		{
			for (int k = 0; k < Rows; k++)
				(this[k, i], this[k, j]) = (this[k, j], this[k, i]);
		}

		public Matrix<TValue> Copy()
		{
			Matrix<TValue> r = new(Rows, Columns);
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Columns; j++)
					r[i, j] = this[i, j];
			return r;
		}

		public TValue this[int row, int column]
		{
			get => _values[row * Columns + column];
			set => _values[row * Columns + column] = value;
		}

		public IEnumerator<TValue[]> GetEnumerator()
		{
			for (int i = 0; i < Rows; i++)
			{
				TValue[] result = new TValue[Columns];
				for (int j = 0; j < Columns; j++)
					result[i] = this[i, j];
				yield return result;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public override string ToString()
		{
			string result = "";
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
					result += $"({this[i, j]})";
				result += '\n';
			}
			return result;
		}

		public static Matrix<TValue> operator +(Matrix<TValue> a, Matrix<TValue> b)
			=> a.Add(b);

		public static Matrix<TValue> operator -(Matrix<TValue> a, Matrix<TValue> b)
			=> a.Subtract(b);

		public static Matrix<TValue> operator *(Matrix<TValue> a, Matrix<TValue> b)
			=> a.Multiply(b);

		public static Matrix<TValue> operator *(Matrix<TValue> a, TValue b)
			=> a.Multiply(b);

		public static Matrix<TValue> operator *(TValue a, Matrix<TValue> b)
			=> b.Multiply(a);
	}

	public static class MatrixBuilder
	{
		public static Matrix<TValue> Create<TValue>(ReadOnlySpan<TValue[]> values) where TValue : INumberBase<TValue>
		{
			return new(values);
		}
	}
}

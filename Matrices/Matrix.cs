using System.Numerics;

namespace Epsilon.Matrices
{
	public class Matrix<TValue> where TValue : INumberBase<TValue>
	{
		public int Rows => _values.Length;

		public int Columns => _values.FirstOrDefault()?.Length ?? 0;

		protected readonly TValue[][] _values;

		public Matrix(int rows, int columns)
		{
			_values = new TValue[rows][];
			for (int x = 0; x < rows; x++)
				_values[x] = new TValue[columns];
		}

		public Matrix(TValue[][] values)
		{
			_values = values;
		}

		public Matrix<TValue> Add(Matrix<TValue> matrix)
		{
			for (int i = 0; i < _values.Length; i++)
				for (int j = 0; j < _values[i].Length; j++)
					this[i, j] += matrix[i, j];
			return this;
		}

		public Matrix<TValue> Multiply(TValue value)
		{
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Columns; j++)
					this[i, j] *= value;
			return this;
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

		public TValue this[int row, int column]
		{
			get => _values[row][column];
			set => _values[row][column] = value;
		}

		public override string ToString()
		{
			string result = "";
			for (int i = 0; i < _values.Length; i++)
				result += $"({string.Join(", ", _values[i])})\n";
			return result;
		}

		public static Matrix<TValue> operator +(Matrix<TValue> a, Matrix<TValue> b)
			=> a.Add(b);

		public static Matrix<TValue> operator *(Matrix<TValue> a, Matrix<TValue> b)
			=> a.Multiply(b);

		public static Matrix<TValue> operator *(Matrix<TValue> a, TValue b)
			=> a.Multiply(b);
	}
}

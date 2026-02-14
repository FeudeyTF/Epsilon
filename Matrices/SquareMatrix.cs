using System.Numerics;

namespace Epsilon.Matrices
{
	public class SquareMatrix<TValue> : Matrix<TValue> where TValue : INumberBase<TValue>
	{
		public int Size { get; }

		public SquareMatrix(int size) : base(size, size)
		{
			Size = size;
		}

		public SquareMatrix<TValue> Transpose()
		{
			for (int i = 0; i < _values.Length; i++)
			{
				for (int j = 0; j < _values[i].Length; j++)
					(_values[i][j], _values[j][i]) = (_values[j][i], _values[i][j]);
			}
			return this;
		}

		public SquareMatrix<TValue> Invert()
		{
			var determinant = GetDeterminant();
			if (determinant == TValue.Zero)
				throw new Exception("Matrix can't be inverted, because it's determinant equals to zero");

			SquareMatrix<TValue> matrix = new(Size);
			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size; j++)
					matrix[j, i] = GetCofactor(i, j);
			}

			matrix.Multiply(TValue.One / determinant);
			return matrix;
		}

		public TValue GetDeterminant()
		{
			SquareMatrix<TValue> diagonalMatrix = new(Size);
			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size; j++)
					diagonalMatrix[i, j] = this[i, j];
			}

			TValue determinantMultiplier = TValue.One;
			for (int i = 0; i < diagonalMatrix.Rows; i++)
			{
				if (diagonalMatrix[i, i] == TValue.Zero)
				{
					for (int j = i + 1; j < diagonalMatrix.Columns; j++)
					{
						if (diagonalMatrix[i, j] != TValue.Zero)
						{
							diagonalMatrix.SwitchColumns(i, j);
							break;
						}
					}

					if (diagonalMatrix[i, i] == TValue.Zero)
						return TValue.Zero;
					determinantMultiplier = -determinantMultiplier;
				}
			}

			for (int i = 0; i < diagonalMatrix.Size - 1; i++)
			{
				for (int j = i + 1; j < diagonalMatrix.Size; j++)
				{
					if (diagonalMatrix[i, i] == TValue.Zero)
						return TValue.Zero;
					TValue multiplier = diagonalMatrix[j, i] / diagonalMatrix[i, i];
					for (int k = 0; k < diagonalMatrix.Columns; k++)
						diagonalMatrix[j, k] -= diagonalMatrix[i, k] * multiplier;
				}
			}

			TValue result = TValue.One;
			for (int i = 0; i < diagonalMatrix.Size; i++)
				result *= diagonalMatrix[i, i];
			return determinantMultiplier * result;
		}

		public TValue GetMinor(int row, int column)
		{
			SquareMatrix<TValue> matrix = new(Size - 1);
			for (int i = 0; i < Size; i++)
			{
				if (i == row)
					continue;
				for (int j = 0; j < Columns; j++)
				{
					if (j == column)
						continue;
					matrix[i < row ? i : i - 1, j < column ? j : j - 1] = this[i, j];
				}
			}
			return matrix.GetDeterminant();
		}

		public TValue GetCofactor(int row, int column)
		{
			TValue multiplier = (row + column) % 2 == 0 ? TValue.One : -TValue.One;
			return multiplier * GetMinor(row, column);
		}

		public (SquareMatrix<TValue> l, SquareMatrix<TValue> u) Decompose()
		{
			SquareMatrix<TValue> l = new(Size);
			SquareMatrix<TValue> u = new(Size);
			for (int i = 0; i < l.Size; i++)
				l[i, i] = TValue.One;

			for (int i = 0; i < l.Size; i++)
			{
				for (int j = 0; j < l.Size; j++)
				{
					TValue s = TValue.Zero;
					if (i <= j)
					{
						for (int k = 0; k < i; k++)
							s += l[i, k] * u[k, j];
						u[i, j] = this[i, j] - s;
					}
					else
					{
						for (int k = 0; k < j; k++)
							s += l[i, k] * u[k, j];
						l[i, j] = (this[i, j] - s) / u[j, j];
					}
				}
			}
			return (l, u);
		}
	}
}

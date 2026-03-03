using System.Numerics;

namespace Epsilon.Matrices.Decompositions
{
	public class LuDecomposition<TValue> : IDecomposition<TValue> where TValue : INumberBase<TValue>
	{
		public SquareMatrix<TValue> L { get; private set; }

		public SquareMatrix<TValue> U { get; private set; }

		private readonly SquareMatrix<TValue> _matrix;

		public LuDecomposition(SquareMatrix<TValue> matrix)
		{
			_matrix = matrix;
			(L, U) = Decompose();
		}

		public (SquareMatrix<TValue> l, SquareMatrix<TValue> u) Decompose()
		{
			SquareMatrix<TValue> l = new(_matrix.Size);
			SquareMatrix<TValue> u = new(_matrix.Size);
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
						u[i, j] = _matrix[i, j] - s;
					}
					else
					{
						for (int k = 0; k < j; k++)
							s += l[i, k] * u[k, j];
						l[i, j] = (_matrix[i, j] - s) / u[j, j];
					}
				}
			}
			return (l, u);
		}

		public Vector<TValue> Solve(Vector<TValue> vector)
		{
			var n = vector.Size;
			Vector<TValue> x = new(n);
			Vector<TValue> y = new(n);

			for (int i = 0; i < n; i++)
			{
				y[i] = vector[i];
				for (int j = 0; j < i; j++)
					y[i] -= L[i, j] * y[j];
			}

			for (int i = n - 1; i >= 0; i--)
			{
				x[i] = y[i];
				for (int j = i + 1; j < n; j++)
					x[i] -= U[i, j] * x[j];
				x[i] /= U[i, i];
			}
			return x;
		}
	}
}

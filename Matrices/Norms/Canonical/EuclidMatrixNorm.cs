using System.Numerics;

namespace Epsilon.Matrices.Norms.Canonical
{
	public class EuclidMatrixNorm : IMatrixNorm<double>
	{
		public double Calculate(Matrix<double> matrix)
		{
			double sum = 0;
			for (int i = 0; i < matrix.Rows; i++)
			{
				for (int j = 0; j < matrix.Columns; j++)
					sum += matrix[i, j] * matrix[i, j];
			}
			return System.Math.Sqrt(sum);
		}
	}
}

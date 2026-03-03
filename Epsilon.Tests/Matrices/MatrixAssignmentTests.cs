using Epsilon.Matrices;
using NUnit.Framework;

namespace Epsilon.Tests.Matrices
{
	[TestFixture, Category("Matrices")]
	public class MatrixAssignmentTests
	{
		[Test]
		public void MatrixAssignmentTest()
		{
			Matrix<double> matrix = [
				[1, 2, 3],
				[4, 5, 6]
			];

			Assert.That(matrix.Rows, Is.EqualTo(2));
			Assert.That(matrix.Columns, Is.EqualTo(3));

			for (int i = 0; i < matrix.Rows; i++)
			{
				for (int j = 0; j < matrix.Columns; j++)
					Assert.That(matrix[i, j], Is.EqualTo(i * matrix.Columns + j + 1));
			}
		}

		[Test]
		public void VectorAssignmentTest()
		{
			Vector<double> vector = [1, 2, 3];

			Assert.That(vector.Size, Is.EqualTo(3));

			Assert.That(vector.Rows, Is.EqualTo(3));
			Assert.That(vector.Columns, Is.EqualTo(1));

			for (int i = 0; i < vector.Size; i++)
			{
				Assert.That(vector[i], Is.EqualTo(i + 1));
				Assert.That(vector[0, i], Is.EqualTo(i + 1));
			}

			int index = 0;
			foreach (double a in vector)
			{
				Assert.That(vector[index], Is.EqualTo(a));
				index++;
			}
		}

		[Test]
		public void SquareMatrixAssignmentTest()
		{
			SquareMatrix<double> matrix = [
				[1, 2, 3],
				[4, 5, 6],
				[7, 8, 9]
			];

			Assert.That(matrix.Size, Is.EqualTo(3));

			Assert.That(matrix.Rows, Is.EqualTo(3));
			Assert.That(matrix.Columns, Is.EqualTo(3));

			for (int i = 0; i < matrix.Size; i++)
			{
				for (int j = 0; j < matrix.Size; j++)
					Assert.That(matrix[i, j], Is.EqualTo(i * matrix.Size + j + 1));
			}
		}
	}
}

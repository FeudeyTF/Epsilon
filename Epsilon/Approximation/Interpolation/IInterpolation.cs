namespace Epsilon.Approximation.Interpolation
{
	public interface IInterpolation : IApproximation
	{
		public double Interpolate(double x);

		double IApproximation.Approximate(double x)
			=> Interpolate(x);
	}
}

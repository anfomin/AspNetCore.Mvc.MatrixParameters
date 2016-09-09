using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCore.Mvc.MatrixParameters
{
	/// <summary>
	/// Provides binding source for matrix parameters.
	/// </summary>
	public static class MatrixBindingSource
	{
		/// <summary>
		/// A <see cref="BindingSource"/> for the request matrix parameters.
		/// </summary>
		public static readonly BindingSource Matrix = new BindingSource("Matrix", "Matrix", isGreedy: false, isFromRequest: true);
	}
}
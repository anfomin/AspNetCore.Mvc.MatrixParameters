using System;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Mvc.MatrixParameters
{
	/// <summary>
	/// Provides matrix parameters for HttpRequest.
	/// </summary>
	public class MatrixFeature : IMatrixFeature
	{
		/// <inheritdoc/>
		public IQueryCollection MatrixParams { get; set; }

		/// <summary>
		/// Initializes HttpContext feature with matrix parameters.
		/// </summary>
		/// <param name="matrixParams">Initial matrix parameters.</param>
		public MatrixFeature(IQueryCollection matrixParams)
		{
			if (matrixParams == null)
				throw new ArgumentNullException(nameof(matrixParams));
			MatrixParams = matrixParams;
		}
	}
}
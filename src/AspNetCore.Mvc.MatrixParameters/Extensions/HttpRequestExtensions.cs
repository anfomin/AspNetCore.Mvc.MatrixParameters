using System;
using AspNetCore.Mvc.MatrixParameters;

namespace Microsoft.AspNetCore.Http
{
	public static class HttpRequestExtensions
	{
		/// <summary>
		/// Returns HttpRequest matrix parameters.
		/// </summary>
		public static IQueryCollection GetMatrix(this HttpRequest request)
		{
			var feature = request.HttpContext.Features.Get<IMatrixFeature>();
			if (feature == null)
				throw new InvalidOperationException("IMatrixFeature is not registered. Add \"app.UseMatrixParams()\" to the application pipeline.");

			return feature.MatrixParams;
		}
	}
}
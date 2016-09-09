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
			return feature?.MatrixParams;
		}
	}
}
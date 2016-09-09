using AspNetCore.Mvc.MatrixParameters;

namespace Microsoft.AspNetCore.Builder
{
	/// <summary>
	/// Extension methods for <see cref="IApplicationBuilder"/> to add <see cref="MatrixParamsMiddleware"/>.
	/// </summary>
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Overwrites request path extracting HTTP matrix parameters into <see cref="IMatrixFeature"/>.
		/// </summary>
		/// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
		public static IApplicationBuilder UseMatrixParams(this IApplicationBuilder app)
		{
			return app.UseMiddleware<MatrixParamsMiddleware>();
		}
	}
}
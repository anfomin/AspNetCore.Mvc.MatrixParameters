using Microsoft.AspNetCore.Http;

namespace AspNetCore.Mvc.MatrixParameters
{
	/// <summary>
	/// Provides matrix parameters for HttpRequest.
	/// </summary>
	public interface IMatrixFeature
	{
		/// <summary>
		/// Gets or sets matrix parameters for HttpRequest.
		/// </summary>
		IQueryCollection MatrixParams { get; set; }
	}
}
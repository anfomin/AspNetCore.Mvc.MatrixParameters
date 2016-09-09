using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCore.Mvc.MatrixParameters
{
	/// <summary>
	/// Setups <see cref="MatrixValueProviderFactory"/> for MVC.
	/// </summary>
	public class MatrixMvcOptions : IConfigureOptions<MvcOptions>
	{
		/// <inheritdoc/>
		public void Configure(MvcOptions options)
		{
			options.ValueProviderFactories.Add(new MatrixValueProviderFactory());
		}
	}
}
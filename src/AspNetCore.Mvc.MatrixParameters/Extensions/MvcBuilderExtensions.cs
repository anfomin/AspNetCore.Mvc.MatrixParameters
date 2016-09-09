using AspNetCore.Mvc.MatrixParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class MvcBuilderExtensions
	{
		/// <summary>
		/// Adds matrix parameters to the databinding source.
		/// You should add <code>app.UseMatrixParams()</code> to the application pipeline before <code>app.UseMvc()</code>.
		/// </summary>
		public static IMvcCoreBuilder AddMatrixDataBinding(this IMvcCoreBuilder builder)
		{
			builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MatrixMvcOptions>());
			return builder;
		}
	}
}
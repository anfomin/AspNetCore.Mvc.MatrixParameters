using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCore.Mvc.MatrixParameters
{
	/// <summary>
	/// A <see cref="IValueProviderFactory"/> that creates <see cref="IValueProvider"/> instances that
	/// read values from the request matrix parameters.
	/// </summary>
	public class MatrixValueProviderFactory : IValueProviderFactory
	{
		/// <inheritdoc />
		public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			var valueProvider = new QueryStringValueProvider(
				MatrixBindingSource.Matrix,
				context.ActionContext.HttpContext.Request.GetMatrix(),
				CultureInfo.InvariantCulture);
			context.ValueProviders.Add(valueProvider);
			return TaskCache.CompletedTask;
		}
	}
}

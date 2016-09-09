using System;
using AspNetCore.Mvc.MatrixParameters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc
{
	/// <summary>
	/// Specifies that a parameter or property should be bound using the request matrix parameters.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class FromMatrixAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider
	{
		/// <inheritdoc />
		public BindingSource BindingSource { get { return MatrixBindingSource.Matrix; } }

		/// <inheritdoc />
		public string Name { get; set; }
	}
}
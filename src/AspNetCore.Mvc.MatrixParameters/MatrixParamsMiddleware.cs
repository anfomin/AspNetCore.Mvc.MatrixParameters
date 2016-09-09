using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AspNetCore.Mvc.MatrixParameters
{
	/// <summary>
	/// Overwrites request path extracting HTTP matrix parameters into <see cref="IMatrixFeature"/>.
	/// </summary>
	public class MatrixParamsMiddleware
	{
		readonly RequestDelegate _next;

		public MatrixParamsMiddleware(RequestDelegate next)
		{
			if (next == null)
				throw new ArgumentNullException(nameof(next));
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			string remainingPath;
			var matrixParams = Parse(context.Request.Path, out remainingPath);
			context.Request.Path = remainingPath;
			context.Features.Set<IMatrixFeature>(new MatrixFeature(matrixParams));
			await _next(context);
		}

		/// <summary>
		/// Parses request path into matrix parameters.
		/// </summary>
		/// <param name="path">Request path.</param>
		/// <param name="remaining">Request path without matrix parameters.</param>
		/// <returns>Matrix parameters.</returns>
		protected virtual IQueryCollection Parse(string path, out string remaining)
		{
			// get path last part
			int slashIndex = path.LastIndexOf('/');
			string part;
			if (slashIndex == -1)
			{
				part = path;
				remaining = "";
			}
			else
			{
				part = path.Substring(slashIndex + 1);
				remaining = path.Substring(0, slashIndex + 1);
			}

			// get matrix parameters
			string[] items = part.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			if (items.Length > 0)
				remaining += items[0];

			var pars = new Dictionary<string, StringValues>();
			for (int i = 1; i < items.Length; i++)
			{
				string item = WebUtility.UrlDecode(items[i]);
				int index = item.IndexOf('=');
				if (index == -1 || index == item.Length - 1)
					pars[item] = StringValues.Empty;
				else
					pars[item.Substring(0, index)] = item.Substring(index + 1);
			}

			return new MatrixCollection(pars);
		}
	}
}
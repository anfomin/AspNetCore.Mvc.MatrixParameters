using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
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
		readonly Regex _outletsRegex = new Regex(@"(\([^)]+\))*$", RegexOptions.Compiled);

		public MatrixParamsMiddleware(RequestDelegate next)
		{
			if (next == null)
				throw new ArgumentNullException(nameof(next));
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			string pathWithoutMatrixParams = RemoveOutlets(context.Request.Path);
			var matrixParams = Parse(pathWithoutMatrixParams, out pathWithoutMatrixParams);

			context.Request.Path = pathWithoutMatrixParams;
			context.Features.Set<IMatrixFeature>(new MatrixFeature(matrixParams));
			await _next(context);
		}

		/// <summary>
		/// Removed outlets from the request path.
		/// </summary>
		protected virtual string RemoveOutlets(string path)
		{
			var match = _outletsRegex.Match(path);
			if (!match.Success)
				return path;
			return path.Substring(0, match.Index);
		}

		/// <summary>
		/// Parses request path into matrix parameters.
		/// </summary>
		/// <param name="path">Request path.</param>
		/// <param name="pathWithoutMatrixParams">Request path without matrix parameters.</param>
		/// <returns>Matrix parameters for the last path part.</returns>
		protected virtual IQueryCollection Parse(string path, out string pathWithoutMatrixParams)
		{
			MatrixCollection matrix = null;
			string[] parts = path.Split('/');
			for (int i = 0; i < parts.Length; i++)
			{
				string part = parts[i];
				matrix = GetMatrixParams(part, out part);
				parts[i] = part;
			}

			pathWithoutMatrixParams = String.Join("/", parts);
			return matrix ?? new MatrixCollection();
		}

		/// <summary>
		/// Returns matrix parameters for path part.
		/// </summary>
		/// <param name="part">Path part between '/'.</param>
		/// <param name="remaining">Path part without matrix parameters.</param>
		/// <returns>Matrix parameters for the path part.</returns>
		MatrixCollection GetMatrixParams(string part, out string remaining)
		{
			if (string.IsNullOrEmpty(part))
			{
				remaining = part;
				return null;
			}

			string[] items = part.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			remaining = items[0];
			if (items.Length == 1)
				return null;

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
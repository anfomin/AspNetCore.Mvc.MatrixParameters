using AspNetCore.Mvc.MatrixParameters;

namespace Microsoft.AspNetCore.Mvc
{
	public static class UrlHelperExtensions
	{
		/// <summary>
		/// Generates a URL with an absolute path for an action method, which contains the specified
		/// route <paramref name="values"/> and route <paramref name="matrixValues"/>.
		/// </summary>
		/// <param name="helper">The <see cref="IUrlHelper"/>.</param>
		/// <param name="values">An object that contains route values.</param>
		/// <param name="matrixValues">An object that contains route matrix values.</param>
		/// <returns>The generated URL.</returns>
		public static string Action(this IUrlHelper helper, object values, object matrixValues)
		{
			return Action(helper, null, null, values, matrixValues);
		}

		/// <summary>
		/// Generates a URL with an absolute path for an action method, which contains the specified
		/// <paramref name="action"/> name, route <paramref name="values"/> and route <paramref name="matrixValues"/>.
		/// </summary>
		/// <param name="helper">The <see cref="IUrlHelper"/>.</param>
		/// <param name="action">The name of the action method.</param>
		/// <param name="values">An object that contains route values.</param>
		/// <param name="matrixValues">An object that contains route matrix values.</param>
		/// <returns>The generated URL.</returns>
		public static string Action(this IUrlHelper helper, string action, object values, object matrixValues)
		{
			return Action(helper, action, null, values, matrixValues);
		}

		/// <summary>
		/// Generates a URL with an absolute path for an action method, which contains the specified
		/// <paramref name="action"/> name, <paramref name="controller"/> name, route <paramref name="values"/> and route <paramref name="matrixValues"/>.
		/// </summary>
		/// <param name="helper">The <see cref="IUrlHelper"/>.</param>
		/// <param name="action">The name of the action method.</param>
		/// <param name="controller">The name of the controller.</param>
		/// <param name="values">An object that contains route values.</param>
		/// <param name="matrixValues">An object that contains route matrix values.</param>
		/// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
		/// <param name="host">The host name for the URL.</param>
		/// <returns>The generated URL.</returns>
		public static string Action(this IUrlHelper helper, string action, string controller, object values, object matrixValues, string protocol = null, string host = null)
		{
			string url = helper.Action(action, controller, values, protocol, host);
			if (matrixValues != null)
				return AddMatrixParams(url, matrixValues);
			return url;
		}

		/// <summary>
		/// Generates a URL with an absolute path for the specified route <paramref name="values"/> and route <paramref name="matrixValues"/>.
		/// </summary>
		/// <param name="helper">The <see cref="IUrlHelper"/>.</param>
		/// <param name="values">An object that contains route values.</param>
		/// <param name="matrixValues">An object that contains route matrix values.</param>
		/// <returns>The generated URL.</returns>
		public static string RouteUrl(this IUrlHelper helper, object values, object matrixValues)
		{
			return RouteUrl(helper, null, values, matrixValues);
		}

		/// <summary>
		/// Generates a URL with an absolute path for the specified <paramref name="routeName"/>, route <paramref name="values"/> and route <paramref name="matrixValues"/>.
		/// </summary>
		/// <param name="helper">The <see cref="IUrlHelper"/>.</param>
		/// <param name="routeName">The name of the route that is used to generate URL.</param>
		/// <param name="values">An object that contains route values.</param>
		/// <param name="matrixValues">An object that contains route matrix values.</param>
		/// <returns>The generated URL.</returns>
		public static string RouteUrl(this IUrlHelper helper, string routeName, object values, object matrixValues)
		{
			string url = helper.RouteUrl(routeName, values);
			if (matrixValues != null)
				return AddMatrixParams(url, matrixValues);
			return url;
		}

		static string AddMatrixParams(string url, object values)
		{
			int index = url.LastIndexOf('?');
			string path;
			string queryString;
			if (index == -1)
			{
				path = url;
				queryString = "";
			}
			else
			{
				path = url.Substring(0, index);
				queryString = url.Substring(index);
			}

			var matrixParams = new MatrixCollection(values);
			return path + matrixParams + queryString;
		}
	}
}
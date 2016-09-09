using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;

namespace AspNetCore.Mvc.MatrixParameters
{
	/// <summary>
	/// The HttpRequest matrix parameters collection.
	/// </summary>
	public class MatrixCollection : QueryCollection
	{
		public MatrixCollection() {}

		public MatrixCollection(object values) : base(ValuesToDictionary(values)) {}

		public MatrixCollection(Dictionary<string, StringValues> store) : base(store) {}

		public MatrixCollection(MatrixCollection store) : base(store) {}

		public MatrixCollection(int capacity) : base(capacity) {}

		/// <inheritdoc />
		public override string ToString()
		{
			if (Count == 0)
				return string.Empty;
			return ";" + string.Join(";", this.Select(kv => kv.Value == StringValues.Empty ? kv.Key : $"{kv.Key}={kv.Value}"));
		}

		static Dictionary<string, StringValues> ValuesToDictionary(object values)
		{
			var dic = new Dictionary<string, StringValues>();
			foreach (var prop in values.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty))
				dic[prop.Name] = prop.GetValue(values).ToString();
			return dic;
		}
	}
}

using System.Collections;
using System.Text;

namespace RetrieveParallelScope
{
    /// <summary>
    /// See <see cref="IEnumerable"/> extension.
    /// </summary>
    public static class IEnumerableEx
    {
        /// <summary>
        /// Appends all items of an enumerable object to a separated string and returns it.
        /// Items that are <c>null</c> are returned as "(null)".
        /// An empty <paramref name="collection"/> is returned as "(empty)".
        /// </summary>
        public static string ItemsToString(IEnumerable collection, string itemEnclosure = null, string itemSeparator = ", ")
        {
            // Attention:
            // Similar code exists in ArrayEx.ValuesToString().
            // Changes here may have to be applied there too.

            var sb = new StringBuilder();

            var isFirst = true;
            foreach (object item in collection)
            {
                if (isFirst)
                    isFirst = false;
                else if (!string.IsNullOrEmpty(itemSeparator))
                    sb.Append(itemSeparator);

                if (!string.IsNullOrEmpty(itemEnclosure))
                    sb.Append(itemEnclosure);

                if (item != null)
                    sb.Append(item.ToString());
                else
                    sb.Append("(null)");

                if (!string.IsNullOrEmpty(itemEnclosure))
                    sb.Append(itemEnclosure);
            }

            if (isFirst) // i.e. no items.
                sb.Append("(empty)");

            return (sb.ToString());
        }
    }
}

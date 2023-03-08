namespace Mitrol.Framework.Domain.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class LinqExtensions
    {
        /// <summary>
        /// Restituisce una lista ordinata in human order in base a selector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderNatural<T>(this IEnumerable<T> source, Func<T, string> selector)
        {
            // ToArray() è necessaria per avere immediatamente l'intera collezione per il corretto ordinamento.
            var array = source.ToArray();

            int max = array
                .SelectMany(i => Regex.Matches(selector(i), @"\d+").Cast<Match>().Select(m => (int?)m.Value.Length))
                .Max() ?? 0;

            return array.OrderBy(i => Regex.Replace(selector(i), @"\d+", m => m.Value.PadLeft(max, '0')));
        }
    }
}

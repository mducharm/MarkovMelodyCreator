using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMelodyCreator.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<(int, T)> WithIndex<T>(this IEnumerable<T> values)
    {
        var idx = 0;

        foreach (var item in values)
        {
            yield return (idx, item);
            idx++;
        }

    }
}

using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Search
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(this IEnumerable<TSource> source, int batchSize)
        {
            var batch = new List<TSource>();

            foreach (var item in source)
            {
                batch.Add(item);

                if (batch.Count != batchSize) continue;

                yield return batch;
                batch = new List<TSource>();
            }

            if (batch.Any())
                yield return batch;
        }
    }
}
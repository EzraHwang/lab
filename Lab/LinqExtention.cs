using System;
using System.Collections.Generic;

namespace Lab
{
    public static class LinqExtention
    {
        public static List<TResource> JoeyWhere<TResource>(this List<TResource> resources, Func<TResource, bool> predicate)
        {
            var output = new List<TResource>();
            foreach (var item in resources)
            {
                if (predicate(item))
                {
                    output.Add(item);
                }
            }

            return output;
        }

        public static List<TSource> JoeyWhereWithIndex<TSource>(this List<TSource> numbers, Func<TSource, int, bool> predicate)
        {
            var result = new List<TSource>();
            var index = 0;
            foreach (var number in numbers)
            {
                if (predicate(number, index))
                {
                    result.Add(number);
                }
                index++;
            }

            return result;
        }
    }
}
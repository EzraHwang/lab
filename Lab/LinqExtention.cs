using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public static class LinqExtention
    {
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> sources)
        {
            return !sources.Any();
        }

        public static List<TResource> JoeyWhere<TResource>(this List<TResource> resources, Func<TResource, bool> predicate)
        {
            //var enumerator = resources.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    var item = enumerator.Current;
            //    if (predicate(item))
            //    {
            //        yield return item;
            //    }
            //}

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

        public static IEnumerable<TSource> JoeyWhereWithIndex<TSource>(this IEnumerable<TSource> numbers, Func<TSource, int, bool> predicate)
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

        public static IEnumerable<string> JoeySelect(this IEnumerable<string> urls, Func<string, string> selector)
        {
            return JoeySelect(urls, selector);
            //var result = new List<string>();
            //foreach (var url in urls)
            //{
            //    result.Add(selector(url));
            //}

            //return result;
        }

        public static IEnumerable<TResult> JoeySelect<TResource, TResult>(this IEnumerable<TResource> sources, Func<TResource, int, TResult> predicate)
        {
            var result = new List<TResult>();
            var index = 1;
            foreach (var resource in sources)
            {
                result.Add(predicate(resource, index));
                index++;
            }
            return result;
        }

        public static IEnumerable<TResult> JoeySelect<TResource, TResult>(this List<TResource> employees, Func<TResource, TResult> selector)
        {
            var result = new List<TResult>();
            foreach (var employee in employees)
            {
                result.Add(selector(employee));
            }

            return result;
        }

        public static bool JoeyAnyWithCondition(this IEnumerable<int> source, Func<int, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
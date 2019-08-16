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
    }
}
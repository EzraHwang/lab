using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyUnionTests
    {
        [Test]
        public void union_numbers()
        {
            var first = new[] { 1, 3, 5, 1 };
            var second = new[] { 5, 3, 7 };

            var actual = JoeyUnion(first, second);
            var expected = new[] { 1, 3, 5, 7 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyUnion(IEnumerable<int> first, IEnumerable<int> second)
        {
            //var enumerator = second.GetEnumerator();
            //var result = new List<int>(first);
            //while (enumerator.MoveNext())
            //{
            //    result.Add(enumerator.Current);
            //}
            //return result.Distinct();

            var hashSet = new HashSet<int>();
            var enumerator = first.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
            var enumerator1 = second.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                var current = enumerator1.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }
    }
}
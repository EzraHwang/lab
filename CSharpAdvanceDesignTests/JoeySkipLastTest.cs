using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpectedObjects;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    internal class JoeySkipLastTest
    {
        [Test]
        public void skip_last_2()
        {
            var numbers = new[] { 10, 20, 30, 40, 50 };
            var actual = JoeySkipLast(numbers, 2);

            var expected = new[] { 10, 20, 30 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeySkipLast(IEnumerable<int> numbers, int count)
        {
            var queue = new Queue<int>();
            var enumerator = numbers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var number = enumerator.Current;
                if (queue.Count == count)
                {
                    yield return queue.Dequeue();
                }
                queue.Enqueue(number);
            }
        }
    }
}
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyAverageTests
    {
        [Test]
        public void average_with_null_value()
        {
            var numbers = new int?[] { 2, 4, null, 6 };

            var actual = JoeyAverage(numbers);

            var expected = 4d;
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private double? JoeyAverage(IEnumerable<int?> numbers)
        {
            var enumerator = numbers.GetEnumerator();
            var result = default(double);
            var index = 0;
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current.HasValue)
                {
                    result += current.Value;
                    index++;
                }
            }

            return result / index;
        }
    }
}
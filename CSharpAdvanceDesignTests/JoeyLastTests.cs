using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyLastTests
    {
        [Test]
        public void get_last_number()
        {
            var numbers = new[] { 1, 2, 3, 4 };
            var actual = JoeyLast(numbers);
            4.ToExpectedObject().ShouldMatch(actual);
        }

        private int JoeyLast(IEnumerable<int> numbers)
        {
            var enumerator = numbers.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            var result = enumerator.Current;
            while (enumerator.MoveNext())
            {
                result = enumerator.Current;
            }

            return result;
        }
    }
}
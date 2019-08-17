using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyCountTests
    {
        [Test]
        public void count_of_numbers()
        {
            var numbers = new[] { 10, 20, 30, 40, 50 };

            var count = JoeyCount(numbers);
            var expected = 5;
            Assert.AreEqual(expected, count);
        }

        private int JoeyCount(IEnumerable<int> numbers)
        {
            if (numbers is ICollection<int> n)
            {
                return n.Count;
            }

            var enumerator = numbers.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                index++;
            }

            return index;
        }
    }
}
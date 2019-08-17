using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAnyTests
    {
        [Test]
        public void three_employees()
        {
            var Employees = new Employee[]
            {
                new Employee(),
                new Employee(),
                new Employee(),
            };

            var actual = JoeyAny(Employees);
            Assert.IsTrue(actual);
        }

        [Test]
        public void empty_employees()
        {
            var emptyEmployees = new Employee[]
            {
            };

            var actual = JoeyAny(emptyEmployees);
            Assert.IsFalse(actual);
        }

        [Test]
        public void any_number_greater_than_91()
        {
            var numbers = new[] { 87, 88, 91, 93, 0 };
            var actual = numbers.JoeyAnyWithCondition(item => item > 91);
            Assert.IsTrue(actual);
        }

        private bool JoeyAny(IEnumerable<Employee> employees)
        {
            return employees.GetEnumerator().MoveNext();
        }
    }
}
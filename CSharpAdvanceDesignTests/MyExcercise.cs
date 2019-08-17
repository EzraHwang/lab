using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyOrderByTests01
    {
        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyOrderByLastNameAndFirstName(
                employees,
                Comparer<string>.Default,
                employee => employee.LastName, Comparer<string>.Default, currentElement => currentElement.FirstName);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(IEnumerable<Employee> employees, IComparer<string> FirstKeyComparer, Func<Employee, string> firstKeySelector, IComparer<string> secondKeyComparer, Func<Employee, string> secondKeySelector)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var currentElement = elements[i];
                    var firstKeyCompareResult =
                        FirstKeyComparer.Compare(firstKeySelector(currentElement), firstKeySelector(minElement));
                    if (firstKeyCompareResult < 0)
                    {
                        minElement = currentElement;
                        index = i;
                    }
                    else if (firstKeyCompareResult == 0)
                    {
                        var secondKeyCompareResult = secondKeyComparer.Compare(secondKeySelector(currentElement), secondKeySelector(minElement));
                        if (secondKeyCompareResult < 0)
                        {
                            minElement = currentElement;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}
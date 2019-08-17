using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyOrderByTests
    {
        public class CombineKeyCompare : IComparer<Employee>
        {
            public CombineKeyCompare(Func<Employee, string> keySelector, IComparer<string> keyComparer)
            {
                KeySelector = keySelector;
                KeyComparer = keyComparer;
            }

            public Func<Employee, string> KeySelector { get; private set; }
            public IComparer<string> KeyComparer { get; private set; }

            public int Compare(Employee x, Employee y)
            {
                return KeyComparer.Compare(KeySelector(x), KeySelector(y));
            }
        }

        [Test]
        public void orderBy_lastName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyOrderByLastNameAndFirstName(employees,
                firstComparer: new CombineKeyCompare(currentElement => currentElement.LastName, Comparer<string>.Default),
                secondComparer: new CombineKeyCompare(currentElement => currentElement.FirstName, Comparer<string>.Default));

            var expected = new[]
            {
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            //expected.ToExpectedObject().ShouldMatch(actual);
        }

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

            Func<Employee, string> secondKeySelector = employee => employee.FirstName;
            IComparer<string> secondKeyComparer = Comparer<string>.Default;
            var actual = JoeyOrderByLastNameAndFirstName(employees, new CombineKeyCompare(
                employee => employee.LastName, Comparer<string>.Default), new CombineKeyCompare(secondKeySelector, secondKeyComparer));

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(IEnumerable<Employee> employees,
            IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
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
                    var firstCompareResult = firstComparer.Compare(currentElement, minElement);
                    if (firstCompareResult < 0)
                    {
                        minElement = currentElement;
                        index = i;
                    }
                    else if (firstCompareResult == 0)
                    {
                        var secondCompareResult = secondComparer.Compare(currentElement, minElement);
                        if (secondCompareResult < 0)
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
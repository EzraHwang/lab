using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class ComboCompare
    {
        //    public ComboCompare(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        //    {
        //        FirstComparer = firstComparer;
        //        SecondComparer = secondComparer;
        //    }

        //    public IComparer<Employee> FirstComparer { get; private set; }
        //    public IComparer<Employee> SecondComparer { get; private set; }
        //}

        //[TestFixture]
        //public class JoeyOrderByTests
        //{
        //    public class CombineKeyCompare : IComparer<Employee>
        //    {
        //        public CombineKeyCompare(Func<Employee, string> keySelector, IComparer<string> keyComparer)
        //        {
        //            KeySelector = keySelector;
        //            KeyComparer = keyComparer;
        //        }

        //        public Func<Employee, string> KeySelector { get; private set; }
        //        public IComparer<string> KeyComparer { get; private set; }

        //        public int Compare(Employee x, Employee y)
        //        {
        //            return KeyComparer.Compare(KeySelector(x), KeySelector(y));
        //        }
        //    }

        //    //[Test]
        //    //public void orderBy_lastName()
        //    //{
        //    //    var employees = new[]
        //    //    {
        //    //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    //        new Employee {FirstName = "Tom", LastName = "Li"},
        //    //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //    //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    //    };

        //    //    var actual = JoeyOrderByLastNameAndFirstName(employees, new ComboCompare(), firstComparer: new CombineKeyCompare(currentElement => currentElement.LastName, Comparer<string>.Default), secondComparer: new CombineKeyCompare(currentElement => currentElement.FirstName, Comparer<string>.Default));

        //    //    var expected = new[]
        //    //    {
        //    //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //    //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    //        new Employee {FirstName = "Tom", LastName = "Li"},
        //    //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    //    };

        //    //    //expected.ToExpectedObject().ShouldMatch(actual);
        //    //}

        //    [Test]
        //    public void orderBy_lastName_and_firstName()
        //    {
        //        var employees = new[]
        //        {
        //            new Employee {FirstName = "Joey", LastName = "Wang"},
        //            new Employee {FirstName = "Tom", LastName = "Li"},
        //            new Employee {FirstName = "Joseph", LastName = "Chen"},
        //            new Employee {FirstName = "Joey", LastName = "Chen"},
        //        };

        //        Func<Employee, string> secondKeySelector = employee => employee.FirstName;
        //        IComparer<string> secondKeyComparer = Comparer<string>.Default;
        //        var actual = JoeyOrderByLastNameAndFirstName(employees, new ComboCompare(new CombineKeyCompare(
        //            employee => employee.LastName, Comparer<string>.Default), new CombineKeyCompare(secondKeySelector, secondKeyComparer)));

        //        var expected = new[]
        //        {
        //            new Employee {FirstName = "Joey", LastName = "Chen"},
        //            new Employee {FirstName = "Joseph", LastName = "Chen"},
        //            new Employee {FirstName = "Tom", LastName = "Li"},
        //            new Employee {FirstName = "Joey", LastName = "Wang"},
        //        };

        //        expected.ToExpectedObject().ShouldMatch(actual);
        //    }

        //    private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(IEnumerable<Employee> employees, ComboCompare comboCompare)
        //    {
        //        //bubble sort
        //        var elements = employees.ToList();

        //        while (elements.Any())
        //        {
        //            var minElement = elements[0];
        //            var index = 0;
        //            for (int i = 1; i < elements.Count; i++)
        //            {
        //                var finalResult = 0;
        //                var currentElement = elements[i];
        //                finalResult = Compare(comboCompare, currentElement, minElement);
        //                if (finalResult < 0)
        //                {
        //                    minElement = currentElement;
        //                    index = i;
        //                }
        //            }

        //            elements.RemoveAt(index);
        //            yield return minElement;
        //        }
        //    }

        //    private static int Compare(ComboCompare comboCompare, Employee currentElement, Employee minElement)
        //    {
        //        int finalResult = 0;
        //        var firstCompareResult = comboCompare.FirstComparer.Compare(currentElement, minElement);
        //        if (firstCompareResult < 0)
        //        {
        //            finalResult = firstCompareResult;
        //        }
        //        else if (firstCompareResult == 0)
        //        {
        //            var secondCompareResult = comboCompare.SecondComparer.Compare(currentElement, minElement);
        //            if (secondCompareResult < 0)
        //            {
        //                finalResult = secondCompareResult;
        //            }
        //        }

        //        return finalResult;
        //    }
        //}

        #region 91 Code

        public class CombineKeyComparer : IComparer<Employee>
        {
            public CombineKeyComparer(Func<Employee, string> keySelector, IComparer<string> keyComparer)
            {
                KeySelector = keySelector;
                KeyComparer = keyComparer;
            }

            private Func<Employee, string> KeySelector { get; set; }
            private IComparer<string> KeyComparer { get; set; }

            public int Compare(Employee x, Employee y)
            {
                return KeyComparer.Compare(KeySelector(x), KeySelector(y));
            }
        }

        public class ComboComparer : IComparer<Employee>
        {
            public ComboComparer(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
            {
                FirstComparer = firstComparer;
                SecondComparer = secondComparer;
            }

            public IComparer<Employee> FirstComparer { get; private set; }
            public IComparer<Employee> SecondComparer { get; private set; }

            public int Compare(Employee x, Employee y)
            {
                var firstCompareResult = FirstComparer.Compare(x, y);
                if (firstCompareResult != 0)
                {
                    return firstCompareResult;
                }

                return SecondComparer.Compare(x, y);
            }
        }

        [TestFixture]
        public class JoeyOrderByTests
        {
            //[Test]
            //public void orderBy_lastName()
            //{
            //    var employees = new[]
            //    {
            //        new Employee {FirstName = "Joey", LastName = "Wang"},
            //        new Employee {FirstName = "Tom", LastName = "Li"},
            //        new Employee {FirstName = "Joseph", LastName = "Chen"},
            //        new Employee {FirstName = "Joey", LastName = "Chen"},
            //    };

            //    var actual = JoeyOrderByLastNameAndFirstName(employees);

            //    var expected = new[]
            //    {
            //        new Employee {FirstName = "Joseph", LastName = "Chen"},
            //        new Employee {FirstName = "Joey", LastName = "Chen"},
            //        new Employee {FirstName = "Tom", LastName = "Li"},
            //        new Employee {FirstName = "Joey", LastName = "Wang"},
            //    };

            //    expected.ToExpectedObject().ShouldMatch(actual);
            //}

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

                var firstComparer = new CombineKeyComparer(employee => employee.LastName, Comparer<string>.Default);

                var secondComparer = new CombineKeyComparer(employee => employee.FirstName, Comparer<string>.Default);

                var actual = JoeyOrderByLastNameAndFirstName(
                    employees,
                    new ComboComparer(firstComparer, secondComparer));

                var expected = new[]
                {
                    new Employee {FirstName = "Joey", LastName = "Chen"},
                    new Employee {FirstName = "Joseph", LastName = "Chen"},
                    new Employee {FirstName = "Tom", LastName = "Li"},
                    new Employee {FirstName = "Joey", LastName = "Wang"},
                };

                expected.ToExpectedObject().ShouldMatch(actual);
            }

            private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(
                IEnumerable<Employee> employees,
                IComparer<Employee> comparer)
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

                        var finalCompareResult = comparer.Compare(currentElement, minElement);

                        if (finalCompareResult < 0)
                        {
                            minElement = currentElement;
                            index = i;
                        }
                    }

                    elements.RemoveAt(index);
                    yield return minElement;
                }
            }
        }

        #endregion 91 Code
    }
}
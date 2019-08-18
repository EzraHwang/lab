using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyOrderByTests
    {
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

            //var actual = employees.JoeyOrderByLastNameAndFirstName(e => e.LastName);
            var actual = employees.JoeyOrderBy(e => e.LastName);

            var expected = new[]
            {
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
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
            var actual = employees.JoeyOrderByLastNameAndFirstName(new ComboCompare(new CombineKeyCompare(
                employee => employee.LastName, Comparer<string>.Default), new CombineKeyCompare(secondKeySelector, secondKeyComparer)));

            var expected = new[]
            {
                    new Employee {FirstName = "Joey", LastName = "Chen"},
                    new Employee {FirstName = "Joseph", LastName = "Chen"},
                    new Employee {FirstName = "Tom", LastName = "Li"},
                    new Employee {FirstName = "Joey", LastName = "Wang"},
                };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_lastName_and_firstName_use_thenBy()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = employees
                .JoeyOrderBy(e => e.LastName)
                .JoeyThenBy(e => e.FirstName);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}
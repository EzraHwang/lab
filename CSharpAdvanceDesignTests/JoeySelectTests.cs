using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeySelectTests
    {
        [Test]
        public void replace_http_to_https()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(url => url.Replace("http://", "https://"));
            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void append_port_9191_to_urls()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(url => $"{url}:9191");
            var expected = new List<string>
            {
                "http://tw.yahoo.com:9191",
                "https://facebook.com:9191",
                "https://twitter.com:9191",
                "http://github.com:9191",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void select_with_seq_no()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect((url, index) => Map(url, index));
            var expected = new List<string>
            {
                "1. http://tw.yahoo.com",
                "2. https://facebook.com",
                "3. https://twitter.com",
                "4. http://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }


        [Test]
        public void get_full_name_of_employees()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };

            var actual = employees.JoeySelect(e => $"{e.FirstName} {e.LastName}");
            var expected = new[]
            {
                "Joey Chen",
                "Tom Li",
                "David Chen",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }


        [Test]
        public void find_price_more_than_700_and_select_id_and_price()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = products
                .JoeyWhere(product => product.Price > 700)
                .JoeySelect(p => $"{p.Id}-{p.Price}");

            foreach (var item in actual)
            {
                Console.WriteLine(item);
            }
            var expected = new[]
            {
                "7-710",
                "8-780",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static string Map(string url, int index)
        {
            return $"{index}. {url}";
        }

        private static IEnumerable<string> GetUrls()
        {
            yield return "http://tw.yahoo.com";
            yield return "https://facebook.com";
            yield return "https://twitter.com";
            yield return "http://github.com";
        }

    }
}
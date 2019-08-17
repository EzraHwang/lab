using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab;
using NUnit.Framework;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class GroupSumTests
    {
        [Test]
        public void group_sum_of_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
            };
            //sum of all Saving of each group which 3 Account per group
            var actual = MyGroupSum(accounts);
            var expected = new[] { 60, 150, 240, 210 };
            //expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerator<int> MyGroupSum(IEnumerable<Account> accounts)
        {
            //91 code
            //var pageIndex = 0;
            //var pageSize = 3;
            //while (pageSize * pageIndex < accounts.Count())
            //{
            //    yield return accounts.Skip(pageIndex * pageSize).Take(pageSize).Sum(x => x.Saving);
            //    pageIndex++;
            //}

            yield return accounts.Skip(0).Take(3).Sum(x => x.Saving);
            yield return accounts.Skip(3).Take(3).Sum(x => x.Saving);
            yield return accounts.Skip(6).Take(3).Sum(x => x.Saving);
            yield return accounts.Skip(9).Take(3).Sum(x => x.Saving);
        }
    }

    public class Account
    {
        public int Saving { get; set; }
        public string Name { get; set; }
    }
}
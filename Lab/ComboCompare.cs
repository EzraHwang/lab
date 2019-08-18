using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboCompare : IComparer<Employee>
    {
        public ComboCompare(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<Employee> FirstComparer { get; private set; }
        public IComparer<Employee> SecondComparer { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            var firstCompareResult = FirstComparer.Compare(x, y);
            return firstCompareResult != 0 ? firstCompareResult : SecondComparer.Compare(x, y);
        }
    }
}
using System.Collections;
using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyOfTypeTests
    {
        public class ProfitValidator : IValidator<Product>
        {
            public bool Validate(Product model)
            {
                return model.Price - model.Cost >= 0;
            }
        }

        public class ProductPriceValidator : IValidator<Product>
        {
            public bool Validate(Product model)
            {
                return model.Price > 0;
            }
        }

        [Test]
        public void get_special_type_value_from_arguments()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"validator1", new ProfitValidator()},
                {"validator2", new ProductPriceValidator()},
                {"model", new Product {Price = 100, Cost = 111}},
            };

            var validators = JoeyOfType<IValidator<Product>>(arguments.Values);

            var product = JoeyOfType<Product>(arguments.Values).Single();

            var isValid = validators.All(x => x.Validate(product));

            Assert.IsFalse(isValid);
            //Assert.AreEqual(2, validators.Count());
        }

        private IEnumerable<TType> JoeyOfType<TType>(IEnumerable values)
        {
            var enumerator = values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                //Check if current type is belong to TType, if yes then change current type
                if (current is TType c)
                {
                    yield return c;
                }
            }
        }
    }
}
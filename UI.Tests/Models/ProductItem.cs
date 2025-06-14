

namespace UI.Tests.Models
{
    public class ProductItem
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public ProductItem(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}

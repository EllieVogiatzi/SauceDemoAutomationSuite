using Microsoft.Playwright;
using NUnit.Framework;
using System.Globalization;
using UI.Tests.Models;

namespace MyE2ETests.UI.Tests.Pages
{
    class CartPage
    {
        private readonly IPage _page;

        protected ILocator AllProducts => _page.Locator("//div[@data-test='inventory-item']");

        private const string RemoveButtonLocatorTemplate = "//div[text()='{0}']/ancestor::div[contains(@class,'cart_item')]//button";
        public CartPage(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Retrieves all items currently in the cart.
        /// </summary>
        /// <returns>A list of <see cref="ProductItem"/> representing the products in the cart.</returns>
        public async Task<List<ProductItem>> GetCartItems()
        {
            
            var productList = new List<ProductItem>();
            var count = await AllProducts.CountAsync();

            for (int i = 0; i < count; i++)
            {
                var product = this.AllProducts.Nth(i);
                var name = await product.Locator(".inventory_item_name").InnerTextAsync();
                var priceText = await product.Locator(".inventory_item_price").InnerTextAsync();
                double price = double.Parse(priceText.Replace("$", "").Trim(),CultureInfo.InvariantCulture);
                
                productList.Add(new ProductItem(name, price));
            }

            return productList;
        }

        /// Removes a specific item from the cart by product name.
        /// </summary>
        /// <param name="productName">The name of the product to remove.</param>
        public async Task RemoveItemFromCart(string productName)
        {
            var removeButton = _page.Locator(string.Format(RemoveButtonLocatorTemplate, productName));
            await removeButton.ClickAsync();
        }

        /// <summary>
        /// Removes all items from the cart.
        /// </summary>
        public async Task RemoveAllItemsFromCart()
        {
            var count = await AllProducts.CountAsync();

            for (int i = 0; i < count; i++)
            {
                var product = this.AllProducts.Nth(i);
                await product.Locator("button").ClickAsync();
            }
        }

        public void IsCartEmpty()
        {
            Assertions.Expect(AllProducts).ToHaveCountAsync(0);
        }

        /// <summary>
        /// Verifies that the items in the cart match the expected list of products.
        /// </summary>
        /// <param name="expectedListOfProducts">
        /// A list of expected products in the cart.
        /// <example>
        /// [
        ///   {
        ///     "Name": "Sauce Labs Backpack",
        ///     "Price": 29.99
        ///   },
        ///   {
        ///     "Name": "Sauce Labs Bike Light",
        ///     "Price": 9.99
        ///   }
        /// ]
        /// </example>
        /// </param>
        public async Task VerifyItemsInCart(List<ProductItem> expectedListOfProducts)
        {
            var productsInCartList = await GetCartItems();

            Assert.That(productsInCartList.Count, Is.EqualTo(expectedListOfProducts.Count), "Product count in cart does not match expected.");

            foreach (var expectedProduct in expectedListOfProducts)
            {
                var match = productsInCartList.Any(p =>
                    p.Name == expectedProduct.Name &&
                    Math.Abs(p.Price - expectedProduct.Price) < 0.01);

                Assert.That(match, Is.True, $"Expected product '{expectedProduct.Name}' with price '{expectedProduct.Price}' not found in cart.");
            }
        }
    }
}

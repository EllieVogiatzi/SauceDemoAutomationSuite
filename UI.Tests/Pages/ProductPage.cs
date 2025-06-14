using Microsoft.Playwright;
using NUnit.Framework;
using System.Globalization;
using UI.Tests.Models;

namespace MyE2ETests.UI.Tests.Pages
{
    class ProductPage
    {
        private readonly IPage _page;
        private const string AddToCartLocatorTemplate = "//div[text()='{0}']/ancestor::div[contains(@class,'inventory_item')]//button";
        private const string PriceLocatorTemplate = "//div[text()='{0}']/ancestor::div[contains(@class,'inventory_item')]//div[@data-test='inventory-item-price']";


        protected ILocator RemoveButton => _page.GetByText("Remove");
        protected ILocator CartBadge => _page.Locator("//span[@class='shopping_cart_badge']");
        protected ILocator Cart => _page.Locator("//div[@class='shopping_cart_container']");

        

        public ProductPage(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Adds the specified product to the cart by clicking the "Add to Cart" button for the given product name.
        /// </summary>
        /// <param name="productName">The name of the product to add to the cart.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddItemToCart(String productName)
        {
            var addItemToCartLocator = _page.Locator(string.Format(AddToCartLocatorTemplate, productName));
            await addItemToCartLocator.ClickAsync();
        }

        public async Task RemoveItemFromCart(String productName)
        {
            await RemoveButton.ClickAsync();
        }

        /// <summary>
        /// Gets the price of the specified product.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <returns>The price of the product as a float.</returns>
        public async Task<float> GetPriceFromProduct(String productName)
        {
            var priceLocator = _page.Locator(string.Format(PriceLocatorTemplate, productName));
            var priceText = await priceLocator.TextContentAsync(); // "$15.99"
            var numericPrice = priceText.Replace("$", "").Trim();      // "15.99"
            return float.Parse(numericPrice, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the product information for the specified product.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <returns>A ProductItem containing the product's name and price.</returns>
        public async Task<ProductItem> GetProductInfo(string productName)
        {
            var price = await GetPriceFromProduct(productName);
            return new ProductItem(productName, price);
        }

        /// <summary>
        /// Verifies that the cart badge displays the expected number.
        /// </summary>
        /// <param name="number">The expected number on the cart badge.</param>
        public async Task VerifyBadgeNumber(int number)
        {
            Assert.That( await CartBadge.TextContentAsync(), Does.Contain(number.ToString()));
        }

        public async Task NavigateToCart()
        {
            await Cart.ClickAsync();
        }
    }
}

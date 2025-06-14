using MyE2ETests.UI.Tests.Pages;
using MyE2ETests.UI.Tests.Utils;
using NUnit.Framework;
using UI.Tests.Models;
using UI.Tests.Pages;

namespace UI.Tests.Tests
{
    public class CartTests:TestBase
    {
        private LoginPage _loginPage;
        private CommonPage _commonPage;
        private ProductPage _productPage;
        private CartPage _cartPage;

        private const string sauceLabsBackpack = "Sauce Labs Backpack";
        private const string sauceLabsFleeceJacket = "Sauce Labs Fleece Jacket";
        private const string sauceLabsOnesie = "Sauce Labs Onesie";

        [SetUp]
        public async Task SetUp()
        {
            _loginPage = GetPage<LoginPage>();
            _productPage = GetPage<ProductPage>();
            _commonPage = GetPage<CommonPage>();
            _cartPage = GetPage<CartPage>();

            
            await _loginPage.NavigateAsync();
            await _loginPage.LoginAsync("standard_user", "secret_sauce");
            _commonPage.VerifyPage("inventory");
        }

        [Test]
        public async Task AddProductToCart_ShouldDisplayInCart_AndBeRemovable()
        {
            List<ProductItem> listOfProducts = new();
            await _productPage.AddItemToCart(sauceLabsBackpack);
            ProductItem product = await _productPage.GetProductInfo(sauceLabsBackpack);
            listOfProducts.Add(product);
            Console.WriteLine(listOfProducts.Count);
            await _productPage.VerifyBadgeNumber(listOfProducts.Count);
            await _productPage.NavigateToCart();

            await _cartPage.VerifyItemsInCart(listOfProducts);

            await _cartPage.RemoveItemFromCart(listOfProducts[0].Name);
            _cartPage.IsCartEmpty();
        }

        [Test]
        public async Task AddMultipleProductsToCart_ShouldDisplayInCart()
        {
            List<ProductItem> listOfProducts = new();
            await _productPage.AddItemToCart(sauceLabsBackpack);
            await _productPage.AddItemToCart(sauceLabsFleeceJacket);
            await _productPage.AddItemToCart(sauceLabsOnesie);

            listOfProducts.Add(await _productPage.GetProductInfo(sauceLabsBackpack));
            listOfProducts.Add(await _productPage.GetProductInfo(sauceLabsFleeceJacket));
            listOfProducts.Add(await _productPage.GetProductInfo(sauceLabsOnesie));

            await _productPage.VerifyBadgeNumber(listOfProducts.Count);
            await _productPage.NavigateToCart();

            await _cartPage.VerifyItemsInCart(listOfProducts);

        }
    }
}

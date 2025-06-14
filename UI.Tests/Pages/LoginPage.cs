using Microsoft.Playwright;
using NUnit.Framework;

namespace MyE2ETests.UI.Tests.Pages
{
    class LoginPage
    {
        private readonly IPage _page;

        protected const string WrongPasswordError = "Epic sadface: Username and password do not match any user in this service";
        protected const string LockedOutUser = "Epic sadface: Sorry, this user has been locked out.";
        protected ILocator UsernameField => _page.Locator("#user-name");
        protected ILocator PasswordField => _page.Locator("#password");
        protected ILocator LoginButton => _page.Locator("#login-button");
        protected ILocator ErrorField => _page.Locator("//h3[@data-test='error']");
        
     
        public LoginPage(IPage page)
        {
            _page = page;
        }


        public async Task NavigateAsync() => await _page.GotoAsync("https://www.saucedemo.com/");

        public async Task LoginAsync(string username, string password)
        {
            await UsernameField.FillAsync(username);
            await PasswordField.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        public async Task VerifyWrongPasswordError()
        {
            Assert.That(await ErrorField.InnerTextAsync(), Does.Contain(WrongPasswordError));
        }
        public async Task VerifyLockedOutUserError() => Assert.That(await ErrorField.InnerTextAsync(), Does.Contain(LockedOutUser));
    }
}

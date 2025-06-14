using MyE2ETests.UI.Tests.Pages;
using MyE2ETests.UI.Tests.Utils;
using NUnit.Framework;
using UI.Tests.Pages;

namespace UI.Tests.Tests
{
    public class LoginTests : TestBase
    {
        private LoginPage _loginPage;
        private CommonPage _commonPage;

        [SetUp]
        public void SetUp()
        {
            _loginPage = GetPage<LoginPage>();
            _commonPage = GetPage<CommonPage>();
        }

        [Test]
        public async Task ValidLogin()
        {
            await _loginPage.NavigateAsync();
            await _loginPage.LoginAsync("standard_user", "secret_sauce");
           _commonPage.VerifyPage("inventory");

        }

        [Test]
        public async Task InvalidLogin()
        {
            await _loginPage.NavigateAsync();
            await _loginPage.LoginAsync("standard_user", "wrong_pass");
            await _loginPage.VerifyWrongPasswordError();

        }
        
        [Test]
        public async Task LockedOutUserLogin()
        {
            await _loginPage.NavigateAsync();
            await _loginPage.LoginAsync("locked_out_user", "secret_sauce");
            await _loginPage.VerifyLockedOutUserError();

        }

    }
}

using Microsoft.Playwright;
using NUnit.Framework;

namespace UI.Tests.Pages
{
    class CommonPage
    {
        private readonly IPage _page;

        public CommonPage(IPage page)
        {
            _page = page;
        }

        public void VerifyPage(String nameOfPage) => Assert.That(_page.Url, Does.Contain(nameOfPage.ToLower()));
    }
}

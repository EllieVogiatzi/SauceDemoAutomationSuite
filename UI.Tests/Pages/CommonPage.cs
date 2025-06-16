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

        /// <summary>
        /// Verifies that the current page URL contains the specified page name (case-insensitive).
        /// </summary>
        /// <param name="nameOfPage">The name of the page to verify in the URL.</param>
        public void VerifyPage(String nameOfPage) => Assert.That(_page.Url, Does.Contain(nameOfPage.ToLower()));
    }
}

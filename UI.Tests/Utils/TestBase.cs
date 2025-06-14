using Microsoft.Playwright;
using NUnit.Framework;

namespace MyE2ETests.UI.Tests.Utils
{
    public class TestBase
    {
        protected IBrowser Browser;
        protected IPage Page;
        protected IBrowserContext Context;

        private readonly Dictionary<Type, object> _pages = new();

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            var playwright = await Playwright.CreateAsync();
            Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = ["--guest", "--disable-popup-blocking", "--no-default-browser-check"]
            });

            Context = await Browser.NewContextAsync();
            Page = await Context.NewPageAsync();
        }

        [OneTimeTearDown]
        public async Task GlobalTeardown()
        {
            await Browser.CloseAsync();
        }

        protected T GetPage<T>() where T : class
        {
            if (_pages.ContainsKey(typeof(T))) return (T)_pages[typeof(T)];
            var page = Activator.CreateInstance(typeof(T), Page) as T;
            _pages[typeof(T)] = page;
            return page;
        }

        [TearDown]
        public async Task CleanupPerTest()
        {
            await Page.EvaluateAsync("window.localStorage.clear()");
            await Context.ClearCookiesAsync();
            await Page.GotoAsync("about:blank");
        }
    }
}

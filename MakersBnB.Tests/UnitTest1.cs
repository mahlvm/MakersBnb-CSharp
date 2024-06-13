namespace MakersBnB.Tests;

using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

public class Tests : PageTest
{
    // the following method is a test
    [Test]
    public void IndexpageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        // go to the MakersBnB Index page
        Page.GotoAsync("http://localhost:5106");

        // expect the page title to contain "Index Page - MakersBnB"
        Expect(Page).ToHaveTitleAsync(new Regex("Index Page - MakersBnB"));
    }

    [Test]
    public void HomePageIncludesWelcomeMessage() 
    {
        Page.GotoAsync("http://localhost:5106");

        Expect(Page.GetByText("Welcome to MakersBnB!")).ToBeVisibleAsync();
    }
}
namespace MakersBnB.Tests;

using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

public class SignupTest : PageTest
{
    // the following method is a test
    [Test]
    public void SigningUpWithCorrectCredentials()
    {
        // go to the MakersBnB Index page
        Page.GotoAsync("http://localhost:5106/Users/New");
        Page.GetByLabel("Username").FillAsync("username");
        Page.GetByLabel("Email").FillAsync("email@email.com");
        Page.GetByLabel("Password").FillAsync("secret");
        Page.GetByRole(AriaRole.Button).ClickAsync();

        Expect(Page).ToHaveTitleAsync(new Regex("Spaces - MakersBnB"));
    }
}
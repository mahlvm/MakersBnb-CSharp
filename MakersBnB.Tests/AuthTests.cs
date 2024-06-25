namespace MakersBnB.Tests;

using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

public class AuthTests : PageTest
{
    [Test]
    public void SigningInWithCorrectCredentials()
    {
        // Criando um novo usuário
        Page.GotoAsync("http://localhost:5106/Users/New");
        Page.GetByLabel("Username").FillAsync("username");
        Page.GetByLabel("Email").FillAsync("email@email.com");
        Page.GetByLabel("Password").FillAsync("secret");
        Page.GetByRole(AriaRole.Button).ClickAsync();

        // Fazendo Login
        Page.GotoAsync("http://localhost:5106/Sessions/New");
        Page.GetByLabel("Email").FillAsync("email@email.com");
        Page.GetByLabel("Password").FillAsync("secret");
        Page.GetByRole(AriaRole.Button).ClickAsync();

        // Se sucesso, vai para /Spaces
        Expect(Page).ToHaveTitleAsync(new Regex("Spaces - MakersBnB"));

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using SelenoTest;
using SelenoTest.Controllers;
using SelenoTest.Models;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.Configuration.WebServers;
using TestStack.Seleno.PageObjects;
using By = TestStack.Seleno.PageObjects.Locators.By;

namespace Tests
{
    [TestFixture]
    class MySeleno
    {
        [Test]
        public void UseSeleno()
        {
            var selenoHost = BrowserHost.Instance;
            AccountPage accountPage = selenoHost.NavigateToInitialPage<AccountController, AccountPage>(x=>x.Login(string.Empty));

            LoginViewModel loginViewModel = new LoginViewModel()
            {
                Email = "adrianboboc2003@yahoo.com",
                Password = "Adrianb_13"
            };
            var homePage =
                accountPage.Submit<AccountPage>(loginViewModel);


            Thread.Sleep(TimeSpan.FromSeconds(2));

            selenoHost.Application.Browser.Url.Should().Be(BrowserHost.RootUrl);


        }

        public static class BrowserHost
        {
            public static readonly SelenoHost Instance = new SelenoHost();
            public static readonly string RootUrl;

            static BrowserHost()
            {
                //Instance.Run(configure => configure.WithWebServer(new InternetWebServer("http://localhost/testing/")));
                Instance.Run("SelenoTest", 49275,configuration=>configuration.WithRouteConfig(RouteConfig.RegisterRoutes));
                RootUrl = Instance.Application.Browser.Url;
            }
        }
    }

    internal class HomePage:Page
    {
    }

    internal class AccountPage : Page<LoginViewModel>
    {
        public AccountPage SetEmail(string email)
        {
            IWebElement element = Find.Element(By.Name("Email"));
            element.SendKeys(email);
            return this;
        }

        public AccountPage SetPassword(string password)
        {
            IWebElement element = Find.Element(By.Name("Password"));
            element.SendKeys(password);
            return this;
        }

        public T Submit<T>(LoginViewModel loginViewModel) where T : UiComponent, new()
        {
            Input.Model(loginViewModel);
            return Navigate.To<T>(By.ClassName("btn"));
        }
    }
}

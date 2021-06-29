using System.Threading;
using System;
using System.IO;
using Bogus;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace CadFuncionario.Tests.Automated
{
    public class AdicionarProfissaoTest
    {
        private readonly Faker _faker;
        private readonly ChromeDriver _driver;
        private readonly WebDriverWait _wait;

        public AdicionarProfissaoTest()
        {
            _faker = new Faker("pt_BR");
            var options = new ChromeOptions();
            options.AddArgument("--headless");

            _driver = new ChromeDriver(Directory.GetCurrentDirectory(), options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(0.5));
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://localhost");
        }

        [Fact(DisplayName = "Adicionar profissao com sucesso")]
        [Trait("Grupo", "Automated")]
        public void ProfissaoAutomated_Sucesso()
        {
            // Arrange & Act & Assert
            Assert.Equal("Profissão cadastrada com sucesso!", CadastrarProfissaoSucesso());

            Thread.Sleep(500);

            Assert.Equal("", _wait.Until(c => c.FindElement(By.Id("txtDescricao")))
                .GetAttribute("value"));
            Assert.Equal("", _wait.Until(c => c.FindElement(By.Id("txtSalarioBase")))
                .GetAttribute("value"));
        }

        [Fact(DisplayName = "Adicionar profissao com falha de validação")]
        [Trait("Grupo", "Automated")]
        public void ProfissaoAutomated_Falha()
        {
            // Arrange & Act & Assert
            Assert.Equal("Informe todos os campos do formulário", CadastrarProfissaoFalha());
            Assert.NotEqual("", _wait.Until(c => c.FindElement(By.Id("txtSalarioBase")))
                .GetAttribute("value"));
        }

        private string CadastrarProfissaoSucesso()
        {
            _wait.Until(c => c.FindElement(By.Id("txtDescricao")))
                .SendKeys(_faker.Company.CompanyName());

            _wait.Until(c => c.FindElement(By.Id("txtSalarioBase")))
                .SendKeys(decimal.Round(_faker.Random.Decimal(1000, 5000), 2)
                    .ToString().Replace(",", "."));

            _wait.Until(c => c.FindElement(By.Id("btnSalvar"))).Click();

            Thread.Sleep(2500);

            return RecuperarTextoAlert();
        }

        private string CadastrarProfissaoFalha()
        {
            _wait.Until(c => c.FindElement(By.Id("txtSalarioBase")))
                .SendKeys(decimal.Round(_faker.Random.Decimal(1000, 5000), 2)
                    .ToString().Replace(",", "."));

            _wait.Until(c => c.FindElement(By.Id("btnSalvar"))).Click();

            Thread.Sleep(200);
            return RecuperarTextoAlert();
        }

        private string RecuperarTextoAlert()
        {
            var alert = _driver.SwitchTo().Alert();
            var textAlert = alert.Text;

            alert.Accept();

            return textAlert;
        }
    }
}
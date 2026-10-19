using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace RaceDayApi.Tests
{
    public class ApiTestBase
    {
        protected CustomWebApplicationFactory Factory { get; private set; } = null!;
        protected HttpClient Client { get; private set; } = null!;

        // NUnit uses this method INSTEAD of a constructor
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Factory = new CustomWebApplicationFactory();
        }

        [SetUp]
        public void SetUp()
        {
            // Force the in-memory HttpClient to look like a secure HTTPS endpoint
            var options = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                BaseAddress = new System.Uri("https://localhost")
            };
            Client = Factory.CreateClient(options);
        }



        [TearDown]
        public void TearDown()
        {
            Client?.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Factory?.Dispose();
        }

        protected StringContent CreateJsonContent(object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected void AuthenticateClient(string token = "mock-jwt-token")
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            base.ConfigureWebHost(builder);
        }
    }
}

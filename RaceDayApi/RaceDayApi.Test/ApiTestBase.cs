using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace RaceDayApi.Tests // <--- Double check this namespace matches the using statements in your test files
{
    public class ApiTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient Client;

        public ApiTestBase(WebApplicationFactory<Program> factory)
        {
            Client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("contentRoot", AppContext.BaseDirectory);
            }).CreateClient();
        
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

        public class CustomWebApplicationFactory : WebApplicationFactory<Program>
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.UseContentRoot(Directory.GetCurrentDirectory());
                base.ConfigureWebHost(builder);
            }
        }
    }
}

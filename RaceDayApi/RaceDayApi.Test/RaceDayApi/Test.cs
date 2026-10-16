using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace RaceDayApi.Test
{
    public class Test
    {
        private WebApplicationFactory<Program> factory;

        public Test(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

    }
}
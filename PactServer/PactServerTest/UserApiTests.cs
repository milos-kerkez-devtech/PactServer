using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactServer;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace PactServerTest
{
    public class UserApiTests : IDisposable
    {
        private string ProviderUri { get; }
        private string PactServiceUri { get; }
        private IWebHost WebHost { get; }
        private ITestOutputHelper OutputHelper { get; }
        private readonly UserService _userService;
       

        public UserApiTests(ITestOutputHelper output)
        {
            _userService = new UserService();
            OutputHelper = output;
            ProviderUri = "http://localhost:63430";
            PactServiceUri = "http://localhost:9000";

            WebHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder()
                .UseUrls(PactServiceUri)
                .UseStartup<TestStartup>()
                .Build();

            WebHost.Start();
        }

        [Fact]
        public void EnsureProviderApiHonoursPactWithConsumer()
        {
            // Arrange
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                                {
                                    new XUnitOutput(OutputHelper)
                                },
                                CustomHeader= new KeyValuePair<string, string> ( "Content-Type", "application/json; charset=utf-8"),
                Verbose = true
            };

            //Act / Assert
            IPactVerifier pactVerifier = new PactVerifier(config);

            pactVerifier.ProviderState($"{PactServiceUri}/provider-states")
                .ServiceProvider("Provider", ProviderUri)
                .HonoursPactWith("Consumer")
                .PactUri(@"C:\DevTech\Repos\pacts\consumer-user_api.json")
                .Verify();

            //_userService.DeleteUser("Test");
        }


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    WebHost.StopAsync().GetAwaiter().GetResult();
                    WebHost.Dispose();
                }

                disposedValue = true;
            }
        }

       
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}


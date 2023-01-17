using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace PruebasMcsGithub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet(nameof(otroPrueba))]
        public async Task<IActionResult> otroPrueba()
        {
            const string secretName = "JwtMas";
            var keyVaultName = "MasAssessments";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var secretValue = "";

            //Console.Write($"Creating a secret in {keyVaultName} called '{secretName}' with the value '{secretValue}' ...");
            //await client.SetSecretAsync(secretName, secretValue);
            //Console.WriteLine(" done.");

            //Console.WriteLine("Forgetting your secret.");
            //secretValue = string.Empty;
            //Console.WriteLine($"Your secret is '{secretValue}'.");

            Console.WriteLine($"Retrieving your secret from {keyVaultName}.");
            var secret = await client.GetSecretAsync(secretName);
            Console.WriteLine($"Your secret is '{secret.Value.Value}'.");

            //Console.Write($"Deleting your secret from {keyVaultName} ...");
            //DeleteSecretOperation operation = await client.StartDeleteSecretAsync(secretName);
            //// You only need to wait for completion if you want to purge or recover the secret.
            //await operation.WaitForCompletionAsync();
            //Console.WriteLine(" done.");

            //Console.Write($"Purging your secret from {keyVaultName} ...");
            //await client.PurgeDeletedSecretAsync(secretName);
            //Console.WriteLine(" done.");

            return Ok(secret);
        }
    }
}
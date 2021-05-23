using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;

namespace Northwind.Infrastructure.Services
{
    public class KeyvaultSecretsProvider
    {
        public KeyvaultSecretsProvider(IConfiguration configuration)
        {
            KeyVaultUrl = configuration.GetValue<string>(nameof(KeyVaultUrl));
        }

        public async Task<X509Certificate2> GetCertificate(string name)
        {
            var client = GetClient();

            var certSecret = await client.GetSecretAsync(KeyVaultUrl, name);
            var certString = System.Convert.FromBase64String(certSecret.Value);

            var x509cert = new X509Certificate2(certString);

            return x509cert;
        }

        private string KeyVaultUrl { get; set; }

        private KeyVaultClient _client;

        private KeyVaultClient GetClient()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            return _client ??= new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
        }
    }
}

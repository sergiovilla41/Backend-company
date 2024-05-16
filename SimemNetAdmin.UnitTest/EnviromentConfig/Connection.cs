using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using SimemNetAdmin.Transversal.KeyVault;
using System.Text;

namespace EnviromentConfig
{
    public static class Connection
    {

        public static void SetEnvironmentVar()
        {
            Environment.SetEnvironmentVariable("AzureWebJobsStorage", "");
            Environment.SetEnvironmentVariable("FUNCTIONS_WORKER_RUNTIME", "dotnet");
            Environment.SetEnvironmentVariable("AzureStorage", "AzureStorage");
            Environment.SetEnvironmentVariable("SimemConnection", "SimemConnection");
            Environment.SetEnvironmentVariable("UrlApim", "UrlApim");
            Environment.SetEnvironmentVariable("OcpApimSubscriptionKey", "OcpApimSubscriptionKey");
            Environment.SetEnvironmentVariable("AzureKeyVaultUri", "https://kvsimemprb02.vault.azure.net/");
            Environment.SetEnvironmentVariable("clientId", "ZQA5ADQANAA2AGIANwAyAC0AOAA3AGMAYgAtADQANQAyADEALQA4ADkAYgBmAC0AZgAwAGYANQBhADAAYwA2AGEAZQAzADUA");
            Environment.SetEnvironmentVariable("clientSecret", "VgBHAGgAOABRAH4AVwBQAH4AQgBSAFQAUABqADgARQBNAHIAWABuAG0AZABIAHoAZQBiAEkAVgBCAEUAagBPAEMAVgBqAFgASQBjAFAAUgA=");
            Environment.SetEnvironmentVariable("tenantId", "YwA5ADgAMABlADQAMQAwAC0AMABiADUAYwAtADQAOABiAGMALQBiAGQAMQBhAC0AOABiADkAMQBjAGEAYgBjADgANABiAGMA");
            Environment.SetEnvironmentVariable("Pipeline", "false");
            Environment.SetEnvironmentVariable("StorageContainer", "StorageContainerSimem");
        }

        public static void ConfigureConnections()
        {
            SetEnvironmentVar();
            KeyVaultTypes[] enumValues = (KeyVaultTypes[])Enum.GetValues(typeof(KeyVaultTypes));
            byte[] decryted;

            decryted = Convert.FromBase64String(Environment.GetEnvironmentVariable("clientId")!);
            string clientId = Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(Environment.GetEnvironmentVariable("clientSecret")!);
            string clientSecret = Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(Environment.GetEnvironmentVariable("tenantId")!);
            string tenantId = Encoding.Unicode.GetString(decryted);

            var vaultUri = new Uri(Environment.GetEnvironmentVariable("AzureKeyVaultUri")!);
            ClientSecretCredential credential = new(tenantId, clientId, clientSecret);
            var client = new SecretClient(vaultUri, credential);

            foreach (var keyName in enumValues)
            {
                string secret = KeyVaultManager.GetSettingValue(keyName);

                if (secret != null)
                {

                    if (!KeyVaultManager.IsPipelineVariableActive())
                    {
                        secret = client.GetSecret(secret).Value.Value;
                    }

                    KeyVaultManager.SetSecretValue(keyName.ToString(), secret);
                }
            }
        }
    }
}

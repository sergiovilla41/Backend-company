using Azure.Analytics.Synapse.Artifacts;
using Azure.Identity;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Transversal.Helper
{
    [ExcludeFromCodeCoverage]
    public static class SynapseHelper
    {
        public static string ExecutePipeline(string nbSynapseName)
        {
            ClientSecretCredential credential = GetCredentials();
            string synapseWorkspaceName = Environment.GetEnvironmentVariable("synapseWorkspaceName")!;
            var pipelineClient = new PipelineClient(
            new Uri($"https://{synapseWorkspaceName}.dev.azuresynapse.net"),
            credential);

            var parameters = new Dictionary<string, object>
            {
                { "NBSynapse", nbSynapseName},
            };

            string pipelineName = Environment.GetEnvironmentVariable("pipelineToExecuteName")!;
            Azure.Response<Azure.Analytics.Synapse.Artifacts.Models.CreateRunResponse> run = pipelineClient.CreatePipelineRun(pipelineName, parameters: parameters);
            string pipelineRunId = run.Value.RunId;
            return pipelineRunId!;
        }

        public static string CancelPipelineRunning(string pipelineRunId)
        {
            try
            {
                ClientSecretCredential credential = GetCredentials();
                string synapseWorkspaceName = Environment.GetEnvironmentVariable("synapseWorkspaceName")!;
                var pipelineRunClient = new PipelineRunClient(
                new Uri($"https://{synapseWorkspaceName}.dev.azuresynapse.net"),
                credential);

                pipelineRunClient.CancelPipelineRun(pipelineRunId, isRecursive: true);
                return "Ejecución cancelada";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
            
        }

        private static ClientSecretCredential GetCredentials()
        {
            byte[] decryted;

            decryted = Convert.FromBase64String(Environment.GetEnvironmentVariable("clientId")!);
            string clientId = System.Text.Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(Environment.GetEnvironmentVariable("clientSecret")!);
            string clientSecret = System.Text.Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(Environment.GetEnvironmentVariable("tenantId")!);
            string tenantId = System.Text.Encoding.Unicode.GetString(decryted);

            return new(tenantId, clientId, clientSecret);
        }
    }
}

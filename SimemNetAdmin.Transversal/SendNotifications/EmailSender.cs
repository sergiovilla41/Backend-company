using Newtonsoft.Json;
using RestSharp;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Transversal.Interfaces;
using SimemNetAdmin.Transversal.KeyVault;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Transversal.SendNotifications
{
    [ExcludeFromCodeCoverage]
    public  class EmailSender : IEmailSender
    {
        public  string SendNotificacions(string bodyData) {
            try {
                
                string? url = KeyVaultManager.GetSecretValue(KeyVaultTypes.UrlApim); 
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Ocp-Apim-Subscription-Key", KeyVaultManager.GetSecretValue(KeyVaultTypes.OcpApimSubscriptionKey));
                request.AddStringBody(bodyData, DataFormat.Json);
                RestResponse respuesta = client.Execute(request);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "";
                }
                return "";
            }catch(Exception) {
                return "";
            }
        }
    }
}

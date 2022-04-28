using Business.JSONModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using PresentationLayer.Helpers;
using Blazored.LocalStorage;

namespace PresentationLayer.Services
{
    
    public static class HttpService
    {   public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T value)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(value),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri),

            };
            return await httpClient.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> PatchAsJsonAsync<T>(HttpClient client, string requestUri, T value)
        {
            Ensure.ArgumentNotNull(client, "client");
            Ensure.ArgumentNotNull(requestUri, "requestUri");
            Ensure.ArgumentNotNull(value, "value");

            var content = new ObjectContent<T>(value, new JsonMediaTypeFormatter());
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = content };
            return await client.SendAsync(request);
        }
    }
}
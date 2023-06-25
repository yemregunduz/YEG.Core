using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YEG.RestSharp.Interfaces;
using YEG.RestSharp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YEG.RestSharp.Repository
{
    public class RestSharpRepository<T> : IRestSharpRepository<T>
        where T:class
    {
        private readonly RestClient _restClient;

        public RestSharpRepository(string baseUrl)
        {
            _restClient = new RestClient(baseUrl);
        }
        public async Task<T> GetAsync(RestRequestParameter requestParameter)
        {
            var request = new RestRequest(CreateResourceUrl(requestParameter), Method.Get);
            return await GetResponse(request, requestParameter);
        }

        public async Task<T> PostAsync(RestRequestParameter requestParameter, T data)
        {
            var request = new RestRequest(CreateResourceUrl(requestParameter), Method.Post);
            return await GetResponse(request, requestParameter);
        }

        public async Task<T> DeleteAsync(RestRequestParameter requestParameter)
        {
            var request = new RestRequest(CreateResourceUrl(requestParameter), Method.Delete);
            return await GetResponse(request, requestParameter);
        }

        public async Task<T> PutAsync(RestRequestParameter requestParameter, T data)
        {
            var request = new RestRequest(CreateResourceUrl(requestParameter), Method.Put);
            return await GetResponse(request, requestParameter);
        }

        public async Task<T> PatchAsync(RestRequestParameter requestParameter, T data)
        {
            var request = new RestRequest(CreateResourceUrl(requestParameter), Method.Patch);
            return await GetResponse(request, requestParameter);
        }


        private async Task<T> GetResponse(RestRequest request, RestRequestParameter requestParameter, T? data = null)
        {

            if(requestParameter is not null && requestParameter.Headers is not null)
            {
                foreach(var header in requestParameter.Headers)
                    request.AddHeader(header.Key, header.Value);    
            }

            if(data is not null)
            {
                request.AddJsonBody(data);
            }

            var response = await _restClient.ExecuteAsync<T>(request);

            return JsonSerializer.Deserialize<T>(response.Content, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        }

        private string CreateResourceUrl(RestRequestParameter requestParameter)
        {
            string resourceUrl = $"{requestParameter.Controller}";

            if (requestParameter.Action is not null)
                resourceUrl += $"/{requestParameter.Action}";

            if (requestParameter.QueryString is not null)
                resourceUrl += $"/{requestParameter.QueryString}";

            if (requestParameter.PathVariable is not null)
                resourceUrl += $"/{requestParameter.PathVariable}";

            return resourceUrl;
        }


    }
}

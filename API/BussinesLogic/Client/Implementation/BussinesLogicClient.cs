using _Core.Models.DTO;
using _Core.Models.Requests;
using _Core.Models.Responses;
using APILayer.BussinesLogic.Client.Interface;
using Core.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace APILayer.BussinesLogic.Client.Implementation
{
    public class BussinesLogicClient : IBussinesLogicClient
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _options;


        public BussinesLogicClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; 
        }
        public async Task<ActionResult<string>> GetSale(SaleRequest request, CancellationToken token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{request.ClientBaseAddres}/api/sale/add/");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonSerializer.Serialize<BuyDTO>(request.Buy);

                await streamWriter.WriteAsync(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = await streamReader.ReadToEndAsync();
            }

            return String.Empty;
        }


        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string BaseUrl, CancellationToken token)
        {
            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"{BaseUrl}api/service/getall"
                );

            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<IEnumerable<Product>> (responseStream, _options);
                return new List<Product>(result);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return null;
        }
    }
}

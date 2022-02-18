using _Core.Models.Requests;
using BussunesLogic.DataLogic.Client.Interface;
using Core.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BussunesLogic.DataLogic.Client.Implementation
{
    public class DataBaseLogic : IDataBaseLogic
    {
        private readonly HttpClient _client;
        private JsonSerializerOptions _options;


        public DataBaseLogic(HttpClient client)
        { 
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }


        public async Task AddSaleEntity(SaleAddEntity request, CancellationToken token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{request.ClientBaseAddres}/api/product/buy/");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonSerializer.Serialize<Sale>(request.SaleEntity);

                    await streamWriter.WriteAsync(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }            
        }


        public async Task<ActionResult<Product>> GetProduct(ProductRequest request, CancellationToken token)
        {
            var httpRequest = new HttpRequestMessage
                (HttpMethod.Get,
                $"{request.ClientBaseAddres}/api/product/{request.ProductId}"
                );
            try
            {
                HttpResponseMessage response = _client.SendAsync(httpRequest).Result;
                using(var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    Product result = await JsonSerializer
                        .DeserializeAsync<Product>(responseStream, _options);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new Product();
        }


        public async Task<ActionResult<int>> GetQuantity(ProvidedProductRequest request, CancellationToken token)
        {
            var httpRequest = new HttpRequestMessage
            (
                HttpMethod.Get,
                $"{request.ClientBaseAddres}/api/salespoint/getquantityproduct/{request.ProductId}"
            );
            try
            {
                HttpResponseMessage response = _client.SendAsync(httpRequest).Result;
                using(var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    int result = await JsonSerializer
                        .DeserializeAsync<int>(responseStream, _options);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return -1;
        }


        public async Task UpdateQuantity(UpdateQuantityRequest request, CancellationToken token)
        {
            var httpRequest = new HttpRequestMessage
                (
                    HttpMethod.Put,
                    $"{request.ClientBaseAddres}/api/providedproduct/" +
                    $"{request.ProductId}/{request.QuantityAfterSale}"
                );
            try
            {
                HttpResponseMessage response = await _client.SendAsync(httpRequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(string baseUrl, CancellationToken token)
        {
            var httpRequest = new HttpRequestMessage
                (
                    HttpMethod.Get,
                    $"{baseUrl}/api/product/getall"
                );
            try
            {
                HttpResponseMessage response = await _client.SendAsync(httpRequest);
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var result = await JsonSerializer
                        .DeserializeAsync<List<Product>>(responseStream, _options);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new List<Product>();        
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WordGuessGame.Models.Dto;

namespace WordGuessGame
{
    internal class APIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public APIClient()
        {
        }


        public async Task<ResponseDto> SendRequest(RequestDto request)
        {
            ResponseDto response = new();
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");
                message.Headers.Add("application/json", "application/json");
                message.Headers.Add("x-api-key", constants._apiKey);


                message.RequestUri = new Uri(constants._apiBaseUrl + request.RequestUrl);

                if (request.ApiType == ApiType.Post)
                    message.Method = HttpMethod.Post;
                if (request.ApiType == ApiType.Get)
                    message.Method = HttpMethod.Get;



                if (request.Data != null)
                {
                    message.Content = (
                        new StringContent(JsonConvert.SerializeObject(request.Data)
                        , Encoding.UTF8, "application/json"));
                }



                HttpResponseMessage apiresponse = await client.SendAsync(message);

                switch (apiresponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { Issuccess = false, Message = "NotFound" };
                        break;

                    case HttpStatusCode.BadRequest:
                        return new() { Issuccess = false, Message = "NotFound" };
                        break;
                    default:

                        var content = await apiresponse.Content.ReadAsStringAsync();
                        return new() { Issuccess = true, Response = content };

                }

            }
            catch (HttpRequestException ex)
            {
                response = new ResponseDto() { Message = ex.Message, Issuccess = false };
                Console.Write(ex.Message);

            }
            catch (Exception ex)
            {
                response = new ResponseDto() { Message = ex.Message, Issuccess = false };
                Console.Write(ex.Message);
            }

            return response;
        }
    }
}

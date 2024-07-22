using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WordGuessGame.Models;
using WordGuessGame.Models.Dto;

namespace WordGuessGame.Services
{
    internal class WordService : IWordService
    {


        private readonly APIClient _client;
        public WordService()
        {

            _client = new APIClient();

        }
        public async Task<List<string>> GetDictionary()
        {

            List<string> dictionary = new();
            var response = await _client.SendRequest(new RequestDto()
            {
                RequestUrl = "api/v1/guess",
                Data = null,
                ApiType = ApiType.Get

            });

            if (response != null && response.Issuccess)
            {
                dictionary = response.Response.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                //dictionary = JsonConvert.DeserializeObject<List<string>>(response.Response);
            }
            else
                Console.WriteLine(response.Message);

            return dictionary;



        }

        public async Task<GuessModel> GuessWord(string letter, string token)
        {

            GuessModel model = new GuessModel();

            var response = await _client.SendRequest(new RequestDto()
            {
                RequestUrl = "api/v1/guess",
                Data = new { letter = letter, token = token },
                ApiType = ApiType.Post

            });

            if (response != null && response.Issuccess)
            {


                model = JsonConvert.DeserializeObject<GuessModel>(response.Response);
            }
            else
                Console.WriteLine(response.Message);

            return model;
        }

        public async Task<WordModel> StartGame()
        {
            WordModel model = new WordModel();

            var response = await _client.SendRequest(new RequestDto()
            {
                RequestUrl = "api/v1/guess",
                Data = null,
                ApiType = ApiType.Get

            });

            if (response != null && response.Issuccess)
            {
                model = JsonConvert.DeserializeObject<WordModel>(response.Response);
            }
            else
                Console.WriteLine(response.Message);

            return model;
        }
    }

}

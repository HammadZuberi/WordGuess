using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGuessGame.Models;

namespace WordGuessGame.Services
{
    internal interface IWordService
    {


        public Task<WordModel> StartGame();
        public Task<List<string>> GetDictionary();
        public Task<GuessModel> GuessWord(string letter, string token);

    }
}

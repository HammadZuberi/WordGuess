// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using WordGuessGame;
using WordGuessGame.Models;
using WordGuessGame.Services;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("Hello, World!");


Console.WriteLine("Start Game");

IWordService service = new WordService();
List<char> guessAlphabets = new();
List<string> Dictionary = new List<string>();

try
{


    Dictionary = await service.GetDictionary();
    List<string> searchDictionary = new List<string>();
    WordModel gameData = await service.StartGame();

    string token = gameData.token;
    int wordlenght = gameData.Word.Length;
    string Word = new string('_', wordlenght);

    int incorrectattempt = 26;
    int numberofattempts = 0;

    //filter words by lenght of same word
    searchDictionary = Dictionary.FindAll(a => a.Length == Word.Length).ToList();

    while (Word.Contains("_"))
    {
        //char nextword = Aproach1();
        char nextword = GuessNextAlphabet(Word, ref searchDictionary);
        if (incorrectattempt <= 0)
            Console.WriteLine("You loose you have 26 incorrect attempts");
        break;

        guessAlphabets.Add(nextword);

        var result = await service.GuessWord(nextword.ToString(), token);
        numberofattempts++;
        if (result.Result == constants.Correct)
        {

            Word = result.Word;
            token = result.Token;
        }
        else
        {
            incorrectattempt--;
        }
    }

    Console.WriteLine("\n///////////Complted///////////////\n");
    Console.WriteLine($"Solved Word is {Word} with number of attempts {numberofattempts}");

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}




char GuessNextAlphabet(string Guessword, ref List<string> searchDict)
{
    //dictionary for finding highest occred letter of resemebled word
    var letterCount = new Dictionary<char, int>();

    //search dict is filtered at first at at every step of guess word minimizing the search in each run, and identifing the right and best 
    foreach (string word in searchDict)
    {
        bool isResemblence = true;

        //fileter if the word contains the same position as the guess word.
        for (int j = 0; j < word.Length; j++)
        {
            //if the word is not '-' and the word placement is not correct.
            if (Guessword[j] != '-' && Guessword[j] != word[j])
            {
                isResemblence = false;
                //remove minimize  from search iteration with each word after every word cound at function call
                searchDict.Remove(word);
                break;
            }
        }
        //discards non matching words in the sequence
        if (!isResemblence) continue;

        for (int i = 0; i < word.Length; i++)
        {
            if (!guessAlphabets.Contains(word[i]))
            {
                //add a new charachter  or increase count
                if (!letterCount.ContainsKey(word[i]))
                    letterCount[word[i]] = 1;
                else
                    letterCount[word[i]]++;

            }

        }
    }

    var randomChar = letterCount.OrderByDescending(x => x.Value).FirstOrDefault().Key;

    return randomChar;
}

//aproach1 simplist approach
char Aproach1()
{

    Random random = new Random();
    char randomChar = (char)random.Next('a', 'z' + 1);

    //a-z 26 char
    for (int i = 0; i < 25; i++)
    {
        //list random by chance if the alphabet repeats generate again
        if (guessAlphabets.Contains(randomChar))
        {
            randomChar = (char)random.Next('a', 'z' + 1); break;
        }
        else
            break;
    }


    return randomChar;
}
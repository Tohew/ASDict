using ASDict.MVVM.Models;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASDict.MVVM.ViewModels
{
    internal partial class DictionaryViewModel :ObservableObject
    {
        [ObservableProperty]
        public string word;
        [ObservableProperty]
        public ObservableCollection<string> synonyms = new ObservableCollection<string>();
        [ObservableProperty]
        public ObservableCollection<string> antonyms = new ObservableCollection<string>();
        [ObservableProperty]
        public bool isProcessing;

        //Call API
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task FetchApi(string query)
        {
            string api_url = $"https://api.api-ninjas.com/v1/thesaurus?word={query}";

            try
            {
                var response = await _httpClient.GetStringAsync(api_url);
                DictionaryModel dictModel = JsonSerializer.Deserialize<DictionaryModel>(response);

                Word = dictModel.word;
                Console.WriteLine(Word);

                for(int i = 0; i < dictModel.synonyms.Count;i++)
                {
                    Console.WriteLine(dictModel.synonyms[i]);
                    Synonyms.Add(dictModel.synonyms[i]);
                }

                for (int i = 0; i < dictModel.antonyms.Count; i++)
                {
                    Console.WriteLine(dictModel.antonyms[i]);
                    Antonyms.Add(dictModel.antonyms[i]);
                }
                IsProcessing = false;
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine($"Request failed. Error: {ex.Message}");
            }
        }

        public DictionaryViewModel(string input)
        {
            _httpClient = new HttpClient();
            string apiKey = "PBZJ12Y7HpKiyW70qMjqml51y4JM6kbq8QjBt0GQ";
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            IsProcessing = true;
            _ = FetchApi(input);
        }
        public DictionaryViewModel()
        {
        }
    }
}

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
        public ObservableCollection<string> synonymsCol1 = new ObservableCollection<string>();
        [ObservableProperty]
        public ObservableCollection<string> synonymsCol2 = new ObservableCollection<string>();
        [ObservableProperty]
        public ObservableCollection<string> antonymsCol1 = new ObservableCollection<string>();
        [ObservableProperty]
        public ObservableCollection<string> antonymsCol2 = new ObservableCollection<string>();
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

                //Kiểm tra điều kiện hiển thị của Synonyms
                if (dictModel.synonyms.Count >= 8)
                {
                    // Nếu có 8 phần tử hoặc nhiều hơn, thêm 4 phần tử vào SynonymsCol1
                    for (int i = 0; i < 4; i++)
                    {
                        SynonymsCol1.Add(dictModel.synonyms[i]);
                    }
                    // Thêm phần tử còn lại vào SynonymsCol2
                    for (int i = 4; i < 8; i++)
                    {
                        SynonymsCol2.Add(dictModel.synonyms[i]);
                    }
                }
                else
                {
                    int totalWords = dictModel.synonyms.Count;
                    int wordsSynCol1 = Math.Min(4, totalWords);
                    int wordsSynCol2 = Math.Max(0, totalWords - 4);

                    for (int i = 0; i < wordsSynCol1; i++)
                    {
                        SynonymsCol1.Add(dictModel.synonyms[i]);
                    }

                    for (int i = 0; i < wordsSynCol2; i++)
                    {
                        SynonymsCol2.Add(dictModel.synonyms[i + wordsSynCol1]);
                    }
                }


                for (int i = 0; i < dictModel.antonyms.Count; i++)
                {
                    Console.WriteLine(dictModel.antonyms[i]);
                    AntonymsCol1.Add(dictModel.antonyms[i]);
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

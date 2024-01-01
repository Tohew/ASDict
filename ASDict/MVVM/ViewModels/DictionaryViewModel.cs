using ASDict.MVVM.Models;
using ASDict.MVVM.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace ASDict.MVVM.ViewModels
{
    internal partial class DictionaryViewModel : ObservableObject
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
        public ObservableCollection<string> tempSynonymsCol1 = new ObservableCollection<string>();
        [ObservableProperty]
        public ObservableCollection<string> tempSynonymsCol2 = new ObservableCollection<string>();
        [ObservableProperty]
        public bool isProcessing;


        [ObservableProperty]
        public Bookmark selectedFavorWord;
        [ObservableProperty]
        public Bookmark favoriteWord;
        [ObservableProperty]
        public bool isFavorite;
        [ObservableProperty]
        public string sourceFavorite;
        [ObservableProperty]
        public string sourceWinFavorite;
        public ICommand starClicked { get; }
        public ICommand starWinClicked { get; }
        private readonly BookmarkService _bookmarkService;


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
                if (dictModel.synonyms.Count == 0 && dictModel.antonyms.Count == 0)
                {
                    App.Current.MainPage.DisplayAlert("ASDict", "No result", "OK");
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }

                //Kiểm tra điều kiện hiển thị của Synonyms
                if (dictModel.synonyms.Count > 0)
                {
                    for (int i = 0; i < dictModel.synonyms.Count / 2; i++)
                    {
                        SynonymsCol1.Add(dictModel.synonyms[i]);
                        TempSynonymsCol1.Add(dictModel.synonyms[i]);
                    }
                    
                    for (int i = dictModel.synonyms.Count / 2; i < dictModel.synonyms.Count; i++)
                    {
                        SynonymsCol2.Add(dictModel.synonyms[i]);
                        TempSynonymsCol2.Add(dictModel.synonyms[i]);
                    }
                }

                if (dictModel.antonyms.Count > 0)
                {
                    for (int i = 0; i < dictModel.antonyms.Count / 2; i++)
                    {
                        AntonymsCol1.Add(dictModel.antonyms[i]);
                    }
                    
                    for (int i = dictModel.antonyms.Count / 2; i < dictModel.antonyms.Count; i++)
                    {
                        AntonymsCol2.Add(dictModel.antonyms[i]);
                    }
                }

                IsProcessing = false;
            }
            catch (HttpRequestException ex)
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

            Word = input;
            _bookmarkService = new BookmarkService();

            _ = CheckIfFavoriteAsync();

            starClicked = new Command(favoriteCommand);
            starWinClicked = new Command(favoriteWinCommand);


        }

        public void ConvertToAnt()
        {
            // Xóa dữ liệu của Synonyms
            TempSynonymsCol1.Clear();
            TempSynonymsCol2.Clear();

            // Gán dữ liệu của Antonyms cho Synonyms
            foreach (var antonym in AntonymsCol1)
            {
                TempSynonymsCol1.Add(antonym);
            }

            foreach (var antonym in AntonymsCol2)
            {
                TempSynonymsCol2.Add(antonym);
            }
        }

        public void ConvertToSyn()
        {
            // Xóa dữ liệu của Antonyms
            TempSynonymsCol1.Clear();
            TempSynonymsCol2.Clear();

            // Gán dữ liệu của Synonyms từ bước lưu trữ tạm thời cho Antonyms
            foreach (var synonym in SynonymsCol1)
            {
                TempSynonymsCol1.Add(synonym);
            }

            foreach (var synonym in SynonymsCol2)
            {
                TempSynonymsCol2.Add(synonym);
            }
        }

        public DictionaryViewModel()
        {
        }

        private async Task CheckIfFavoriteAsync()
        {
            FavoriteWord = await _bookmarkService.GetByWord(Word);
            IsFavorite = FavoriteWord != null;
            SourceFavorite = IsFavorite ? "star_icon_blue.svg" : "star_icon_white.svg";
            SourceWinFavorite = IsFavorite ? "star_icon_blue.png" : "star_icon_white.png";
        }

        public async void favoriteCommand()
        {
            if (IsFavorite)
            {
                SourceFavorite = "star_icon_white.svg";
                IsFavorite = false;
                App.Current.MainPage.DisplayAlert("ASDict", "Successfully remove from bookmark", "OK");
                await _bookmarkService.DeleteByWordAsync(Word);
            }
            else
            {
                SourceFavorite = "star_icon_blue.svg";
                IsFavorite = true;
                App.Current.MainPage.DisplayAlert("ASDict", "Successfully add to bookmark", "OK");
                await _bookmarkService.Create(new Bookmark
                {
                    bookmarkWord = Word
                });
            }
        }

        public async void favoriteWinCommand()
        {
            if (IsFavorite)
            {
                SourceWinFavorite = "star_icon_white.png";
                IsFavorite = false;
                App.Current.MainPage.DisplayAlert("ASDict", "Successfully remove from bookmark", "OK");
                await _bookmarkService.DeleteByWordAsync(Word);
            }
            else
            {
                SourceWinFavorite = "star_icon_blue.png";
                IsFavorite = true;
                App.Current.MainPage.DisplayAlert("ASDict", "Successfully add to bookmark", "OK");
                await _bookmarkService.Create(new Bookmark
                {
                    bookmarkWord = Word
                });
            }
        }
        [RelayCommand]
        void HomeClick()
        {
            var homeView = new HomeScreenView();
            homeView.BindingContext = new HomeScreenViewModel();
            App.Current.MainPage.Navigation.PushModalAsync(homeView);
        }

        [RelayCommand]
        void Bookmark1Click()
        {
            var bookmarkView = new BookmarkScreenView();
            bookmarkView.BindingContext = new BookmarkScreenViewModel();
            App.Current.MainPage.Navigation.PushModalAsync(bookmarkView);
        }
        [RelayCommand]
        void SearchClick()
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }

        [RelayCommand]
        void InfoClick()
        {
            App.Current.MainPage.DisplayAlert("About us", "Welcome to our ASDict app! At GHQ, we're dedicated to crafting this tool to help you expand your vocabulary and use language more precisely. We offer an intuitive search for synonyms and antonyms, allowing you to gain a deeper understanding of word meanings and their contextual usage. With cross-platform support, our app is ready to accompany you across various devices.\n\nIf you have any questions, suggestions, or feedback about ASDict, please feel free to contact us via email:\n\n22520357@gm.uit.edu.com\n22521205@gm.uit.edu.com\n22520577@gm.uit.edu.com", "OK");
        }
        [RelayCommand]
        void HelpClick()
        {
            string url = "https://nghgi.github.io/ASDict-Download/";
            _ = Launcher.OpenAsync(new Uri(url));
        }
    }
}
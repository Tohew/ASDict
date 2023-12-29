using ASDict.MVVM.Models;
using ASDict.MVVM.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace ASDict.MVVM.ViewModels
{

    public partial class HomeScreenViewModel : ObservableObject
    {
        [ObservableProperty]
        private HistoryWord inputWord;

        [ObservableProperty]
        private HistoryWord selectedWord;

        [ObservableProperty]
        private ObservableCollection<HistoryWord> recentWords;

        private readonly HistoryWordService _historyWordService;

        private int _editWordId;

        public ICommand searchCommand { get; }
        public HomeScreenViewModel()
        {

            _historyWordService = new HistoryWordService();
            InputWord = new HistoryWord();
            RecentWords = new ObservableCollection<HistoryWord>();
            Task.Run(async () => await LoadRecentWords());
            searchCommand = new Command(async () => await search_Clicked());

            LoadRandomWords();
            RandomWord = GetRandomWord(RandomWords);


            _httpClient = new HttpClient();
            string apiKey = "PBZJ12Y7HpKiyW70qMjqml51y4JM6kbq8QjBt0GQ";
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            _ = FetchApiRandom(RandomWord);

            _list = new List<SuggestionModel>();
            FillList();
            _count = _list.Count;
        }

        private async Task LoadRecentWords()
        {
            RecentWords = new ObservableCollection<HistoryWord>(await _historyWordService.GetHistoryWordsAsync());
        }
        private async Task NavigateToContentScreen(string inputWord)
        {
            if (InputWord != null)
            {
#if ANDROID
                if (Platform.CurrentActivity.CurrentFocus != null)
                    Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
                var result = new ContentScreenView();
                var resultViewModel = new DictionaryViewModel(inputWord);
                result.BindingContext = resultViewModel;
                await App.Current.MainPage.Navigation.PushModalAsync(result);
            }
        }
        private async Task search_Clicked()
        {
            if (string.IsNullOrEmpty(InputWord.word))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please input a word", "OK");
                return;
            }
            if (_editWordId == 0)
            {
                await _historyWordService.Create(new HistoryWord
                {
                    word = InputWord.word,

                });
            }
            else
            {
                await _historyWordService.Update(new HistoryWord
                {
                    word = InputWord.word
                });
                _editWordId = 0;
            }
            await NavigateToContentScreen(InputWord.word);
            InputWord = new HistoryWord();
            await LoadRecentWords();
        }
        [RelayCommand]
        async Task DeleteAllWords()
        {
            if (RecentWords.Count > 0)
            {
                await _historyWordService.DeleteAllAsync();
                await LoadRecentWords();
            }
            else
                App.Current.MainPage.DisplayAlert("Error", "There is no word in history to delete", "OK");
        }
        [RelayCommand]
        async Task SelectionChanged(HistoryWord s)
        {
            if (RecentWords.Contains(s))
            {
                var resultView = new ContentScreenView();
                resultView.BindingContext = new DictionaryViewModel(s.word);
                App.Current.MainPage.Navigation.PushModalAsync(resultView);
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", "Unable to show result", "OK");
            }
        }

        [RelayCommand]
        async Task RandWord()
        {

            var resultView = new ContentScreenView();
            resultView.BindingContext = new DictionaryViewModel(RandomWord);
            await App.Current.MainPage.Navigation.PushModalAsync(resultView);

        }
        [RelayCommand]
        async Task Delete(HistoryWord s)
        {
            if (RecentWords.Contains(s))
            {
                await _historyWordService.Delete(s);
                await LoadRecentWords();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Unable to delete", "OK");
            }
        }



        private List<SuggestionModel> _list;
        public List<SuggestionModel> TheList
        {
            get { return _list; }
            set { _list = value; }
        }

        private int _count;

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
        public async void FillList()
        {
            String line;
            SuggestionModel aSuggest = new SuggestionModel();
            try
            {
                using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("smallwords.txt");
                using StreamReader reader = new StreamReader(fileStream);
                while ((line = reader.ReadLine()) != null)
                {
                    aSuggest = new SuggestionModel();
                    aSuggest.TheSuggest = line;
                    _list.Add(aSuggest);
                }
            }
            catch (Exception ex)
            {
                _count = -1;
                SuggestionModel error = new SuggestionModel();
                error.TheSuggest = ex.ToString();
                _list.Add(error);
            }
        }

        [RelayCommand]
        void Bookmark1Click()
        {
            var bookmarkView = new BookmarkScreenView();
            bookmarkView.BindingContext = new BookmarkScreenViewModel();
            App.Current.MainPage.Navigation.PushModalAsync(bookmarkView);
        }
        [RelayCommand]
        void HomeClick()
        {
            App.Current.MainPage.DisplayAlert("ASDict", "You are in home", "OK");
        }

        [ObservableProperty]
        public string randomWord;

        public ObservableCollection<string> RandomWords = new ObservableCollection<string>();
        public async void LoadRandomWords()
        {
            string line;
            try
            {
                using Stream fileStream1 = await FileSystem.Current.OpenAppPackageFileAsync("randomwords.txt");
                using StreamReader reader1 = new StreamReader(fileStream1);
                while ((line = reader1.ReadLine()) != null)
                {
                    RandomWords.Add(line);
                }
            }
            catch
            {
                Console.WriteLine($"Request failed. Error");
            }
        }
        string GetRandomWord(ObservableCollection<string> collection)
        {
            if (collection.Count == 0)
            {
                return null;
            }

            Random random = new Random();
            int index = random.Next(collection.Count);
            return collection[index];
        }
        [ObservableProperty]
        public ObservableCollection<string> resultRandomList = new ObservableCollection<string>();

        [RelayCommand]
        async Task ListRandWord(string selectedWord)
        {

            var resultView = new ContentScreenView();
            resultView.BindingContext = new DictionaryViewModel(selectedWord);
            await App.Current.MainPage.Navigation.PushModalAsync(resultView);

        }
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task FetchApiRandom(string query)
        {
            string api_url = $"https://api.api-ninjas.com/v1/thesaurus?word={query}";

            try
            {
                var response = await _httpClient.GetStringAsync(api_url);
                DictionaryModel dictModel = JsonSerializer.Deserialize<DictionaryModel>(response);

                if (dictModel.synonyms.Count > 0)
                {
                    int count = 0;
                    while (count < 3)
                    {
                        ResultRandomList.Add(dictModel.synonyms[count]);
                        count++;
                    }   
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed. Error: {ex.Message}");
            }
        } 
    }
}

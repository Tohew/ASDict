using ASDict.MVVM.Models;
using ASDict.MVVM.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ASDict.MVVM.ViewModels
{
    public partial class BookmarkScreenViewModel : ObservableObject
    {
        [ObservableProperty]
        private HistoryWord inputWord;
        
        [ObservableProperty]
        private HistoryWord inputWordWin;

        [ObservableProperty]
        private ObservableCollection<HistoryWord> recentWords;

        private readonly HistoryWordService _historyWordService;
        private int _editWordId;

        [ObservableProperty]
        public string word;

        [ObservableProperty]
        public Bookmark selectedFavoriteWord;

        [ObservableProperty]
        ObservableCollection<Bookmark> favoriteWords;

        public ICommand selectedCommand { get; }
        public ICommand setSelectedFavoriteWord { get; }
        public ICommand searchBarCommand{ get; }
        private readonly BookmarkService _bookmarkService;
        private bool _isSortedAlphabetically = false;

        public BookmarkScreenViewModel()
        {
            _historyWordService = new HistoryWordService();
            InputWord = new HistoryWord();
            InputWordWin = new HistoryWord();
            RecentWords = new ObservableCollection<HistoryWord>();
            Task.Run(async () => await LoadRecentWords());

            _bookmarkService = new BookmarkService();
            FavoriteWords = new ObservableCollection<Bookmark>();
            Task.Run(async () => await LoadFavoriteWords());

            searchBarCommand = new Command(searchWinBookmark);

            _list = new List<SuggestionModel>();
            FillList();
            _count = _list.Count;
        }
        public ICommand SortBookmarksCommand => new Command(SortBookmarks);

        private void SortBookmarks()
        {
            if (!_isSortedAlphabetically)
            {
                FavoriteWords = new ObservableCollection<Bookmark>(FavoriteWords.OrderBy(b => b.bookmarkWord));
                _isSortedAlphabetically = true;
            }
            else
            {
                // Sort back to default (newest first, assuming 'wordId' is used for this purpose)
                FavoriteWords = new ObservableCollection<Bookmark>(FavoriteWords.OrderByDescending(b => b.wordId));
                _isSortedAlphabetically = false;
            }
        }
        private async Task LoadFavoriteWords()
        {
            FavoriteWords = new ObservableCollection<Bookmark>(await _bookmarkService.GetBookmarksAsync());
        }

        [RelayCommand]
        async Task Delete(Bookmark s)
        {
            if (FavoriteWords.Contains(s))
            {
                await _bookmarkService.Delete(s);
                await LoadFavoriteWords();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Unable to delete", "OK");
            }
        }

        [RelayCommand]
        void Search(Bookmark s)
        {
            if (FavoriteWords.Contains(s))
            {
                var resultView = new ContentScreenView();
                resultView.BindingContext = new DictionaryViewModel(s.bookmarkWord);
                App.Current.MainPage.Navigation.PushModalAsync(resultView);
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", "Unable to show result", "OK");
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
        [RelayCommand]
        private async void searchBookmark()
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
        private async void searchWinBookmark()
        {
            if (string.IsNullOrEmpty(InputWordWin.word))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please input a word", "OK");
                return;
            }
            if (_editWordId == 0)
            {
                await _historyWordService.Create(new HistoryWord
                {
                    word = InputWordWin.word,

                });
            }
            else
            {
                await _historyWordService.Update(new HistoryWord
                {
                    word = InputWordWin.word
                });
                _editWordId = 0;
            }
            await NavigateToContentScreen(InputWordWin.word);
            InputWordWin = new HistoryWord();
            await LoadRecentWords();
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
            App.Current.MainPage.DisplayAlert("ASDict", "You are in Bookmark", "OK");
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
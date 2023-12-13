using ASDict.MVVM.Models;
using ASDict.MVVM.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            searchCommand = new Command(search_Clicked);
            //selectionChangedCommand = new Command(selectionChanged);
        }
        private async Task LoadRecentWords()
        {
            RecentWords = new ObservableCollection<HistoryWord>(await _historyWordService.GetHistoryWordsAsync());
        }
        private async Task NavigateToContentScreen(string inputWord)
        {
            if (InputWord != null)
            {
                if (Platform.CurrentActivity.CurrentFocus != null)
                    Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);

                var result = new ContentScreenView();
                var resultViewModel = new DictionaryViewModel(inputWord);
                result.BindingContext = resultViewModel;
                await App.Current.MainPage.Navigation.PushModalAsync(result);
            }
        }
        private async void search_Clicked()
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
    }
}

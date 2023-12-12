﻿using ASDict.MVVM.Models;
using ASDict.MVVM.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ASDict.MVVM.ViewModels
{
    public partial class BookmarkScreenViewModel : ObservableObject
    {
        [ObservableProperty]
        public string word;

        [ObservableProperty]
        public Bookmark selectedFavoriteWord;

        [ObservableProperty]
        ObservableCollection<Bookmark> favoriteWords;


        public ICommand selectedCommand {  get; }
        public ICommand setSelectedFavoriteWord {  get; }
        public ICommand deleteClicked { get; }
        private readonly BookmarkService _bookmarkService;
        private bool _isSortedAlphabetically = false;

        public BookmarkScreenViewModel()
        {
            _bookmarkService = new BookmarkService();
            FavoriteWords = new ObservableCollection<Bookmark>();
            Task.Run(async () => await LoadFavoriteWords());            
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
    }
}

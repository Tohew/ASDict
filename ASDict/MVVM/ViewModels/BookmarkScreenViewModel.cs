using ASDict.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Javax.Security.Auth;
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
        public Bookmark selectedFavorWord;

        [ObservableProperty]
        public ObservableCollection<Bookmark> favoriteWords;

        public ICommand starWhiteClicked { get; }

        private readonly BookmarkService _bookmarkService;
        private int _editWordId;

        public BookmarkScreenViewModel()
        {
            _bookmarkService = new BookmarkService();
            FavoriteWords = new ObservableCollection<Bookmark>();
            Task.Run(async () => await LoadFavoriteWords());
            starWhiteClicked = new Command(star_white_Clicked);
        }
        private async Task LoadFavoriteWords()
        {
            FavoriteWords = new ObservableCollection<Bookmark>(await _bookmarkService.GetBookmarksAsync());
        }
        private bool isStarWhite = true;
        private async void star_white_Clicked()
        {
            if(_editWordId == 0)
            {
                await _bookmarkService.Create(new Bookmark
                {
                    bookmarkWord = Word,
                });
            }
            else
            {
                await _bookmarkService.Update(new Bookmark
                {
                    bookmarkWord = Word
                });
            }
            await LoadFavoriteWords();
        }
    }
}

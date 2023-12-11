using ASDict.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private readonly BookmarkService bookmarkService;

        public BookmarkScreenViewModel()
        {
            bookmarkService = new BookmarkService();
            FavoriteWords = new ObservableCollection<Bookmark>();
            Task.Run(async () => await LoadFavoriteWords());
        }
        private async Task LoadFavoriteWords()
        {
            FavoriteWords = new ObservableCollection<Bookmark>(await bookmarkService.GetBookmarksAsync());
        }
    }
}

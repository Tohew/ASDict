using ASDict.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDict.MVVM.ViewModels
{
    public partial class SuggestionViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<SuggestionModel> _suggestions;
        public SuggestionViewModel()
        {
            Suggestions = new ObservableCollection<SuggestionModel>();
            this.Suggestions.Add(new SuggestionModel() { Name = "Facebook", ID = 0 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Google Plus", ID = 1 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Instagram", ID = 2 });
            this.Suggestions.Add(new SuggestionModel() { Name = "LinkedIn", ID = 3 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Skype", ID = 4 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Telegram", ID = 5 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Televzr", ID = 6 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Tik Tok", ID = 7 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Tout", ID = 8 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Tumblr", ID = 9 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Twitter", ID = 10 });
            this.Suggestions.Add(new SuggestionModel() { Name = "Vimeo", ID = 11 });
            this.Suggestions.Add(new SuggestionModel() { Name = "WhatsApp", ID = 12 });
            this.Suggestions.Add(new SuggestionModel() { Name = "YouTube", ID = 13 });
        }
    }
}

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
    public class SuggestionViewModel
    {
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

        public SuggestionViewModel()
        {
            _list = new List<SuggestionModel>();
            FillList();
            _count = _list.Count;
        }
    }
}

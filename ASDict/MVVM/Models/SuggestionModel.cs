using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDict.MVVM.Models
{
    public class SuggestionModel
    {
        private String _suggestionmodel;
        public SuggestionModel()
        {

        }

        public String TheSuggest
        { 
            get { return _suggestionmodel; }
            set { _suggestionmodel = value; } 
        }
    }
}

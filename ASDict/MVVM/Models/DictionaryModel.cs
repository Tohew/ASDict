using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDict.MVVM.Models
{
    public class DictionaryModel
    {
        public string word {  get; set; }
        public List<string> synonyms { get; set; }
        public List<string> antonyms { get; set; }
    }
}

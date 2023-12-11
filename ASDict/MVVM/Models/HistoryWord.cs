using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDict.MVVM.Models
{
    [Table("HistoryWord")]
    public class HistoryWord
    {
            [PrimaryKey, AutoIncrement]
            [Column("ID")]
            public int wordId { get; set; }
            [Column("word")]
            public string word { get; set; }
    }
}

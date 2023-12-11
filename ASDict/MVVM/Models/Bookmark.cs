using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDict.MVVM.Models
{
    [Table("Bookmark")]
    public class Bookmark
    {
        [PrimaryKey, AutoIncrement]
        [Column("ID")]
        public int wordId { get; set; }
        [Column("word")]
        public string bookmarkWord { get; set; }
    }
}

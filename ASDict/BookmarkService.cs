using ASDict.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDict
{
    public class BookmarkService
    {
        private const string DB_NAME = "favour.db3";
        private const int MaxStorageLimit = 100;
        private readonly SQLiteAsyncConnection _connection;

        public BookmarkService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            _connection.CreateTableAsync<Bookmark>();
        }
        public async Task<IEnumerable<Bookmark>> GetBookmarksAsync()
        {
            return await _connection.Table<Bookmark>().OrderByDescending(x => x.wordId).ToListAsync();
        }
        public async Task<Bookmark> GetById(int wordId)
        {
            return await _connection.Table<Bookmark>().
                Where(x => x.wordId == wordId).FirstOrDefaultAsync();
        }
        public async Task Create(Bookmark bookmark)
        {
            var existWord = await _connection.Table<Bookmark>().
                Where(x => x.bookmarkWord == bookmark.bookmarkWord).FirstOrDefaultAsync();
            if(existWord != null)
            {
                await _connection.DeleteAsync(existWord);
            }       
            var currWordCount = await _connection.Table<Bookmark>().CountAsync();
            if(currWordCount >= MaxStorageLimit)
            {
                var oldestWord = await _connection.Table<Bookmark>().OrderBy(x => x.wordId).FirstOrDefaultAsync();
                if(oldestWord != null)
                {
                    await _connection.DeleteAsync(oldestWord);
                }
            }
            await _connection.InsertAsync(bookmark);
        }
        public async Task Update(Bookmark bookmark)
        {
            await _connection.UpdateAsync(bookmark);
        }
        public async Task Delete(Bookmark bookmark)
        {
            await _connection.DeleteAsync(bookmark);
        }
    }
}

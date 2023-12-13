using ASDict.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDict
{
    public class HistoryWordService
    {
        private const string DB_NAME = "db_recent.db3";
        private const int MaxStorageLimit = 5;
        private readonly SQLiteAsyncConnection _connection;

        public HistoryWordService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<HistoryWord>();
        }
        public async Task<IEnumerable<HistoryWord>> GetHistoryWordsAsync()
        {
            return await _connection.Table<HistoryWord>().OrderByDescending(x => x.wordId).ToListAsync();
        }
        public async Task<HistoryWord> GetById(int wordId)
        {
            return await _connection.Table<HistoryWord>().Where(x => x.wordId == wordId).FirstOrDefaultAsync();
        }
        public async Task Create(HistoryWord historyWord)
        {
            var existingWord = await _connection.Table<HistoryWord>().Where(x => x.word == historyWord.word).FirstOrDefaultAsync();
            if (existingWord != null)
            {
                await _connection.DeleteAsync(existingWord);
            }
            var currentWordCount = await _connection.Table<HistoryWord>().CountAsync();
            if (currentWordCount >= MaxStorageLimit)
            {
                var oldestWord = await _connection.Table<HistoryWord>().OrderBy(x => x.wordId).FirstOrDefaultAsync();
                if (oldestWord != null)
                {
                    await _connection.DeleteAsync(oldestWord);
                }
            }
            await _connection.InsertAsync(historyWord);
        }
        public async Task Update(HistoryWord historyWord)
        {
            await _connection.UpdateAsync(historyWord);
        }
        public async Task Delete(HistoryWord historyWord)
        {
            await _connection.DeleteAsync(historyWord);
        }
        public async Task DeleteAllAsync()
        {
            await _connection.DeleteAllAsync<HistoryWord>();
        }
    }
}
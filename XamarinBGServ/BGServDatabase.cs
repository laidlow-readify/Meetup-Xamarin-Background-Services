using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using XamarinBGServ.Models;

namespace XamarinBGServ
{
    public class BGServDatabase
    {
        readonly SQLiteAsyncConnection database;

        public BGServDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<LoggerItem>().Wait();
        }

        public Task<List<LoggerItem>> GetItemsAsync()
        {
            return database.Table<LoggerItem>().ToListAsync();
        }

        public Task<int> SaveItemAsync(LoggerItem item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(LoggerItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}

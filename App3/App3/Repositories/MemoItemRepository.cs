using System.Collections.Generic;
using System.Threading.Tasks;
using App3.Models;
using SQLite;

namespace App3.Repositories
{
    public class MemoItemRepository : IMemoItemRepository
    {
        readonly SQLiteAsyncConnection connection;

        public MemoItemRepository(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<MemoItem>().Wait();
        }

        public Task<List<MemoItem>> GetAllItemsAsync()
        {
            return connection.Table<MemoItem>().ToListAsync();
        }

        public Task<MemoItem> GetItemAsync(int id)
        {
            return connection.Table<MemoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(MemoItem item)
        {
            if (item.ID != 0)
            {
                return connection.UpdateAsync(item);
            }
            else
            {
                return connection.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(MemoItem item)
        {
            return connection.DeleteAsync(item);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using App3.Models;

namespace App3.Repositories
{
    public interface IMemoItemRepository
    {
        Task<List<MemoItem>> GetAllItemsAsync();
        Task<MemoItem> GetItemAsync(int id);
        Task<int> SaveItemAsync(MemoItem item);
        Task<int> DeleteItemAsync(MemoItem item);
    }
}

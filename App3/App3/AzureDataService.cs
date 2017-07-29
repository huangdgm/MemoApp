using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace App3
{

    public class AzureDataService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<MemoItem> memoTable;

        public async Task Initialize()
        {
            //Create our client
            MobileService = new MobileServiceClient("https://memotool.azurewebsites.net");

            //const string path = "syncstore.db";
            const string path = "MS_TableConnectionString";
            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<MemoItem>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            memoTable = MobileService.GetSyncTable<MemoItem>();
        }

        public async Task<IEnumerable> GetMemos()
        {
            await SyncMemo();
            return await memoTable.OrderBy(c => c.DateUtc).ToEnumerableAsync();
        }

        public async Task AddMemo()
        {
            //create and insert Memo
            var memo = new MemoItem
            {
                DateUtc = DateTime.UtcNow,
            };

            await memoTable.InsertAsync(memo);

            //Synchronize Memo
            await SyncMemo();
        }

        public async Task SyncMemo()
        {
            //pull down all latest changes and then push current memos up
            await memoTable.PullAsync("allMemos", memoTable.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }
    }

}

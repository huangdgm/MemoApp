using SQLite;

namespace App3.Models
{
    [Table("MemoItem")]
    public class MemoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
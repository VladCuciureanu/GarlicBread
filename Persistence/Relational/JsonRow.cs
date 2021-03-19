using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GarlicBread.Persistence.Document;

namespace GarlicBread.Persistence.Relational
{
    public class JsonRow<TData> where TData : JsonRootObject<TData>, new()
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong GuildId { get; set; }

        [Column(TypeName = "jsonb")] public TData Data { get; set; } = new TData();
    }
}
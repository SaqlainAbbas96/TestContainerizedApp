using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestContainerizedApp
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }


        [Column("full_name")]
        public string FullName { get; set; } = null!;


        [Column("email")]
        public string? Email { get; set; }


        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}

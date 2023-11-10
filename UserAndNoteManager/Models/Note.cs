using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace UserAndNoteManager.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string Content { get; set; }
        
        [Required]
        public DateTime DateCreated { get; set; }
        
        [Required]
        public DateTime DateModified { get; set; }
        
        [Required]
        public int Views { get; set; }
        
        [Required]
        public bool Published { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User? User { get; set; }
    }
}

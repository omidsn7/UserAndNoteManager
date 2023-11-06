using System.ComponentModel.DataAnnotations;

namespace UserAndNoteManager.Models
{
    public class Note
    {
        [Key]
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
    }
}

namespace UserAndNoteManager.Models
{
    public class Note
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Views { get; set; }
        public bool Published { get; set; }

    }
}

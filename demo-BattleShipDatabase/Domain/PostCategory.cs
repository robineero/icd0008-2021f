namespace Domain
{
    public class PostCategory
    {
        public int Id { get; set; }
        
        // Foreign key
        public int PostId { get; set; }
        public Post? Post { get; set; }
        
        // Foreign key
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
    }
}
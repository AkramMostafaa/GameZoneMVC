namespace GameZone.Models
{
    public class Category:BaseEntity
    {
        public ICollection<Games> Games { get; set; } = new HashSet<Games>();
    }
}

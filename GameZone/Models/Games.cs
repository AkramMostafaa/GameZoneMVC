namespace GameZone.Models
{
    public class Games:BaseEntity
    {

        public string Description { get; set; } = string.Empty;
        public string Cover { get; set; }=string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<GameDevice> GameDevices { get; set; }=new HashSet<GameDevice>();

    }
}

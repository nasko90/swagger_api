namespace Data_Access.Models
{
    public class TestModel : BaseEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string[] Questions { get; set; }
    }
}
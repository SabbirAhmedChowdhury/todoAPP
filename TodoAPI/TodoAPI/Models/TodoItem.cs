namespace TodoApi.Models
{
    public class TodoItem
    {
        public decimal Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}

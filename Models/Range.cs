namespace MyProject18._05._2026.Models
{
    public class Range : BaseEntity
    {
        public string name { get; set; }
        public DateTime StartTime {  get; set; }
        public List<Employee> Employees { get; set; }

    }
}

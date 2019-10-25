namespace DorllyServiceManager.Models
{
    public class CategoryProperties
    {
        public int CategoryId { get; set; }
        public ServiceCategory Category { get; set; }
        public int PropertyId { get; set; }
        public ServiceProperty Property { get; set; }
    }
}

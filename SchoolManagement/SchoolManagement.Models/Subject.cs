namespace SchoolManagement.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public bool IsCore { get; set; }

        public void Print()
        {
            Console.WriteLine("ID: " + this.Id);
            Console.WriteLine("Name: " + this.Name);
            Console.WriteLine("Category: " + this.Category);
            Console.WriteLine("ClassRoom: " + this.ClassRoom);
            Console.WriteLine("IsCore " + this.IsCore);
        }
    }
}

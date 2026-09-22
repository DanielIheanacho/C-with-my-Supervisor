namespace SchoolManagement.Models
{
    public class ClassRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public void Print()
        {
            Console.WriteLine("ClassRoom ID: " + this.Id);
            Console.WriteLine("Name: " + this.Name);
            Console.WriteLine("Capacity: " + this.Capacity);
        }
    }
}

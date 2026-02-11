namespace StudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }          // Primary Key
        public string Name { get; set; }     // Not null
        public int Age { get; set; }
        public string Email { get; set; }
    }
}

namespace Student_WebApp.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }          // Primary Key
        public string Name { get; set; }     // Not null
        public int Age { get; set; }
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace StudentApi.DTOs
{
    public class UpdateStudentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }


}

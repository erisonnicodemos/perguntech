using System.ComponentModel.DataAnnotations;

namespace Perguntech.API.DTO
{
    public class QuestionDto
    {
        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Question { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Answer { get; set; }
    }

}
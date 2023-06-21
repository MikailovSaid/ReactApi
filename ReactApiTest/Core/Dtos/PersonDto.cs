using System.ComponentModel.DataAnnotations;

namespace ReactApiTest.Core.Dtos
{
    public class PersonDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
    }
}

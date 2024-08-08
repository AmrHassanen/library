using System.ComponentModel.DataAnnotations;

namespace eLoclizationTask.Dtos
{
    public class BookDto
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public bool IsBorrowed { get; set; }

        public IFormFile ImageUrl { get; set; }

        public int Count { get; set; }

    }
}

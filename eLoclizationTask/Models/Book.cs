using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    public bool IsBorrowed { get; set; }

    public string ImageUrl { get; set; }

    public int Count { get; set; }

    public ICollection<Borrow> Borrows { get; set; }
}

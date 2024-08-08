using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Borrow> Borrows { get; set; }
}

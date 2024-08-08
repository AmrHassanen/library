using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Borrow
{
    public int Id { get; set; }

    [Required]
    [ForeignKey("Book")]
    public int BookId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; } = 1;

    [Required]
    public DateTime BorrowDate { get; set; }

    public Book Book { get; set; }
    public User User { get; set; }
}

namespace BookStoreApi.Models;

public class Book
{
    public int Id {get; set;}
    public required string Title {get; set;}
    public string? Author {get; set;}
    public decimal Price {get; set;}
    public bool IsRead {get; set;} //false por default
    public string? SecretNotes {get; set;}
    public int CategoryId {get; set;}
    public Category? Category {get; set;}
}
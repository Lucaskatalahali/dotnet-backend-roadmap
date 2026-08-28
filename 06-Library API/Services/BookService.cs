using Library_API.Data;
using Library_API.Dtos;
using Library_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Services;

public class BookService
{
    private readonly AppDbContext _db;   

    public BookService(AppDbContext db)
    {
        _db = db;
    } 

    public async Task<BookResponseDto> CreateBook(CreateBookDto dto)
    {
        var book = new Book
        {
            ISBN = dto.ISBN,
            Title = dto.Title,
            Author = dto.Author,
            PublishedYear = dto.PublishedYear,
        };

        _db.Books.Add(book);
        await _db.SaveChangesAsync();

        return new BookResponseDto(
            book.Id, book.ISBN, book.Title, book.Author, book.PublishedYear, book.IsAvailable
        );
    }

    public async Task<List<BookResponseDto>> GetAllBooks()
    {
        var booksDto = await _db.Books.Select(b => new BookResponseDto(
            b.Id,
            b.ISBN,
            b.Title,
            b.Author,
            b.PublishedYear,
            b.IsAvailable
            )
        ).ToListAsync();

        return booksDto;
    }

    public async Task<BookResponseDto?> GetBook(int id)
    {
        var book = await _db.Books
            .Where(b => b.Id == id)
            .Select(b => new BookResponseDto(
            b.Id,
            b.ISBN,
            b.Title,
            b.Author,
            b.PublishedYear,
            b.IsAvailable
            )).FirstOrDefaultAsync();
        
        return book;
    }

    public async Task<bool> UpdateBook(int id, UpdateBookDto dto)
    {
        var book = await _db.Books.FindAsync(id);

        if(book is null) return false;

        book.ISBN = dto.ISBN;
        book.Title = dto.Title;
        book.Author = dto.Author;
        book.PublishedYear = dto.PublishedYear;
        book.IsAvailable = dto.IsAvailable!.Value;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> PatchBook(int id, PatchBookDto dto)
    {
        var book = await _db.Books.FindAsync(id);

        if(book is null) return false;

        if(dto.ISBN is not null) book.ISBN = dto.ISBN;
        if(dto.Author is not null) book.Author = dto.Author;
        if(dto.Title is not null) book.Title = dto.Title;
        if(dto.PublishedYear is not null) book.PublishedYear = dto.PublishedYear.Value;
        if(dto.IsAvailable is not null) book.IsAvailable = dto.IsAvailable.Value;

        await _db.SaveChangesAsync();
        
        return true;        
    }

    public async Task<bool> DeleteBook(int id)
    {
        var book = await _db.Books.FindAsync(id);

        if(book is null) return false;

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();

        return true;
    }
}
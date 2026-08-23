using BookStoreApi.Data;
using BookStoreApi.Dtos;
using BookStoreApi.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Endpoints;

public static class BookEndpoints
{
    public static RouteGroupBuilder MapBookEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/books");

        group.MapGet("/", GetAllBooks);
        group.MapGet("/{id}", GetBook);
        group.MapPost("/", CreateBook);
        group.MapPut("/{id}", UpdateBook);
        group.MapPatch("/{id}", PatchBook);
    group.MapDelete("/{id}", DeleteBook);

        return group;
    }

    private static async Task<IResult> GetAllBooks(AppDbContext db)
    {
        var books = await db.Books.Select(b => new BookResponseDto(
            b.Id, 
            b.Title, 
            b.Author, 
            b.Price, 
            b.IsRead, 
            new CategoryResponseDto(b.CategoryId, b.Category!.Name))).ToListAsync();

        return TypedResults.Ok(books);
    }

    private static async Task<IResult> GetBook(int id, AppDbContext db)
    {
        var bookDto = await db.Books
            .Where(b => b.Id == id)
            .Select(b => new BookResponseDto(
                b.Id,
                b.Title,
                b.Author,
                b.Price,
                b.IsRead,
                new CategoryResponseDto(b.CategoryId, b.Category!.Name)
            )).FirstOrDefaultAsync();
            
            return bookDto is null? TypedResults.NotFound() : TypedResults.Ok(bookDto);
    }

    private static async Task<IResult> CreateBook(CreateBookDto dto, AppDbContext db)
    {
        //I need to make sure that this category exists in data base, if yes, i will return it in the response
        var category = await db.Categories.FindAsync(dto.CategoryId);

        if(category is null) return Results.BadRequest("This category doesn't exist");

        var book = new Book{
            Title = dto.Title, 
            Author = dto.Author,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            IsRead = dto.IsRead
        };

        db.Books.Add(book);
        await db.SaveChangesAsync();

        var bookDto = new BookResponseDto(
                    book.Id, 
                    book.Title,
                    book.Author,
                    book.Price,
                    book.IsRead,
                    new CategoryResponseDto(book.CategoryId, category.Name));

        return TypedResults.Created($"/books/{book.Id}", bookDto);
    }

    private static async Task<IResult> UpdateBook(int id, UpdateBookDto dto, AppDbContext db, IValidator<UpdateBookDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        var categoryExists = await db.Categories.AnyAsync(c => c.Id == dto.CategoryId);
        if(!categoryExists)
        return TypedResults.BadRequest("The specified category does not exist.");
        
        var book = await db.Books.FindAsync(id);

        if(book is null) return TypedResults.NotFound();

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.Price = dto.Price;
        book.IsRead = dto.IsRead;

        book.CategoryId = dto.CategoryId;

        book.SecretNotes = dto.SecretNotes;

        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }
    
    private static async Task<IResult> PatchBook(int id, PatchBookDto dto, AppDbContext db)
    {
         if (dto.CategoryId.HasValue)
        {
            var categoryExists = await db.Categories.AnyAsync(c => c.Id == dto.CategoryId.Value);
            if (!categoryExists) return TypedResults.BadRequest("The specified category does not exist.");
        }

        var book = await db.Books.FindAsync(id);

        if(book is null) return TypedResults.NotFound();

        if(dto.Title is not null) book.Title = dto.Title;

        if(dto.Author is not null) book.Author = dto.Author;

        if(dto.Price.HasValue) book.Price = dto.Price.Value;

        if(dto.IsRead.HasValue) book.IsRead = dto.IsRead.Value;

        if(dto.SecretNotes is not null) book.SecretNotes = dto.SecretNotes;

        if (dto.CategoryId.HasValue) book.CategoryId = dto.CategoryId.Value;

        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteBook(int id, AppDbContext db)
    {
        var book = await db.Books.FindAsync(id);

        if(book is null) return TypedResults.NotFound();

        db.Books.Remove(book);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}
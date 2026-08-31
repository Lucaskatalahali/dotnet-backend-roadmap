using FluentValidation;
using Library_API.Dtos;
using Library_API.Services;

namespace Library_API.Endpoints;

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

    private static async Task<IResult> GetAllBooks(BookService bookService)
    {
        var books = await bookService.GetAllBooks();

        return TypedResults.Ok(books);
    }

    private static async Task<IResult> GetBook(int id, BookService bookService)
    {
        if(id <= 0) return TypedResults.BadRequest("Book id must be grater than zero");

        var bookDto = await bookService.GetBook(id);

        return bookDto is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(bookDto);
    }


    private static async Task<IResult> CreateBook(CreateBookDto BookDto, BookService bookService, IValidator<CreateBookDto> validator)
    {
        var validationResult = await validator.ValidateAsync(BookDto);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var bookResponseDto = await bookService.CreateBook(BookDto);

        return TypedResults.Created($"/books/{bookResponseDto.Id}", bookResponseDto);         
    }

    private static async Task<IResult> UpdateBook(int id, UpdateBookDto dto, BookService bookService, IValidator<UpdateBookDto> validator)
    {
        if(id <= 0) return TypedResults.BadRequest();
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        bool wasUpdated = await bookService.UpdateBook(id, dto);

        if(!wasUpdated) return TypedResults.NotFound();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> PatchBook(int id, PatchBookDto dto, BookService bookService, IValidator<PatchBookDto> validator)
    {
        if(id <= 0) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        bool wasPatched = await bookService.PatchBook(id, dto);

        if(!wasPatched) return TypedResults.NotFound();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteBook(int id, BookService bookService)
    {
        if(id <= 0) return TypedResults.BadRequest();

        bool WasDeleted = await bookService.DeleteBook(id);

        if(!WasDeleted) return TypedResults.NotFound();

        return TypedResults.NoContent();
    }
}
namespace Library_API.Dtos;

public record CreateBookDto(
    string ISBN,
    string Title,
    string Author,
    int PublishedYear
);
namespace Library_API.Dtos;

public record UpdateBookDto(
    string ISBN,
    string Title,
    string Author,
    int PublishedYear,
    bool? IsAvailable
);
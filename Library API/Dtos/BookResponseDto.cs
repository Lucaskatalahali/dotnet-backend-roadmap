namespace Library_API.Dtos;

public record BookResponseDto(
    int Id,
    string ISBN,
    string Title,
    string Author,
    int PublishedYear,
    bool IsAvailable
);
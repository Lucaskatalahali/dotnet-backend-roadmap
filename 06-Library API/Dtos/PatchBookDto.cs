namespace Library_API.Dtos;

public record PatchBookDto(
    string? ISBN,
    string? Title,
    string? Author,
    int? PublishedYear,
    bool? IsAvailable
);
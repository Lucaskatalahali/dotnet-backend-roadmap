namespace BookStoreApi.Dtos;

public record PatchBookDto(
    string? Title,
    string? Author,
    decimal? Price,
    bool? IsRead,
    string? SecretNotes,
    int? CategoryId
);
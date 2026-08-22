namespace BookStoreApi.Dtos;

public record UpdateBookDto(
    string Title,
    string? Author,
    decimal Price,
    bool IsRead,
    int CategoryId,
    string? SecretNotes //we are assuming it's an internal update, so we can include it.
);
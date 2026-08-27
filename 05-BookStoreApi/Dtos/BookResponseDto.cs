namespace BookStoreApi.Dtos;

public record BookResponseDto(
    int Id, 
    string Title, 
    string? Author,
    decimal Price, 
    bool IsRead,
    CategoryResponseDto Category
);

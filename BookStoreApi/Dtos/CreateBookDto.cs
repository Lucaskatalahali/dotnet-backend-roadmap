using BookStoreApi.Models;

namespace BookStoreApi.Dtos;

public record CreateBookDto(
    string Title, 
    string? Author, 
    decimal Price, 
    int CategoryId,
    bool IsRead = false
    );
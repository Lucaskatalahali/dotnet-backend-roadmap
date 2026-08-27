using Library_API.Models;

namespace Library_API.Dtos;

public record LoanResponseDto(
    int Id,
    Member Member,
    Book Book,
    DateTime BorrowedAt,
    DateTime DueDate,
    DateTime? ReturnedAt
);
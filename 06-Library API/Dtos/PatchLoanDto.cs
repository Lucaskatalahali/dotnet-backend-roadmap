namespace Library_API.Dtos;

public record PatchLoanDto(
    DateTime? DueDate,
    DateTime? ReturnedAt
);

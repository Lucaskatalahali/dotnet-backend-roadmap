namespace Library_API.Dtos;

public record CreateLoanDto(
    int MemberId,
    int BookId
);
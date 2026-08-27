namespace Library_API.Dtos;

public record MemberResponseDto(
    int Id,
    string Name,
    string Email,
    DateTime MembershipDate
);
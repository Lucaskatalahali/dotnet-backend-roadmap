using Library_API.Data;
using Library_API.Dtos;
using Library_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Services;

public record LoanResult(Loan? Loan, string? Error);

public class LoanService
{
    private AppDbContext _db;

    public LoanService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<LoanResponseDto>> GetAllLoans()
    {
        var loans = await _db.Loans.Select(l => new LoanResponseDto(
            l.Id, l.Member, l.Book, l.BorrowedAt, l.DueDate, l.ReturnedAt
            )
        ).ToListAsync();

        return loans;
    }

    public async Task<LoanResponseDto?> GetLoan(int id)
    {
        var loan = _db.Loans
            .Where(l => l.Id == id)
            .Select(l => new LoanResponseDto(
                l.Id, l.Member, l.Book, l.BorrowedAt, l.DueDate, l.ReturnedAt
                )
            ).FirstOrDefault();

        return loan;
    }

    public async Task<LoanResult> CreateLoan(CreateLoanDto loanDto)
    {
        //"Find" faz com que o Loan já tenha as entidades Book e Member, diferente de "Any".
        var member = await _db.Members.FindAsync(loanDto.MemberId);

        if(member is null) return new LoanResult(null, "MemberNotFound");

        var book = await _db.Books.FindAsync(loanDto.BookId);

        if(book is null) return new LoanResult(null, "BookNotFound");

        if(!book.IsAvailable) return new LoanResult(null, "BookNotAvailable");

        var loan = new Loan
        {
           MemberId = loanDto.MemberId,
           BookId = loanDto.BookId,
           Book = book,
           BorrowedAt = DateTime.UtcNow,
           DueDate = DateTime.UtcNow.AddDays(15),
        };

        book.IsAvailable = false;

        _db.Loans.Add(loan);
        await _db.SaveChangesAsync();



        return new LoanResult(loan, null);
    }
}
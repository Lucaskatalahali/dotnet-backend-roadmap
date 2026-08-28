using Library_API.Data;
using Library_API.Dtos;
using Library_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Services;

public record LoanResult(LoanResponseDto? LoanResponseDto, string? Error);

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

         var loanResponseDto = new LoanResponseDto(
            loan.Id,
            loan.Member,
            loan.Book,
            loan.BorrowedAt,
            loan.DueDate,
            loan.ReturnedAt 
        );

        return new LoanResult(loanResponseDto, null);
    }

    public async Task<LoanResponseDto?> PatchLoan(int id, PatchLoanDto loanDto)
    {
        var loan = await _db.Loans
        .Include(l => l.Book)
        .Include(l => l.Member)
        .FirstOrDefaultAsync(l => l.Id == id);

        if(loan is null) return null;


        if(loanDto.ReturnedAt is not null)
        {
            loan.ReturnedAt = loanDto.ReturnedAt;
            loan.Book.IsAvailable = true; // The book has been returned
        }
        
        if(loanDto.DueDate is not null)
            loan.DueDate = loanDto.DueDate.Value;

        await _db.SaveChangesAsync();

        return new LoanResponseDto(loan.Id, loan.Member, loan.Book, loan.BorrowedAt, loan.DueDate, loan.ReturnedAt);
    }

    public async Task<bool> DeleteLoan(int id)
    {
        var loan = await _db.Loans
        .Include(l => l.Book)
        .FirstOrDefaultAsync(l => l.Id == id);

        if(loan is null) return false;
        
        _db.Loans.Remove(loan);
        loan.Book.IsAvailable = true;

        await _db.SaveChangesAsync();

        return true;
    }
}
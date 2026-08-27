using Library_API.Dtos;
using Library_API.Services;

namespace Library_API.Endpoints;

public static class LoanEndpoints
{
    public static RouteGroupBuilder MapLoanEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/loans");

        group.MapGet("/", GetAllLoans);
        group.MapGet("/{id}", GetLoan);
        group.MapPost("/", CreateLoan);
        //group.MapPut("/{id}", UpdateLoan);
        //group.MapPatch("/{id}", PatchLoan);
        //group.MapDelete("/{id}", DeleteLoan);   

        return group; 
    }

    private static async Task<IResult> GetAllLoans(LoanService loanService)
    {
        var loans = await loanService.GetAllLoans();

        return TypedResults.Ok(loans);
    }

    private static async Task<IResult> GetLoan(int id, LoanService loanService)
    {
        if(id <= 0) return TypedResults.BadRequest();

        var loanDto = await loanService.GetLoan(id);

        return loanDto is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(loanDto);
    }

    private static async Task<IResult> CreateLoan(CreateLoanDto loanDto, LoanService loanService)
    {
        if(loanDto.BookId <= 0 || loanDto.MemberId <= 0) return TypedResults.BadRequest();

        var loanResult = await loanService.CreateLoan(loanDto);

        if(loanResult.Error == "MemberNotFound") 
            return TypedResults.NotFound("Member Not Found");
        
        if(loanResult.Error == "BookNotFound") 
            return TypedResults.NotFound("Book Not Found");

        if(loanResult.Error == "BookNotAvailable")
            return TypedResults.Conflict("Book Not Available");

        var loanResponseDto = new LoanResponseDto(
            loanResult.Loan!.Id,
            loanResult.Loan.Member,
            loanResult.Loan.Book,
            loanResult.Loan.BorrowedAt,
            loanResult.Loan.DueDate,
            loanResult.Loan.ReturnedAt 
        );
        
        return TypedResults.Created($"/loans/{loanResponseDto.Id}", loanResponseDto);
    }
}
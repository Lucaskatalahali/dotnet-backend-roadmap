using FluentValidation;
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
        group.MapPatch("/{id}", PatchLoan);
        group.MapDelete("/{id}", DeleteLoan);   

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
        
        return TypedResults.Created($"/loans/{loanResult.LoanResponseDto!.Id}", loanResult.LoanResponseDto);
    }

    private static async Task<IResult> PatchLoan(int id, PatchLoanDto loanDto, LoanService loanService, IValidator<PatchLoanDto> validator)
    {
        if(id <= 0) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(loanDto);

        if(!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var updatedLoanDto = await loanService.PatchLoan(id, loanDto);

        if(updatedLoanDto is null) return TypedResults.NotFound();

        return TypedResults.Ok(updatedLoanDto);        
    }

    private static async Task<IResult> DeleteLoan(int id, LoanService loanService)
    {
        if(id <= 0) return TypedResults.BadRequest();

        bool WasDeleted = await loanService.DeleteLoan(id);

        if(!WasDeleted) return TypedResults.NotFound();

        return TypedResults.NoContent();
    }
}
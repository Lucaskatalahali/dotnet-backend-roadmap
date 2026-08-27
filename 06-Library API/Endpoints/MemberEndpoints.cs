using FluentValidation;
using Library_API.Dtos;
using Library_API.Services;

namespace Library_API.Endpoints;

public static class MemberEndpoints
{
    public static RouteGroupBuilder MapMemberEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/members");

        group.MapGet("/", GetAllMembers);
        group.MapGet("/{id}", GetMember);
        group.MapPost("/", CreateMember);   
        group.MapPut("/{id}", UpdateMember);
        group.MapPatch("/{id}", PatchMember);
        group.MapDelete("/{id}", DeleteMember);

        return group;
    }

    private static async Task<IResult> GetAllMembers(MemberService memberService)
    {
        var membersDto = await memberService.GetAllMembers();

        return TypedResults.Ok(membersDto);
    }

    private static async Task<IResult> GetMember(int id, MemberService memberService)
    {
        if(id <= 0) return TypedResults.BadRequest();

        var MemberDto = await memberService.GetMember(id);

        if(MemberDto is null) return TypedResults.NotFound();

        return TypedResults.Ok(MemberDto);
    }

    private static async Task<IResult> CreateMember(CreateMemberDto memberDto, MemberService memberservice, IValidator<CreateMemberDto> validator)
    {
        var validationResult = await validator.ValidateAsync(memberDto);

        if(!validationResult.IsValid) 
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var member = await memberservice.CreateMember(memberDto);

        return TypedResults.Created($"/members/{member.Id}", memberDto);
    }

    private static async Task<IResult> UpdateMember(int id, UpdateMemberDto memberDto, MemberService memberService, IValidator<UpdateMemberDto> validator)
    {
        if(id <= 0) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(memberDto);

        if(!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        bool wasUpdated = await memberService.UpdateMember(id, memberDto);

        if(!wasUpdated) return TypedResults.NotFound();
        
        return TypedResults.NoContent();
    }

    private static async Task<IResult> PatchMember(int id, PatchMemberDto memberDto, MemberService memberService, IValidator<PatchMemberDto> validator)
    {
        if(id <= 0) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(memberDto);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        bool wasPatched = await memberService.PatchMember(id, memberDto);

        if(!wasPatched) return TypedResults.NotFound();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteMember(int id, MemberService memberService)
    {
        if(id <= 0) return TypedResults.BadRequest();

        bool WasDeleted = await memberService.DeleteMember(id);

        if(!WasDeleted) return TypedResults.NotFound();

        return TypedResults.NoContent();
    }
}
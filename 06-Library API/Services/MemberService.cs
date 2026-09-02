using Library_API.Data;
using Library_API.Dtos;
using Library_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal;

namespace Library_API.Services;

public class MemberService
{
    private readonly AppDbContext _db;

    public MemberService(AppDbContext db)
    {
        _db = db;   
    }

    public async Task<Member?> Login(LoginDto dto)
    {
        var member = await _db.Members.FirstOrDefaultAsync(x => x.Email == dto.Email);

        if(member is null) return null;

        var hasher = new PasswordHasher<Member>();

        var result = hasher.VerifyHashedPassword(
            member, 
            member.PasswordHash,
            dto.Password
        );

        return result == PasswordVerificationResult.Success
            ? member
            : null;
    }

    public async Task<List<MemberResponseDto>> GetAllMembers()
    {
        var membersDto = await _db.Members.Select(m => new MemberResponseDto(
            m.Id,
            m.Name,
            m.Email,
            m.MembershipDate
        )).ToListAsync();

        return membersDto;
    }

    public async Task<MemberResponseDto?> GetMember(int id)
    {
        //Para um simples GET que só precisa retornar um DTO, mais vale usar a abordagem uada no GetBook
        // Mas aqui vou usar o FindAsync só pra praticar as duas abordagens
        var member = await _db.Members.FindAsync(id);

        if(member is null) return null;

        return new MemberResponseDto(
            member.Id,
            member.Name,
            member.Email,
            member.MembershipDate
        );
    }

    public async Task<MemberResponseDto> CreateMember(CreateMemberDto dto)
    {
        var member = new Member{
            Name = dto.Name,
            Email = dto.Email,
            MembershipDate = DateTime.UtcNow
        };

        var hasher = new PasswordHasher<Member>();
        member.PasswordHash = hasher.HashPassword(member, dto.Password);

        _db.Members.Add(member);
        await _db.SaveChangesAsync();

        return new MemberResponseDto(member.Id, member.Name, member.Email, member.MembershipDate);        
    }

    public async Task<bool> UpdateMember(int id, UpdateMemberDto dto)
    {
        var member = await _db.Members.FindAsync(id);

        if(member is null) return false;

        member.Name = dto.Name;
        member.Email = dto.Email;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> PatchMember(int id, PatchMemberDto dto)
    {
        var member = await _db.Members.FindAsync(id);

        if(member is null) return false;

        if(dto.Name is not null) member.Name = dto.Name;
        if(dto.Email is not null) member.Email = dto.Email;

        await _db.SaveChangesAsync();
        
        return true;        
    }

    public async Task<bool> DeleteMember(int id)
    {
        var member = await _db.Members.FindAsync(id);

        if(member is null) return false;

        _db.Members.Remove(member);
        await _db.SaveChangesAsync();

        return true;
    }
}
using System.IdentityModel.Tokens.Jwt; // Classes específicas para trabalhar com JWT
using System.Security.Claims; // Classes para criar as claims do usuário
using System.Text; // Usado para converter a chave secreta em bytes (Encoding)
using Library_API.Models;
using Microsoft.IdentityModel.Tokens; // Classes para chave e assinatura do JWT

namespace Library_API.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(Member member)
    {
        // Informações que serão colocadas dentro do JWT.
        // Essas informações representam a identidade/permissões do usuário.
        var claims = new[]
        {
            // "sub" (subject): identifica quem é o usuário.
            // Aqui estamos usando o Id do Member.
            new Claim(JwtRegisteredClaimNames.Sub, member.Id.ToString()),

            // "email": adiciona o email do usuário ao token.
            new Claim(JwtRegisteredClaimNames.Email, member.Email),

            // Role: informa o papel/permissão do usuário.
            // Ex: "Admin" ou "Member".
            // RequireRole("Admin") poderá verificar essa claim depois.
            new Claim(ClaimTypes.Role, member.Role)
        };

        // A chave secreta usada para assinar o JWT.
        // Encoding.UTF8.GetBytes() converte a string em bytes,
        // porque SymmetricSecurityKey trabalha com bytes.
        //
        // IMPORTANTE: esta chave está aqui apenas temporariamente.
        // Depois vamos colocá-la de forma segura na configuração.

        /*var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("chave-secreta-aqui")
        );*/

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]! //key comes from appsettings
            )
        );

        // Define como o JWT será assinado:
        // - key: qual chave secreta será usada
        // - HmacSha256: qual algoritmo será usado para criar a assinatura
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        // Cria o objeto JWT propriamente dito.
        var token = new JwtSecurityToken(
            // Coloca as claims que criamos dentro do token.
            claims: claims,

            // Define quando o token irá expirar.
            // Por enquanto estamos usando 1 hora.
            expires: DateTime.UtcNow.AddHours(1),

            // Informa como o token deve ser assinado.
            signingCredentials: credentials
        );

        // Converte o objeto JwtSecurityToken para a string
        // que será enviada ao cliente.
        //
        // Resultado:
        // xxxxx.yyyyy.zzzzz
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
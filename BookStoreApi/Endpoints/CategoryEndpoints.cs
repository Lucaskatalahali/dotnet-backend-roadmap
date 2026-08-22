using Microsoft.EntityFrameworkCore;
using BookStoreApi.Data;    // Para enxergar o AppDbContext
using BookStoreApi.Models;  // Para enxergar a entidade Category
using BookStoreApi.Dtos;


namespace BookStoreApi.Endpoints;

public static class CategoryEndpoints
{
    public static RouteGroupBuilder MapCategoryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/categories");

        // Apenas aponta para o método correspondente
        group.MapGet("/", GetAllCategories); 
        group.MapGet("/{id}", GetCategory);
        group.MapPost("/", CreateCategory);
        group.MapPut("/{id}", UpdateCategory);
        group.MapPatch("/{id}", PatchCategory);
        group.MapDelete("/{id}", DeleteCategory);

        return group;
    }

    private static async Task<IResult> GetAllCategories(AppDbContext db)
    {
        var categories = await db.Categories.Select(c => new CategoryResponseDto(c.Id, c.Name)).ToListAsync();
        return TypedResults.Ok(categories);
    }

    private static async Task<IResult> GetCategory(int id, AppDbContext db)
    {
        var category = await db.Categories.FindAsync(id);

        if(category is null) return TypedResults.NotFound();

        CategoryResponseDto dto = new(category.Id, category.Name);
        return TypedResults.Ok(dto);
    }

    private static async Task<IResult> CreateCategory(CreateCategoryDto dto, AppDbContext db)
    {
        var category = new Category
        {
            Name = dto.Name
        };

        db.Categories.Add(category);
        await db.SaveChangesAsync();    

        var response = new CategoryResponseDto(category.Id, category.Name);
        
        return TypedResults.Created($"/categories/{category.Id}", response);
    }

    private static async Task<IResult> UpdateCategory(int id, CreateCategoryDto dto, AppDbContext db)
    {
        var category = await db.Categories.FindAsync(id);

        if(category is null) return TypedResults.NotFound();

        category.Name = dto.Name;

        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> PatchCategory(int id, CreateCategoryDto dto, AppDbContext db)
    {
        var category = await db.Categories.FindAsync(id);

        if(category is null) return TypedResults.NotFound();

        if(dto.Name is not null) category.Name = dto.Name;

        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteCategory(int id, AppDbContext db)
    {
        var category = await db.Categories.FindAsync(id);

        if(category is null) return TypedResults.NotFound();

        db.Categories.Remove(category);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }
    
}
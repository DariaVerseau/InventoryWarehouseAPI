using System.ComponentModel.DataAnnotations;

namespace DTO.Category;

public class UpdateCategoryDto
{
    public Guid Id { get; set; } 

    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название должно быть от 2 до 100 символов")]
    public string? Name { get; set; } 

    [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
    public string? Description { get; set; } 

    public bool? IsVisible { get; set; } 
    
}
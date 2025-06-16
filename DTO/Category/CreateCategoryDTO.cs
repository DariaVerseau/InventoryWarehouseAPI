using System.ComponentModel.DataAnnotations;

namespace DTO.Category;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Название категории обязательно")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название должно быть от 2 до 100 символов")]
    public string Name { get; set; } = String.Empty;
    
    [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
    public string Description { get; set; } = String.Empty;
    
    //public List<Guid> ProductIds { get; set; } = new();
}
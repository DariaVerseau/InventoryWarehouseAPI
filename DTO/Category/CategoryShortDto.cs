using System.ComponentModel.DataAnnotations;

namespace DTO.Category;

public class CategoryShortDto
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = String.Empty;
    //[StringLength(500)]
    //public string Description { get; set; } = String.Empty;
}
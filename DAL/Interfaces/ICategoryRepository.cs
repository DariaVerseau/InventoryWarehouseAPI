using DTO.Category;
namespace DAL.Interfaces;

public interface ICategoryRepository : IRepository<CategoryDto, CreateCategoryDto, UpdateCategoryDto>
{

}

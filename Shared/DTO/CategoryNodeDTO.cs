using SharedLib.AdminDTO;

namespace BlazorAppAdmin.Pages.Category
{
    public class CategoryNodeDTO : CategoryDTO
    {
        public List<CategoryNodeDTO> children = new List<CategoryNodeDTO>();
    }
}

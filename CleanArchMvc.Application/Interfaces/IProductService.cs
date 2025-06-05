using CleanArchMvc.Application.DTOs;


namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int id);
        Task<ProductDTO> GetProductCategory(int id);
        Task<ProductDTO> Add(ProductDTO productDTO);
        Task<ProductDTO> Update(ProductDTO productDTO);
        Task Delete(int? id);

    }
}

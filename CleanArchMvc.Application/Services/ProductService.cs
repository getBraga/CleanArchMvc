using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;


namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper;
      
        }

        public async Task<ProductDTO> Add(ProductDTO productDTO)
        {
            var addProduct = _mapper.Map<Product>(productDTO);
            var productAddEntity = await _productRepository.CreateAsync(addProduct);
            return _mapper.Map<ProductDTO>(productAddEntity);
        }

        public async Task Delete(int? id)
        {
            var deleteProduct = await _productRepository.GetByIdAsync(id);
            await _productRepository.RemoveAsync(deleteProduct);
        }

        public async Task<ProductDTO> GetProductById(int? id)
        {
            var productId = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(productId);
        }

        public async Task<ProductDTO> GetProductCategory(int? id)
        {
            var productCaregory = await _productRepository.GetProductCategoryAsync(id);
            return _mapper.Map<ProductDTO>(productCaregory);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            var addProduct = _mapper.Map<Product>(productDTO);
            var productUpdate = await _productRepository.UpdateAsync(addProduct);
            return _mapper.Map<ProductDTO>(productUpdate);
        }
    }
}

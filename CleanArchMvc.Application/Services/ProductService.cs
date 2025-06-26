using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;


namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
  
        public ProductService(IMapper mapper, IMediator mediator)
        {
          
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ProductDTO> Add(ProductDTO productDTO)
        {  
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            var result  = await _mediator.Send(productCreateCommand);
            return _mapper.Map<ProductDTO>(result);
        }

        public async Task Delete(int id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id);
            await _mediator.Send(productRemoveCommand);

        }
        public async Task<ProductDTO> GetProductById(int id)
        {
            var mediatorProduts = new GetProductByIdQuery(id);
            var products = await _mediator.Send(mediatorProduts);        
            return _mapper.Map<ProductDTO>(products);
        }

      
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var mediatorProduts = new GetProductsQuery();
            var productsCQRS = await _mediator.Send(mediatorProduts);
            return _mapper.Map<IEnumerable<ProductDTO>>(productsCQRS);

        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            var productResult = await _mediator.Send(productUpdateCommand);
            return _mapper.Map<ProductDTO>(productResult);
        }
    }
}

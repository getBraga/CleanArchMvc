using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mappe)
        {
            _categoryRepository = categoryRepository;
            _mapper = mappe;
        }

        public async Task<CategoryDTO> Add(CategoryDTO categoryDTO)
        {
            var categoryEntityAdd = _mapper.Map<Category>(categoryDTO);
            var category = await _categoryRepository.CreateAsync(categoryEntityAdd);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);

        }

        public async Task Remove(int id)
        {
            var categoryRemoveAwait = await _categoryRepository.GetByIdAsync(id);
            if(categoryRemoveAwait != null) 
            await _categoryRepository.RemoveAsync(categoryRemoveAwait);
        }

        public async Task<CategoryDTO> Update(CategoryDTO categoryDTO)
        {
            var categoryEntityUpdate = _mapper.Map<Category>(categoryDTO);
            var category = await _categoryRepository.UpdateAsync(categoryEntityUpdate);
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}

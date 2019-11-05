using AutoMapper;
using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.DataTransferObject.Product;
using Granny.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Granny.Services
{
    public class ProductServices : IProductServices
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductServices(
            IUnitOfWork unitOfWork, 
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            await _productRepository.Create(product);
            await _unitOfWork.SaveAsync();
        }

        public ProductDto GetById(string pluCode)
        {
            long productId = _mapper.Map<long>(pluCode);
            Product result = _productRepository.Get(productId);
            return _mapper.Map<ProductDto>(result);
        }
    }
}

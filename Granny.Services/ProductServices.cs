using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Services.Interfaces;
using System.Threading.Tasks;

namespace Granny.Services
{
    public class ProductServices : IProductServices
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public ProductServices(
            IUnitOfWork unitOfWork, 
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Product product)
        {
            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public Product GetById(long pluCode)
        {
           return _productRepository.Get(pluCode);
        }
    }
}

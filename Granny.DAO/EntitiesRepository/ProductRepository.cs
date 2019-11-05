using System.Threading.Tasks;
using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.Repository;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;

namespace Granny.DAO.EntitiesRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(Product product)
        {
            await ObjectSet.AddAsync(product);
        }
    }
}

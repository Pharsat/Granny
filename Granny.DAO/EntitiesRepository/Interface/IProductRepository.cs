using System.Threading.Tasks;
using Granny.DAO.Repository.Interface;
using Granny.DataModel;

namespace Granny.DAO.EntitiesRepository.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task Create(Product location);
    }
}

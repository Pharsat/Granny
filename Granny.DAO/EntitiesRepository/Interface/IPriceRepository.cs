using Granny.DAO.Repository.Interface;
using Granny.DataModel;

namespace Granny.DAO.EntitiesRepository.Interface
{
    public interface IPriceRepository : IRepository<Price>
    {
        Price GetByProductLocation(long productId, int locationId);

        Price GetByProduct(long productId);
    }
}

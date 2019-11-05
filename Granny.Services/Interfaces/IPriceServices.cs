using Granny.DataModel;

namespace Granny.Services.Interfaces
{
    public interface IPriceServices
    {
        void Create(Price price);

        void Update(Price price);

        Price GetByProduct(long productId);

        Price GetByProductLocation(long productId, int locationId);
    }
}

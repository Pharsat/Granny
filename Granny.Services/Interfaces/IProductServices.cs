using Granny.DataModel;

namespace Granny.Services.Interfaces
{
    public interface IProductServices
    {
        void Create(Product product);

        void Update(Product product);

        Product GetById(long productId);
    }
}

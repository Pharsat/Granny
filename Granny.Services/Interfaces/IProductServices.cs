using Granny.DataModel;
using System.Collections.Generic;

namespace Granny.Services.Interfaces
{
    public interface IProductServices
    {
        void Create(Product product);

        void Update(Product product);

        List<Product> Get();
    }
}

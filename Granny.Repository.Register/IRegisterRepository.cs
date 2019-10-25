using Granny.DataModel;
using System;
using System.Threading.Tasks;

namespace Granny.Repository.Register
{
    public interface IRegisterRepository
    {
        Task InsertProduct(Product product);
        Task<int> InsertPrice(Price price);
    }
}

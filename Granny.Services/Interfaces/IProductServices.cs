using Granny.DataModel;
using System.Threading.Tasks;

namespace Granny.Services.Interfaces
{
    public interface IProductServices
    {
        Task Create(Product productDto);

        Product GetById(long pluCode);
    }
}

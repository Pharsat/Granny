using System.Threading.Tasks;
using Granny.DataModel;
using Granny.DataTransferObject.Product;

namespace Granny.Services.Interfaces
{
    public interface IProductServices
    {
        Task Create(ProductDto productDto);
        ProductDto GetById(string pluCode);
    }
}

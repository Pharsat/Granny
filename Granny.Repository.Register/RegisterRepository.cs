using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Granny.DataModel;

namespace Granny.Repository.Register
{
    public class RegisterRepository : IRegisterRepository
    {
        public Task<int> InsertPrice(Price price)
        {
            throw new NotImplementedException();
        }

        public Task InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
